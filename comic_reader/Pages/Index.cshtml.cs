using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using comic_reader.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace comic_reader.Pages
{
    public class IndexModel : PageModel
    {
        public List<(string url, string cover, bool isLocal)> Comics;

        public void OnGet()
        {
            this.Comics = new List<(string url, string cover, bool isLocal)>();
            foreach (KeyValuePair<string, ComicFolder> comic in ComicCollection.ComicsPages)
            {
                if (comic.Value.Count == 0)
                {
                    this.Comics.Add((comic.Key, string.Empty, comic.Value.IsLocal));
                    continue;
                }

                this.Comics.Add((comic.Key, comic.Value.ComicPages.First(), comic.Value.IsLocal));
            }
        }
    }
}