using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollatzCoreRazorPage.Pages
{
    public class IndexModel : PageModel
    {
    public string CurrentEvenExp { get; set; }
        public void OnGet(string sortOrder, string evenExp, string currentEvenExp, string oddExp, string currentOddExp, int? page)
        {
            CurrentEvenExp = currentEvenExp;
        }
    }
}
