using MonsterHotel.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.UnitTest
{
    public class SweepingRandomNumberGenerator : IRandomNumberGenerator
    {
        private class Selection
        {
            public int Selected { get; set; }
            public int Max { get; set; }
        };

        private List<Selection> _selections = new List<Selection>();
        private int _selectionIndex = -1;
        private bool _started = false;

        public bool Next()
        {
            if (!_started)
            {
                _started = true;
                return true;
            }

            var lastSelection = _selections.LastOrDefault();
            while (lastSelection != null)
            {
                lastSelection.Selected++;
                if (lastSelection.Selected < lastSelection.Max)
                    break;

                _selections.Remove(lastSelection);
                lastSelection = _selections.LastOrDefault();
            }

            _selectionIndex = -1;
            return lastSelection != null;
        }

        public int GetNumber(int maxNumber)
        {
            if (!_started)
                throw new InvalidOperationException("You must run the sweeping random number generator in a loop to capture all possible combinations.");

            _selectionIndex++;
            if (_selections.Count <= _selectionIndex)
            {
                var selection = new Selection { Selected = 0, Max = maxNumber };
                _selections.Add(selection);
                return selection.Selected;
            }
            else
            {
                var selection = _selections[_selectionIndex];
                if (maxNumber != selection.Max)
                    throw new ArgumentException(String.Format("The max was different last time. Was {0}, now {1}.",
                        selection.Max, maxNumber));
                return selection.Selected;
            }
        }
    }
}
