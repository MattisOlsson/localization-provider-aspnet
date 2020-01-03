﻿// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using Newtonsoft.Json;

namespace DbLocalizationProvider.AdminUI.Models
{
    [JsonObject]
    public class CreateOrUpdateTranslationRequestModel
    {
        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("language")] public string Language { get; set; }

        [JsonProperty("newTranslation")] public string Translation { get; set; }
    }
}
