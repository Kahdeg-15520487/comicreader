using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using comic_reader.ExtensionMethod;
using comic_reader.Model;

namespace comic_reader
{
    public static class ComicCollection
    {
        public static Dictionary<string, string> ComicsPaths { get; set; }

        public static Dictionary<string, ComicFolder> ComicsPages { get; set; }

        private static string LoadOther(string[] ext, string comic)
        {
            var chapterName = Path.GetFileName(comic);
            var parent = comic.Substring("comic", chapterName);
            if (parent.StartsWith("\\"))
            {
                parent = parent.Remove(0, 1);
            }
            var virtualPath = Path.Combine("comic", parent, chapterName);
            var images = Directory.GetFiles(comic, "*.*", SearchOption.TopDirectoryOnly)
                 .Where(s => ext.Contains(Path.GetExtension(s)))
                 .Select(i =>
                 {
                     var imageName = Path.GetFileName(i);
                     return Path.Combine("static", virtualPath, imageName);
                 });

            //prioritize remote path
            if (ComicsPages.ContainsKey(virtualPath))
            {
                ComicsPages.Remove(virtualPath);
            }

            ComicFolder comicFolder = new ComicFolder(virtualPath, images, parent, false);

            ComicsPages.Add(virtualPath, comicFolder);
            return virtualPath;
        }

        private static string LoadLocal(string[] ext, string comic)
        {
            var images = Directory.GetFiles(comic, "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)))
                 .Select(i => Path.Combine("static", i));
            ComicFolder comicFolder = new ComicFolder(comic, images, "");
            ComicsPages.Add(comic, comicFolder);

            return comic;
        }

        public static void Init(string[] dirs, string[] ext)
        {
            var localDirs = Directory.GetDirectories("comic", "*", SearchOption.AllDirectories).ToList();

            var otherDirs = dirs.SelectMany(d => Directory.GetDirectories(d, "*", SearchOption.AllDirectories)).ToList();

            ComicsPaths = new Dictionary<string, string>();
            foreach (string localDir in localDirs)
            {
                if (ComicsPaths.ContainsKey(localDir))
                {
                    continue;
                }

                ComicsPaths.Add(localDir, localDir);
            }
            foreach (string otherDir in otherDirs)
            {
                if (ComicsPaths.ContainsKey(otherDir))
                {
                    continue;
                }

                var chapterName = Path.GetFileName(otherDir);
                var parent = otherDir.Substring("comic", chapterName);
                if (parent.StartsWith("\\"))
                {
                    parent = parent.Remove(0, 1);
                }
                var virtualPath = Path.Combine("comic", parent, chapterName);

                ComicsPaths.Add(otherDir, virtualPath);
            }

            ComicsPages = new Dictionary<string, ComicFolder>();
            foreach (var comic in localDirs)
            {
                Console.WriteLine("loading {0}", comic);
                LoadLocal(ext, comic);
                Console.WriteLine("loaded {0} pages", ComicsPages[comic].Count);
            }
            Console.WriteLine();
            foreach (var comic in otherDirs)
            {
                Console.WriteLine("loading {0}", comic);
                LoadOther(ext, comic);
                Console.WriteLine("loaded {0} pages", ComicsPages[ComicsPaths[comic]].Count);
            }
            Console.WriteLine("loaded {0} comic", ComicsPaths.Count);
        }
    }
}
