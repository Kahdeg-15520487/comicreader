using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace comic_reader.Pages
{
    public class IndexModel : PageModel
    {
        public List<(string url, string cover)> Comics;

        public void OnGet()
        {
            this.Comics = new List<(string url, string cover)>();
            foreach (KeyValuePair<string, List<string>> comic in ComicCollection.ComicsPages)
            {
                this.Comics.Add((comic.Key, comic.Value.First()));
            }
        }
    }
}