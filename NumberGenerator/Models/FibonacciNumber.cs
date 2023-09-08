using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace NumberGenerator.Models
{
    internal class FibonacciNumber : IGenerator
    {
        private ulong _current;

        private ulong? _startIndex;

        private ulong _currentIndex;

        private ulong? _endIndex;
        
        private ulong _preview;
        private ulong _last;

        public FibonacciNumber(ulong? start = null, ulong? end = null) => Reset(start, end);

        object IEnumerator.Current => _current;

        public ulong Current => _current;

        public void Dispose() {}

        public bool MoveNext()
        {
            if(_endIndex != null && _currentIndex == _endIndex) return false;
            if (_currentIndex == 1)
            {
                _current = 1;
                _preview = 1;
               _last = 0;
            }
            else
            {
                _current = _preview + _last;
                _last = _preview;
                _preview = _current;
            }
            _currentIndex++;
            return true;
        }
        public void Reset()
        {
            _current = _preview = _last = _currentIndex = 0;
           for (ulong i = 0; i < _startIndex; i++)
               MoveNext();
        }


        public IEnumerator<ulong> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset(ulong? start, ulong? end)
        {
            _startIndex = start == null || _startIndex < 0 ? 0 : start;
            _endIndex = end < 0 ? 0 : end;
            Reset();
        }
    }
}
