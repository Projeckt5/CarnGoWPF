namespace CarnGo
{
    public class ViewModelLocator
    {
        #region Constructors
        private ViewModelLocator() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Singleton instance of ViewModelLocator
        /// </summary>
        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        /// <summary>
        /// Singleton instance of ViewModelLocator
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel { get; set; } = new ApplicationViewModel();
        #endregion
    }
}
