using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo.ViewModel.MessageViewModel
{
    public class MessageViewModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MessageViewModel()
        {

        }

        public NotificationModel CurrentMessage { get; set; }

        //#region Commands
        //private ICommand _messagePressedCommand;

        //public ICommand MesssagePressedCommand =>
        //    _messagePressedCommand ?? (_messagePressedCommand = new DelegateCommand(MessageExecute));

        //#endregion

        //#region Executes & CanExecutes
        //private void MessageExecute()
        //{
        //    //Send event
        //}
        //#endregion
    }
}
