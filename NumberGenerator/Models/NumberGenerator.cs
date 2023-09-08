using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace NumberGenerator.Models
{
    internal class NumberGenerator
    {
        private bool isPause = false;
        private bool IsPause
        {
            get => isPause;
            set
            {
                isPause = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }
        private bool isThreadStoped = true;
        private bool IsThreadStoped
        {
            get => isThreadStoped;
            set 
            {
                isThreadStoped = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private Thread generatorThread;

        private Paragraph paragraph;

        private IGenerator generator { get; set; }

        private Brush brush;

        private bool buttonEnable => !IsThreadStoped && !IsPause;

        private void generate()
        {
            foreach (var item in generator)
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        paragraph.Inlines.Add(new Bold(new Run($"{item}   ")) { Foreground = brush });
                    }));
                    if (!IsPause) Thread.Sleep(100);
                    else Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    
                    if (!IsPause) break;
                    else
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                        {
                            IsPause = false;
                        }));
                        continue;
                    }
                }
            }
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                IsThreadStoped = true;
            }));
        }

        private void start(object o)
        {
            generatorThread = new(generate) { IsBackground = true };
            generator.Reset(StartPosition, EndPosition);
            IsThreadStoped = false;
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
           
        public ulong? StartPosition { get; set; }

        public ulong? EndPosition { get; set; }

        public Brush Brush => brush;

        public RelayCommand Start => new((o) => start(o), (o) => !IsPause && IsThreadStoped);
        public RelayCommand Pause => new((o) => pause(o), (o) => buttonEnable);
        public RelayCommand Resume => new((o) => stop(o), (o) => IsPause && !IsThreadStoped);
        public RelayCommand Stop => new((o) => stop(o), (o) => buttonEnable );
        public RelayCommand Restart => new((o) => restart(o), (o) => buttonEnable);

    }
}
