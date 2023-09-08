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
        private Paragraph paragraph;

        public NumberGenerator SimpleNumberGenerator { get; set; }

        public NumberGenerator FibonacciNumberGenerator { get; set; }

        public RelayCommand Clear => new((o) => paragraph.Inlines.Clear(), (o) => paragraph.Inlines.Count!=0);

        public WindowModel(Paragraph paragraph)
        {
            this.paragraph = paragraph;
            SimpleNumberGenerator = new(new SimpleNumber(), paragraph,Brushes.Green);
            FibonacciNumberGenerator = new(new FibonacciNumber(), paragraph, Brushes.Red);
        }
    }
}
