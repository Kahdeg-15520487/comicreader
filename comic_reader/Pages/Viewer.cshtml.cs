using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace comic_reader.Pages
{
    public class ViewerModel : PageModel
    {
        public string Chapter;
        public string CurrentPage;
        public string NextPage;
        public string PreviousPage;
        public void OnGet()
        {
            this.Chapter = this.HttpContext.Request.Query["chapter"];
            int.TryParse(this.HttpContext.Request.Query["curr"], out var currPage);
            var prevPage = currPage - 1;
            var nextPage = currPage + 1;

            var chapter = ComicCollection.ComicsPages[this.Chapter];
            {
                var imageUrl = $"\\static\\{chapter[currPage]}\"".Replace('\\', '/');
                this.CurrentPage = imageUrl;
            }
            if (currPage == 0)
            {
                prevPage = -1;
            }
            if (currPage == chapter.Count - 1)
            {
                nextPage = -1;
            }

            if (prevPage != -1)
            {
                this.PreviousPage = $"/viewer?chapter={this.Chapter}&curr={prevPage}";
            }
            else
            {
                this.PreviousPage = "";
            }

            if (nextPage != -1)
            {
                this.NextPage = $"/viewer?chapter={this.Chapter}&curr={nextPage}";
            }
            else
            {
                this.NextPage = "";
            }
        }
    }
}