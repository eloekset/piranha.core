/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/piranhacms/piranha
 *
 */

using Piranha.AspNetCore.Services;
using Piranha.Repositories;
using Piranha.Services;
using Shouldly;
using Xunit;

namespace Piranha.AspNetCore.Tests.Services
{
    public class ApplicationService_AbsoluteUrlShould : BaseTests
    {
        private ApplicationService _applicationService;
        private IApi _api;

        public ApplicationService_AbsoluteUrlShould()
            : base()
        {
            _applicationService = new ApplicationService(_api);
        }

        /// <summary>
        /// Sets up & initializes the tests.
        /// </summary>
        protected override void Init()
        {
            _api = CreateApi();
            Piranha.App.Init(_api);
        }

        /// <summary>
        /// Cleans up any possible data and resources
        /// created by the test.
        /// </summary>
        protected override void Cleanup() 
        {
            _api.Dispose();
            _api = null;
            _applicationService = null;
        }

        private IApi CreateApi()
        {
            var factory = new ContentFactory(services);
            var serviceFactory = new ContentServiceFactory(factory);

            var db = GetDb();

            return new Api(
                factory,
                new AliasRepository(db),
                new ArchiveRepository(db),
                new Piranha.Repositories.MediaRepository(db),
                new PageRepository(db, serviceFactory),
                new PageTypeRepository(db),
                new ParamRepository(db),
                new PostRepository(db, serviceFactory),
                new PostTypeRepository(db),
                new SiteRepository(db, serviceFactory),
                new SiteTypeRepository(db)
            );
        }

        [Fact]
        public void AppendPathBase_ForVirtualDirectory()
        {
            string slug = "/";
            string expectedUrl = @"http://localhost:5000/virtual-directory/";
            TestData.PopulateRequestHelperFor_Localhost_Port5000_VirtualDirectory_NoSite(_applicationService);
            string actualUrl = _applicationService.AbsoluteUrl(slug);
            actualUrl.ShouldNotBeNullOrWhiteSpace("AbsoluteUrl generated some value");
            actualUrl.ShouldBe(expectedUrl, "AbsoluteUrl generated the expected value");
        }

        [Fact]
        public void NotAppendPathBase_WhenNotHostedInVirtualDirectory()
        {
            string slug = "/";
            string expectedUrl = @"http://localhost:5000/";
            TestData.PopulateRequestHelperFor_Localhost_Port5000_NoSite(_applicationService);
            string actualUrl = _applicationService.AbsoluteUrl(slug);
            actualUrl.ShouldNotBeNullOrWhiteSpace("AbsoluteUrl generated some value");
            actualUrl.ShouldBe(expectedUrl, "AbsoluteUrl generated the expected value");
        }
    }
}
