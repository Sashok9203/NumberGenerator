using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;


namespace NumberGenerator.Models
{
    internal class NumberGenerator
    {
        private bool IsPause  = false;

        private Thread generatorThread;

        private Paragraph paragraph;

        private IGenerator generator { get; set; }

        private Brush brush;

        private bool buttonEnable => generatorThread.ThreadState != ThreadState.Stopped && generatorThread.ThreadState != ThreadState.Unstarted && !IsPause;

        private void generate()
        {
            foreach (var item in generator)
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        paragraph.Inlines.Add(new Bold(new Run($"{item} , ")) { Foreground = brush });
                    }));
                    if (!IsPause) Thread.Sleep(100);
                    else Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    if (!IsPause) return;
                    else
                    {
                        IsPause = false;
                        continue;
                    }
                }
            };
        }

        private void start(object o)
        {
            generatorThread = new(generate) { IsBackground = true };
            generator.Reset(StartPosition, EndPosition);
            generatorThread.Start();
        }

        private void stop(object o) => generatorThread?.Interrupt();

        private void pause(object o) => IsPause = true;

        private void restart(object o) => generator.Reset(StartPosition, EndPosition);

        public NumberGenerator(IGenerator generator, Paragraph paragraph, Brush brush)
        {
            this.generator = generator;
            this.paragraph = paragraph;
            this.brush = brush;
            generatorThread = new(generate);
        }
           
        public ThreadState ThreadState => generatorThread.ThreadState;

        public int? StartPosition { get; set; }

        public int? EndPosition { get; set; }

        public Brush Brush => brush;

        public RelayCommand Start => new((o) => start(o), (o) => generatorThread.ThreadState == ThreadState.Stopped || generatorThread.ThreadState == ThreadState.Unstarted);
        public RelayCommand Pause => new((o) => pause(o), (o) => buttonEnable);
        public RelayCommand Resume => new((o) => stop(o), (o) => IsPause);
        public RelayCommand Stop => new((o) => stop(o), (o) => buttonEnable);
        public RelayCommand Restart => new((o) => restart(o), (o) => buttonEnable);

    }
}
