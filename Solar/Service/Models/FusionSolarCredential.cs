using System;
using Vidyano.Service.Repository;

namespace Solar.Service.Models
{
    /// <summary>
    /// Contains the credentials used to access the openAPI of FusionSolar.
    /// </summary>
    /// <remarks>
    /// https://forum.huawei.com/enterprise/en/communicate-with-fusionsolar-through-an-openapi-account/thread/591478-100027
    /// </remarks>
    public class FusionSolarCredential
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        /// <summary>
        /// The XSRF-TOKEN cookie as received from the https://eu5.fusionsolar.huawei.com/thirdData/login call.
        /// </summary>
        [IgnoreProperty]
        public string? Token { get; set; }

        /// <summary>
        /// The <see cref="Token"/> is valid for 30 minutes, we'll use 28 minutes.
        /// </summary>
        [IgnoreProperty]
        public DateTimeOffset? TokenExpiration { get; set; }
    }
}