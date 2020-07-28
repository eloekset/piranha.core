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

namespace Piranha.AspNetCore.Tests
{
    internal class TestData
    {
        public const string SchemeHttp = "http";
        public const string SchemeHttps = "https";
        public const string HostLocalhost = "localhost";
        public const int Port5000 = 5000;
        public const string PathBaseVirtualDirectory = @"/virtual-directory";

        public static void PopulateRequestHelperFor_Localhost_Port5000_VirtualDirectory_NoSite(IApplicationService service)
        {
            service.Request.Scheme = SchemeHttp;
            service.Request.Host = HostLocalhost;
            service.Request.Port = Port5000;
            service.Request.PathBase = PathBaseVirtualDirectory;
            service.Request.Url = CreateUrlForRequest(service.Request);
        }

        public static void PopulateRequestHelperFor_Localhost_Port5000_NoSite(IApplicationService service)
        {
            service.Request.Scheme = SchemeHttp;
            service.Request.Host = HostLocalhost;
            service.Request.Port = Port5000;
            service.Request.PathBase = null;
            service.Request.Url = CreateUrlForRequest(service.Request);
        }

        public static string CreateUrlForRequest(IRequestHelper request)
        {
            string port = request.Port.HasValue ? (":" + request.Port.Value) : string.Empty;
            return $"{request.Scheme}://{request.Host}{port}{request.PathBase}";
        }
    }
}
