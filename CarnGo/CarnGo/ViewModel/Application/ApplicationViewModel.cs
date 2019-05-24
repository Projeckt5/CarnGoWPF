﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Model.ThreadTimer;
using CarnGo.Security;
using Prism.Events;
using Unity;

namespace CarnGo
{
    public class StartThreadTimer : PubSubEvent<bool> { }
    public class NotificationMessagesUpdateEvent: PubSubEvent<List<MessageModel>>{ }
    public class UserUpdateEvent: PubSubEvent<UserModel>{ }
    public class NewUserDataReadyEvent : PubSubEvent { }

    public class ApplicationViewModel : BaseViewModel, IApplication
    {
        private readonly IEventAggregator _eventAggregator;
        private ApplicationPage _applicationPage;
        

        private UserModel _currentUser = null;

        public ApplicationViewModel(IEventAggregator eventAggregator)
        {
            _applicationPage = ApplicationPage.LoginPage;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NewUserDataReadyEvent>()
                .Subscribe(()=>OnPropertyChanged(nameof(CurrentUser)));
        }

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage
        {
            get => _applicationPage;
            private set
            {
                if (_applicationPage == value)
                    return;
                _applicationPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                OnPropertyChanged(nameof(ShowHeaderBar));
            }
        }

        public bool ShowHeaderBar => CurrentPage != ApplicationPage.LoginPage && CurrentPage != ApplicationPage.UserSignUpPage;

        /// <summary>
        /// The current user logged into the application
        /// </summary>
        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser == value)
                    return;
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                _eventAggregator.GetEvent<UserUpdateEvent>().Publish(_currentUser);
            }
        }

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }
    }
}

