using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix.Core.DTOs
{
    public class PhotoPrewiewDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
