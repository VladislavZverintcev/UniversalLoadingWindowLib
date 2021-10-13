using System.Windows.Media;

namespace UniversalLoadingWindowLib
{
    /// <summary>
    /// Class for build Universal Loading Window with Title, Annotation, 
    /// animated bubbles, and percents of Progress (optional). Height="160" Width="400"
    /// </summary>
    public class UniversalLoadingWindow
    {
        #region Fields
        private LoadingWindowViewModel viewModel = new LoadingWindowViewModel();
        WindowLoading wl;
        #endregion Fields
        #region Properties
        private Brush MainBrush { get; set; }
        private Color ElementColor { get; set; }
        public LoadingWindowViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        #endregion Properties
        #region Constructors
        /// <summary>
        /// Default Constructor with default gray colors of LoadingWindow
        /// </summary>
        public UniversalLoadingWindow()
        {

        }
        /// <summary>
        /// Constructor with configurated LoadingWindow Parameters
        /// </summary>
        /// <param name="_title"> Title</param>
        /// <param name="_annotation"> Annotation</param>
        /// <param name="_mainBrush"> Brush of main background</param>
        /// <param name="_elementColor"> Color of animated bubbles</param>
        /// <param name="_foregroundTitle">Title's font color (and color of line and percents)</param>
        /// <param name="_foregroundAnnotation"> Annotation's font color</param>
        public UniversalLoadingWindow(string _title, string _annotation, Brush _mainBrush, 
            Color _elementColor, Color _foregroundTitle, Color _foregroundAnnotation)
        {
            MainBrush = _mainBrush;
            ElementColor = _elementColor;
            ViewModel = new LoadingWindowViewModel(_title, _annotation, MainBrush, ElementColor, _foregroundTitle, _foregroundAnnotation);
        }
        #endregion Constructors
        #region Methods
        /// <summary>
        /// Show Loading Window
        /// </summary>
        public void Show()
        {
            wl = new WindowLoading();
            wl.DataContext = this;
            wl.Show();
        }
        /// <summary>
        /// Show Loading Window (Modal)
        /// </summary>
        public void ShowDialog()
        {
            if (ViewModel != null)
            {
                wl = new WindowLoading();
                wl.DataContext = this;
                wl.ShowDialog();
            }
        }
        /// <summary>
        /// Close Loading Window
        /// </summary>
        public void Close()
        {
            if (wl != null)
            {
                ViewModel.StopAnimation();
                ViewModel = null;
                wl.Close();
            }
        }
        /// <summary>
        /// Set Title of Loading window. This is show in the top of window.
        /// </summary>
        /// <param name="newTitle"> If not used set Empty.String </param>
        public void SetTitle(string newTitle)
        {
            if (ViewModel != null)
            {
                ViewModel.Title = newTitle;
            }
        }
        /// <summary>
        /// Set Annotation of Loading.
        /// </summary>
        /// <param name="newAnnotation">  if not used set Empty.String </param>
        public void SetAnnotation(string newAnnotation)
        {
            if (ViewModel != null)
            {
                ViewModel.Annotation = newAnnotation;
            }
        }
        /// <summary>
        /// Set Percents of Progress, this is shows inside animated bubbles, default value "0". When value "0" percents not shown
        /// </summary>
        /// <param name="newPercent"> byte value from 0 to 100 </param>
        public void SetPercentProgress(byte newPercent)
        {
            if (ViewModel != null)
            {
                ViewModel.PercentsProgress = newPercent;
            }
        }
        #endregion Methods
    }
}
