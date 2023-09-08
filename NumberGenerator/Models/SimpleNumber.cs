using System;
using System.Collections;
using System.Collections.Generic;

namespace NumberGenerator.Models
{
    internal class SimpleNumber : IGenerator
    {
        private ulong _current;

        private ulong? _start;

        private ulong? _currentStart;

        private ulong? _end;

        object IEnumerator.Current => _current;

        public ulong Current => _current;

        public SimpleNumber(ulong? start = null, ulong? end = null) => Reset(start, end);
        
        public void Dispose() {}

        public bool MoveNext()
        {
            while (_end == null || _currentStart <= _end)
            {
                ulong range = (ulong)Math.Sqrt((double)_currentStart);
                bool isSimpl = true;
                for (ulong i = 2; i <= range; i++)
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
       
        public void Reset(ulong? start, ulong? end)
        {
            _start = start == null || _start < 2 ? 2 : start;
            _end = end < 0 ? 0 : end;
            Reset();
        }

        public IEnumerator<ulong> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
    }
}
