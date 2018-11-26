using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CollatzCoreRazorPage.Models;

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

            //generate data
            IList<CollatzSequence> collatzSequences = new List<CollatzSequence>();
            for (int i = 1; i < 100; i++)
            {
                collatzSequences.Add(new CollatzSequence(i, int.Parse(evenExp), int.Parse(oddExp))); //TODO: parse error checking
            }

            //sort
            CurrentSort = sortOrder;
            TheNumSortParm = string.IsNullOrEmpty(sortOrder) ? "thenum_desc" : "";
            NumStepsSortParm = sortOrder == "numsteps" ? "numsteps_desc" : "numsteps";

            IOrderedEnumerable<CollatzSequence> orderedCollatzSequences;
            switch (sortOrder)
            {
                case "thenum_desc":
                    orderedCollatzSequences = collatzSequences.OrderByDescending(cs => cs.InitialValue);
                    break;
                case "numsteps":
                    orderedCollatzSequences = collatzSequences.OrderBy(cs => cs.TotalStoppingTime); //TODO: use property
                    break;
                case "numsteps_desc":
                    orderedCollatzSequences = collatzSequences.OrderByDescending(cs => cs.TotalStoppingTime);
                    break;
                default:
                    orderedCollatzSequences = collatzSequences.OrderBy(cs => cs.InitialValue);
                    break;
            }

            int pageSize = 10; //UNDONE: items per page
            int pageNumber = page ?? 1;

            IQueryable<CollatzSequence> orderedCollatzSequencesQuery = orderedCollatzSequences.AsQueryable();
            //return View(orderedCollatzSequencesQuery.ToPagedList(pageNumber, pageSize));
        }
    }
}
