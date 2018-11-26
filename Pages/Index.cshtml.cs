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
        public string CurrentOddExp { get; set; }
        public string CurrentSort { get; set; }
        public string TheNumSortParm { get; set; }
        public string NumStepsSortParm { get; set; }
        public void OnGet(string sortOrder, string evenExp, string currentEvenExp, string oddExp, string currentOddExp, int? page)
        {
            //UNDONE: allow for actual expression input instead of ints only
            //UNDONE: use floats instead of ints for expression

            //get expressions
            if (evenExp != null)
            {
                page = 1;
            }
            else
            {
                evenExp = currentEvenExp ?? "2";
            }
            CurrentEvenExp = evenExp;

            if (oddExp != null)
            {
                page = 1;
            }
            else
            {
                oddExp = currentOddExp ?? "3";
            }
            CurrentOddExp = oddExp;
        }
    }
}
