using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace comic_reader
{
    public static class ComicCollection
    {
        public static List<string> Comics { get; set; }

        public static Dictionary<string, List<string>> ComicsPages { get; set; }

        public static void Init()
        {
            Comics = Directory.GetDirectories("comic").ToList();
            ComicsPages = new Dictionary<string, List<string>>();
            foreach (var comic in Comics)
            {
                var ext = new List<string> { ".jpg", ".png", ".gif" };
                var images = Directory.GetFiles(comic, "*.*", SearchOption.AllDirectories)
                     .Where(s => ext.Contains(Path.GetExtension(s)));
                ComicsPages.Add(comic, images.ToList());
            }
        }
    }
}
