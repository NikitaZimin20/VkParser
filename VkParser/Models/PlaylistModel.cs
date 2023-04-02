using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkParser.Models
{
    internal class PlaylistModel
    {
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public int Played { get; set; }
        public string Target { get; set; }
    }
}
