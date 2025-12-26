using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix.Application.Extensions
{
    public static class PhotoExtensions
    {
        public static string BuildThumbnailUrl(this string photoUrl, Guid photoId)
        {
            var lastSlashIndex = photoUrl.LastIndexOf('/');
            if (lastSlashIndex == -1)
                return photoUrl;
            var baseUrl = photoUrl.Substring(0, lastSlashIndex);
            return $"{baseUrl}/thumbnail_{photoId}";
        }
    }
}
