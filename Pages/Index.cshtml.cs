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
        [BindProperty(SupportsGet = true)]
        public int EvenExp { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OddExp { get; set; }
        [BindProperty(SupportsGet = true)]
        public SortOrders SortOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageNum { get; set; }
        public IPagedList<CollatzSequence> Sequences { get; set; }
        public enum SortOrders { InitValAsc, InitValDsc, StopTimeAsc, StopTimeDsc };

        //hard coded constants
        //UNDONE: allow these to be adjusted via the UI
        const int pageSize = 10;
        const int numSequencestoGenerate = 100;

        //incoming request handlers
        public void OnGet()
        {
            //set defaults
            EvenExp = 2;
            OddExp = 3;
            SortOrder = SortOrders.InitValAsc;
            PageNum = 1;

            //generate data
            Sequences = GetSequences();
        }
        public void OnGetPageNum()
        {
            //model binding should capture all incoming parms via query string parms

            //generate data
            Sequences = GetSequences();
        }
        public void OnPost()
        {
            //currently, the button that calls this handler is the "conject" button.
            //it would generally be pressed when the evenexp/oddexp are changed.
            //generate data
            Sequences = GetSequences();
        }
        public void OnPostSortInitVal()
        {
            //adjust sort order
            SortOrder = SortOrder == SortOrders.InitValAsc ? SortOrders.InitValDsc : SortOrders.InitValAsc;
            //generate data
            Sequences = GetSequences();
        }
        public void OnPostSortStopTime()
        {
            //adjust sort order
            SortOrder = SortOrder == SortOrders.StopTimeAsc ? SortOrders.StopTimeDsc : SortOrders.StopTimeAsc;
            //generate data
            Sequences = GetSequences();
        }
        IPagedList<CollatzSequence> GetSequences()
        {
            //generate data
            IList<CollatzSequence> collatzSequences = new List<CollatzSequence>();
            for (int i = 1; i < numSequencestoGenerate; i++)
            {
                collatzSequences.Add(new CollatzSequence(i, EvenExp, OddExp));
            }

            //sort data
            IOrderedEnumerable<CollatzSequence> orderedCollatzSequences;
            switch (SortOrder)
            {
                case SortOrders.InitValAsc:
                    orderedCollatzSequences = collatzSequences.OrderBy(cs => cs.InitialValue);
                    break;
                case SortOrders.InitValDsc:
                    orderedCollatzSequences = collatzSequences.OrderByDescending(cs => cs.InitialValue);
                    break;
                case SortOrders.StopTimeAsc:
                    orderedCollatzSequences = collatzSequences.OrderBy(cs => cs.TotalStoppingTime);
                    break;
                case SortOrders.StopTimeDsc:
                    orderedCollatzSequences = collatzSequences.OrderByDescending(cs => cs.TotalStoppingTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(SortOrder), SortOrder, "Can't order by that.");
            }

            //convert to proper data type
            //using the https://github.com/troygoode/PagedList library (no longer maintained), i got runtime crashes
            //if I didn't explicitly convert to IQueryable
            //IQueryable<CollatzSequence> orderedCollatzSequencesQuery = orderedCollatzSequences.AsQueryable();
            //return orderedCollatzSequencesQuery.ToPagedList(PageNum, pageSize);
            //that doesn't seem to be an issue for https://github.com/dncuug/X.PagedList
            return orderedCollatzSequences.ToPagedList(PageNum, pageSize);
        }
    }
}
