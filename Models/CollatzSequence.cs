using System.Collections.Generic;
using System.Linq;

namespace CollatzCoreRazorPage.Models
{
    public class CollatzSequence
    {
        #region Properties
        public int InitialValue { get; private set; }
        public int? RepeatedValIndex { get; private set; }
        public bool TendsToInfinity { get; private set; }
        public int EvenExp { get; private set; }
        public int OddExp { get; private set; }
        public int? TotalStoppingTime
        {
            get
            {
                //https://stackoverflow.com/questions/2766932/why-cant-i-set-a-nullable-int-to-null-in-a-ternary-if-statement
                return (RepeatedValIndex == null && !TendsToInfinity) ? (int?)Steps.Count - 1 : null;
            }
        }
        public string StepsString
        {
            get
            {
                string s = "";
                foreach (int i in Steps.OrderBy(step => step.Value).Select(step => step.Key))
                {
                    s += i.ToString() + ", ";
                }
                return s;
            }
        }
        public string LegendString
        {
            get
            {
                string s = "";
                for (int i = 0; i < Steps.Count; i++)
                {
                    s += i.ToString() + ", ";
                }
                return s;
            }
        }
        public IDictionary<int, int> Steps { get; set; } //IDictionary<stepVal, stepIndex>
        #endregion
        public CollatzSequence(int initialValue, int evenExp, int oddExp)
        {
            //constructor parameters to properties
            InitialValue = initialValue;
            Steps = new Dictionary<int, int> { { initialValue, 0 } };
            EvenExp = evenExp;
            OddExp = oddExp;

            //containers
            int inValue = initialValue;
            int? nextValue = null;

            //generate step data
            while (TryGetNext(inValue, out nextValue))
            {
                Steps.Add((int)nextValue, Steps.Count);
                inValue = (int)nextValue;
            }
        }
        public long CalcNext(int inValue)
        {
            //calculate next value
            if (inValue % 2 == 0) //number is even
            {
                return inValue / EvenExp;
            }
            else
            {
                return inValue * OddExp + 1;
            }
        }
        private bool TryGetNext(int inValue, out int? nextValue)
        {
            //already at end state
            if (inValue == 1)
            {
                nextValue = null;
                return false;
            }
            else
            {
                //calculate as potentially valid next value
                long potentialNextValue = CalcNext(inValue);

                //tending to infinity
                if (potentialNextValue > int.MaxValue || potentialNextValue < int.MinValue)
                {
                    TendsToInfinity = true;
                    nextValue = null;
                    return false;
                }
                //repeating loop
                else if (Steps.ContainsKey((int)potentialNextValue))
                {
                    RepeatedValIndex = Steps[(int)potentialNextValue];
                    nextValue = null;
                    return false;
                }
                //valid
                else
                {
                    nextValue = (int)potentialNextValue;
                    return true;
                }
            }
        }
    }
}