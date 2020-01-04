// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.Cache;
using DbLocalizationProvider.Queries;

namespace DbLocalizationProvider.Storage.MsSql
{
    public class AvailableLanguagesHandler : IQueryHandler<AvailableLanguages.Query, IEnumerable<CultureInfo>>
    {
        public IEnumerable<CultureInfo> Execute(AvailableLanguages.Query query)
        {
            var cacheKey = CacheKeyHelper.BuildKey($"AvailableLanguages_{query.IncludeInvariant}");

            if (ConfigurationContext.Current.CacheManager.Get(cacheKey) is IEnumerable<CultureInfo> cachedLanguages) return cachedLanguages;

            var languages = GetAvailableLanguages(query.IncludeInvariant);
            ConfigurationContext.Current.CacheManager.Insert(cacheKey, languages, false);

            return languages;
        }

        private IEnumerable<CultureInfo> GetAvailableLanguages(bool includeInvariant)
        {
            using (var db = new LanguageEntities())
            {
                var availableLanguages = db.LocalizationResourceTranslations
                    .Select(t => t.Language)
                    .Distinct()
                    .Where(l => includeInvariant || l != CultureInfo.InvariantCulture.Name)
                    .ToList()
                    .Select(l => new CultureInfo(l)).ToList();

                return availableLanguages;
            }
        }
    }
}
