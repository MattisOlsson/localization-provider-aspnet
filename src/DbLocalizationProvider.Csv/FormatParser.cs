using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using DbLocalizationProvider.Import;

namespace DbLocalizationProvider.Csv
{
    public class FormatParser : IResourceFormatParser
    {
        private readonly Func<ICollection<CultureInfo>> _getLanguagesFunc;

        public FormatParser(Func<ICollection<CultureInfo>> getLanguagesFunc)
        {
            _getLanguagesFunc = getLanguagesFunc;
        }

        public string FormatName => "CSV";
        
        public string[] SupportedFileExtensions => new[] {".csv"};
        
        public string ProviderId => "csv";

        public ParseResult Parse(string fileContent)
        {
            var resources = new List<LocalizationResource>();
            var languages = _getLanguagesFunc();
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim | TrimOptions.InsideQuotes
            };

            using (var stream = AsStream(fileContent))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                var records = csv.GetRecords<dynamic>().ToList();

                foreach (var record in records)
                {
                    var dict = (IDictionary<string, object>)record;
                    var resourceKey = dict["ResourceKey"] as string;

                    resources.Add(new LocalizationResource(resourceKey)
                    {
                        Translations = GetTranslations(dict, languages)
                    });
                }
            }

            return new ParseResult(resources, languages);
        }

        private ICollection<LocalizationResourceTranslation> GetTranslations(IDictionary<string, object> record, IEnumerable<CultureInfo> languages)
        {
            return languages.Select(x => new LocalizationResourceTranslation
            {
                Language = x.Name,
                Value = record.ContainsKey(x.Name)
                    ? record[x.Name] as string
                    : null
            }).ToList();
        }

        private Stream AsStream(string fileContent)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(fileContent);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
