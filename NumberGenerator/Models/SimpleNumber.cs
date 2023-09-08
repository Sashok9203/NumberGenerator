using System;
using System.Collections;
using System.Collections.Generic;

namespace NumberGenerator.Models
{
    internal class SimpleNumber : IGenerator
    {
        private int _current;

        private int? _start;

        private int? _currentStart;

        private int? _end;

        object IEnumerator.Current => _current;

        public int Current => _current;

        public SimpleNumber(int? start = null, int? end = null) => Reset(start, end);
        
        public void Dispose() {}

        public bool MoveNext()
        {
            while (_end == null || _currentStart <= _end)
            {
                int range = (int)Math.Sqrt((double)_currentStart);
                bool isSimpl = true;
                for (int i = 2; i <= range; i++)
                {
                    if (_currentStart % i == 0)
                    {
                        isSimpl = false;
                        break;
                    }
                }
                if (isSimpl)
                {
                    _current = _currentStart.Value;
                    _currentStart++;
                    return true;
                }
                _currentStart++;
            }
            return false;
        }

        public void Reset() => _currentStart = _start;
       
        public void Reset(int? start,int? end)
        {
            _start = start == null || _start < 2 ? 2 : start;
            _end = end;
            Reset();
        }

        public IEnumerator<int> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
    }
}
