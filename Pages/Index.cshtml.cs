using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CollatzCoreRazorPage.Models;
using X.PagedList;

namespace CollatzCoreRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        //properties
        [BindProperty]
        public int EvenExp { get; set; }
        [BindProperty]
        public int OddExp { get; set; }
        [BindProperty]
        public string SortOrder { get; set; }
        [BindProperty]
        public int PageNum { get; set; }
        public IPagedList<CollatzSequence> Sequences { get; set; }

        //incoming request handlers
        public void OnGet()
        {
            //UNDONE: allow for actual expression input instead of ints only
            //UNDONE: use floats instead of ints for expression

            //set defaults
            EvenExp = 2;
            OddExp = 3;
            SortOrder = "InitValAsc";
            PageNum = 1;

            //generate data
            Sequences = GetSequences();
        }
        public void OnPost()
        {
            //generate data
            Sequences = GetSequences();
        }
        public void OnPostSortInitVal()
        {
            if (SortOrder == "InitValAsc")
            {
                SortOrder = "InitValDsc";
            }
            else
            {
                SortOrder = "InitValAsc";
            }
            //generate data
            Sequences = GetSequences();
        }
        public void OnPostSortStopTime()
        {
            SortOrder = SortOrder == "StopTimeAsc" ? "StopTimeDsc" : "StopTimeAsc";

            //generate data
            Sequences = GetSequences();
        }
        // public void OnGet(string sortOrder, string evenExp, string currentEvenExp, string oddExp, string currentOddExp, int? pageNumArg)
        // {
        //     //UNDONE: allow for actual expression input instead of ints only
        //     //UNDONE: use floats instead of ints for expression

        //     //set defaults
        //     EvenExp = 2;
        //     OddExp = 3;
        //     SortOrder = "InitValAsc";
        //     PageNum = 1;

        //     //generate data
        //     Sequences = GetSequences();
        // }
        IPagedList<CollatzSequence> GetSequences()
        {
            //generate data
            IList<CollatzSequence> collatzSequences = new List<CollatzSequence>();
            for (int i = 1; i < 100; i++)
            {
                collatzSequences.Add(new CollatzSequence(i, EvenExp, OddExp));
            }

            IOrderedEnumerable<CollatzSequence> orderedCollatzSequences;
            switch (SortOrder)
            {
                case "InitValDsc":
                    orderedCollatzSequences = collatzSequences.OrderByDescending(cs => cs.InitialValue);
                    break;
                case "StopTimeAsc":
                    orderedCollatzSequences = collatzSequences.OrderBy(cs => cs.TotalStoppingTime);
                    break;
                case "StopTimeDsc":
                    orderedCollatzSequences = collatzSequences.OrderByDescending(cs => cs.TotalStoppingTime);
                    break;
                case "InitValAsc":
                    orderedCollatzSequences = collatzSequences.OrderBy(cs => cs.InitialValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(SortOrder), SortOrder, "Can't order by that.");
            }

            int pageSize = 10; //UNDONE: items per page

            IQueryable<CollatzSequence> orderedCollatzSequencesQuery = orderedCollatzSequences.AsQueryable();
            return orderedCollatzSequencesQuery.ToPagedList(PageNum, pageSize);
        }
    }
}
