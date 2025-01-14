// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using DbLocalizationProvider.AdminUI.ApiModels;
using DbLocalizationProvider.AdminUI.Models;
using DbLocalizationProvider.Commands;
using DbLocalizationProvider.Queries;

namespace DbLocalizationProvider.AdminUI
{
    //[Authorize]
    public class ResourcesApiController : ApiController
    {
        private const string CookieName = ".DbLocalizationProvider-SelectedLanguages";

        public IHttpActionResult Get()
        {
            return Ok(PrepareViewModel());
        }

        [HttpPost]
        public IHttpActionResult Update(CreateOrUpdateTranslationRequestModel model)
        {
            var cmd = new CreateOrUpdateTranslation.Command(model.Key, new CultureInfo(model.Language), model.Translation);
            cmd.Execute();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Remove(RemoveTranslationRequestModel model)
        {
            var cmd = new RemoveTranslation.Command(model.Key, new CultureInfo(model.Language));
            cmd.Execute();

            return Ok();
        }

        private LocalizationResourceApiModel PrepareViewModel()
        {
            var context = UiConfigurationContext.Current;
            var availableLanguagesQuery = new AvailableLanguages.Query { IncludeInvariant = context.ShowInvariantCulture };
            var languages = availableLanguagesQuery.Execute();

            var getResourcesQuery = new GetAllResources.Query(true);
            var resources = getResourcesQuery.Execute().OrderBy(r => r.ResourceKey).ToList();

            var user = RequestContext.Principal;
            var isAdmin = false;

            if (user != null)
            {
                isAdmin = user.Identity.IsAuthenticated && context.AuthorizedAdminRoles.Any(r => user.IsInRole(r));
            }

            return new LocalizationResourceApiModel(resources, languages)
            {
                AdminMode = isAdmin,
                HideDeleteButton = context.HideDeleteButton,
                IsRemoveTranslationButtonDisabled = UiConfigurationContext.Current.DisableRemoveTranslationButton
            };
        }

        private IEnumerable<string> GetSelectedLanguages()
        {
            var cookie = Request.Headers.GetCookies(CookieName).FirstOrDefault();

            return cookie?[CookieName].Value?.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
