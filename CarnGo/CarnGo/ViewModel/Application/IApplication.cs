using System.Security;
using System.Threading.Tasks;

namespace CarnGo
{
    public interface IApplication
    {
        /// <summary>
        /// The current page of the application
        /// </summary>
        ApplicationPage CurrentPage { get; }

        bool ShowHeaderBar { get; }

        /// <summary>
        /// The current user logged into the application
        /// </summary>
        UserModel CurrentUser { get; }

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        void GoToPage(ApplicationPage page);

        Task LogUserIn(string email, SecureString password);
        void LogUserOut();
    }
}