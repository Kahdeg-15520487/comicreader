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

        public static void Init(string[] dirs)
        {
            var localDirs = Directory.GetDirectories("comic").ToList();

            var otherDirs = dirs.SelectMany(d => Directory.GetDirectories(d)).ToList();
            Comics = new List<string>();
            Comics.AddRange(localDirs);
            Comics.AddRange(otherDirs.Select(d => Path.Combine("comic", Path.GetFileName(d))));
            Comics = Comics.Distinct().ToList();

            ComicsPages = new Dictionary<string, List<string>>();
            foreach (var comic in localDirs)
            {
                var ext = new List<string> { ".jpg", ".png", ".gif" };
                var images = Directory.GetFiles(comic, "*.*", SearchOption.AllDirectories)
                     .Where(s => ext.Contains(Path.GetExtension(s)));
                ComicsPages.Add(comic, images.ToList());
            }
            Console.WriteLine();
            foreach (var comic in otherDirs)
            {
                var vcomic = Path.Combine("comic", Path.GetFileName(comic));
                var ext = new List<string> { ".jpg", ".png", ".gif" };
                var images = Directory.GetFiles(comic, "*.*", SearchOption.AllDirectories)
                     .Where(s => ext.Contains(Path.GetExtension(s)))
                     .Select(i =>
                     {
                         var chapter = Path.GetFileName(Path.GetDirectoryName(i));
                         var imageName = Path.GetFileName(i);
                         return Path.Combine("comic", chapter, imageName);
                     });
                if (ComicsPages.ContainsKey(vcomic))
                {
                    ComicsPages.Remove(vcomic);
                }
                ComicsPages.Add(vcomic, images.ToList());
            }
        }
    }
}
