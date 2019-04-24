using System.Collections.Generic;
using System.Linq;

namespace comic_reader.Model
{
    public class ComicFolder
    {
        public ComicFolder(string path, IEnumerable<string> comicPages, string parent, bool isLocal = true)
        {
            this.Path = path;
            this.ComicPages = comicPages.ToArray();
            this.IsContainComic = this.ComicPages.Length != 0;
            this.Parent = parent;
        }

        public string Parent { get; private set; } = null;
        public string Path { get; }
        public bool IsContainComic { get; }
        public bool IsLocal { get; }
        public string[] ComicPages { get; }

        public int Count => this.ComicPages.Length;

        public string this[int index] => this.ComicPages[index];
    }
}
