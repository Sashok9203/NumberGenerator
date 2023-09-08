using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace NumberGenerator.Models
{
    internal class WindowModel
    {
        public NumberGenerator SimpleNumberGenerator { get; set; }

        public NumberGenerator FibonacciNumberGenerator { get; set; }

        public WindowModel(Paragraph paragraph)
        {
            SimpleNumberGenerator = new(new SimpleNumber(), paragraph,Brushes.Green);
            FibonacciNumberGenerator = new(new FibonacciNumber(), paragraph, Brushes.Red);
        }
    }
}
