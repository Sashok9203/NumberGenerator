using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGenerator.Models
{
    internal interface IGenerator:IEnumerator<int>,IEnumerable<int>
    {
        void Reset(int? start, int? end);
    }
}
