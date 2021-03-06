/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

namespace Piranha.AspNetCore.Services
{
    /// <summary>
    /// The request helper provides information regarding the
    /// current request.
    /// </summary>
    public sealed class RequestHelper : IRequestHelper
    {
        /// <summary>
        /// Gets/sets the current hostname.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets/sets the current PathBase, which represents an URI segment for a IIS or Azure App Service Virtual Directory.
        /// </summary>
        public string PathBase { get; set; }

        /// <summary>
        /// Gets/sets the current port.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Gets/sets the current scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets/sets the requested raw url.
        /// </summary>
        public string Url { get; set; }
    }
}