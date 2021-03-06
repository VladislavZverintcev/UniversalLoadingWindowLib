using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace UniversalLoadingWindowLib
{
    public class LoadingWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        string title;
        string annotation;
        byte percentsProgress = 0;
        //Brushes and colors
        Brush mainBrush;
        SolidColorBrush foreground_title;
        SolidColorBrush foreground_annotation;
        SolidColorBrush elementBrush;
        SolidColorBrush elementBrush6;
        SolidColorBrush elementBrush5;
        SolidColorBrush elementBrush4;
        SolidColorBrush elementBrush3;
        SolidColorBrush elementBrush2;
        SolidColorBrush elementBrush1;
        SolidColorBrush elementBrush0;
        //
        //CurrentThread
        Thread ownerThread;
        private System.Timers.Timer animValueTimer;
        #endregion Fields

        #region Properties
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }
        public string Annotation
        {
            get { return annotation; }
            set
            {
                annotation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Annotation"));
            }
        }
        public byte PercentsProgress
        {
            get { return percentsProgress; }
            set
            {
                if(value >= 0 && value <= 100)
                {
                    percentsProgress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PercentsProgress"));
                }
            }
        }
        public Brush MainBrush
        {
            get { return mainBrush; }
            set
            {
                mainBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MainBrush"));
            }
        }
        public SolidColorBrush Foreground_Title
        {
            get { return foreground_title; }
            set
            {
                foreground_title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foreground_Title"));
            }
        }
        public SolidColorBrush Foreground_Annotation
        {
            get { return foreground_annotation; }
            set
            {
                foreground_annotation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foreground_Annotation"));
            }
        }
        public SolidColorBrush ElementBrush 
        { 
            get { return elementBrush; } 
            set { elementBrush = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush")); }
        }
        public SolidColorBrush ElementBrush6
        {
            get { return elementBrush6; }
            set
            {
                elementBrush6 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush6"));
            }
        }
        public SolidColorBrush ElementBrush5
        {
            get { return elementBrush5; }
            set
            {
                elementBrush5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush5"));
            }
        }
        public SolidColorBrush ElementBrush4
        {
            get { return elementBrush4; }
            set
            {
                elementBrush4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush4"));
            }
        }
        public SolidColorBrush ElementBrush3
        {
            get { return elementBrush3; }
            set
            {
                elementBrush3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush3"));
            }
        }
        public SolidColorBrush ElementBrush2
        {
            get { return elementBrush2; }
            set
            {
                elementBrush2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush2"));
            }
        }
        public SolidColorBrush ElementBrush1
        {
            get { return elementBrush1; }
            set
            {
                elementBrush1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush1"));
            }
        }
        public SolidColorBrush ElementBrush0
        {
            get { return elementBrush0; }
            set
            {
                elementBrush0 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ElementBrush0"));
            }
        }
        public LinearGradientBrush BrushOfLine { get; set; }
        #endregion Properties

        #region Constructors
        public LoadingWindowViewModel(string _title, string _annotation, 
            Brush _mainBrush, Color _elementColor, Color _foregroundTitle, 
            Color _foregroundAnnotation)
        {
            if(_mainBrush != null && _elementColor != null && _title != null && _annotation != null && _foregroundTitle != null)
            {
                Title = _title;
                Annotation = _annotation;
                MainBrush = _mainBrush;
                Foreground_Title = new SolidColorBrush(_foregroundTitle);
                Foreground_Annotation = new SolidColorBrush(_foregroundAnnotation);
                BrushOfLine = new LinearGradientBrush(Foreground_Title.Color, new Color { A = 0, R = 0, G = 0, B = 0 }, 0);
                ElementBrush = new SolidColorBrush(new Color { A = 255, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush6 = new SolidColorBrush(new Color { A = 223, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush5 = new SolidColorBrush(new Color { A = 191, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush4 = new SolidColorBrush(new Color { A = 159, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush3 = new SolidColorBrush(new Color { A = 128, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush2 = new SolidColorBrush(new Color { A = 97, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush1 = new SolidColorBrush(new Color { A = 66, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ElementBrush0 = new SolidColorBrush(new Color { A = 35, R = _elementColor.R, G = _elementColor.G, B = _elementColor.B });
                ownerThread = Thread.CurrentThread;
            }
            else
            {
                Title = "Title";
                Annotation = "Annotation";
                SetDefaultColors();
            }
            SetAnimValueTimer();
        }
        public LoadingWindowViewModel()
        {
            Title = "Title";
            Annotation = "Annotation";
            SetDefaultColors();
            SetAnimValueTimer();
        }
        #endregion Constructors

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                Dispatcher.FromThread(ownerThread)?.Invoke(() => RefreshAnimValues());
            }
            catch(System.Threading.Tasks.TaskCanceledException)
            { return; }
        }
        #endregion Events

        #region Methods
        private void SetDefaultColors()
        {
            Color defaultElementColor = new Color { A = 255, R = 128, G = 128, B = 128 };
            MainBrush = new SolidColorBrush(new Color { A = 255, R = 100, G = 100, B = 100 });
            Foreground_Title = new SolidColorBrush(new Color { A = 255, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            Foreground_Annotation =new SolidColorBrush(new Color { A = 255, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            BrushOfLine = new LinearGradientBrush(Foreground_Title.Color, new Color { A = 0, R = 0, G = 0, B = 0 }, 0);
            ElementBrush = new SolidColorBrush(new Color { A = 255, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush6 = new SolidColorBrush(new Color { A = 223, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush5 = new SolidColorBrush(new Color { A = 191, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush4 = new SolidColorBrush(new Color { A = 159, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush3 = new SolidColorBrush(new Color { A = 128, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush2 = new SolidColorBrush(new Color { A = 97, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush1 = new SolidColorBrush(new Color { A = 66, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ElementBrush0 = new SolidColorBrush(new Color { A = 35, R = defaultElementColor.R, G = defaultElementColor.G, B = defaultElementColor.B });
            ownerThread = Thread.CurrentThread;
        }
        private SolidColorBrush GetShiftColorBrush(SolidColorBrush oldBrush)
        {
            if(oldBrush != null && oldBrush is SolidColorBrush)
            {
                Color newColor = oldBrush.Color;
                if (oldBrush.Color.A == 255) { newColor.A = 223; }
                if (oldBrush.Color.A == 223) { newColor.A = 191; }
                if (oldBrush.Color.A == 191) { newColor.A = 159; }
                if (oldBrush.Color.A == 159) { newColor.A = 128; }
                if (oldBrush.Color.A == 128) { newColor.A = 97; }
                if (oldBrush.Color.A == 97) { newColor.A = 66; }
                if (oldBrush.Color.A == 66) { newColor.A = 35; }
                if (oldBrush.Color.A == 35) { newColor.A = 255; }
                return new SolidColorBrush(newColor);

            }
            else { return null; }
        }
        private void SetAnimValueTimer()
        {
            animValueTimer = new System.Timers.Timer(80);
            animValueTimer.Elapsed += OnTimedEvent;
            animValueTimer.Enabled = true;
        }
        private void RefreshAnimValues()
        {
            ElementBrush = GetShiftColorBrush(ElementBrush);
            ElementBrush0 = GetShiftColorBrush(ElementBrush0);
            ElementBrush1 = GetShiftColorBrush(ElementBrush1);
            ElementBrush2 = GetShiftColorBrush(ElementBrush2);
            ElementBrush3 = GetShiftColorBrush(ElementBrush3);
            ElementBrush4 = GetShiftColorBrush(ElementBrush4);
            ElementBrush5 = GetShiftColorBrush(ElementBrush5);
            ElementBrush6 = GetShiftColorBrush(ElementBrush6);
        }
        public void StopAnimation()
        {
            animValueTimer?.Stop();
            animValueTimer?.Close();
            animValueTimer?.Dispose();
            animValueTimer = null;
        }
        #endregion Methods
    }
}
