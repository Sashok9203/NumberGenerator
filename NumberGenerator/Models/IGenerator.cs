using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGenerator.Models
{
    internal interface IGenerator:IEnumerator<ulong>,IEnumerable<ulong>
    {
        void Reset(ulong? start, ulong? end);
    }
}
