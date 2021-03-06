﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using CarnGo.View;

namespace CarnGo
{
    public class ApplicationPageValueConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO: Move this to a PageFactory
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.LoginPage:
                    return new LoginView();
                case ApplicationPage.EditUserPage:
                    return new EditUser();
                case ApplicationPage.CarLeasePage:
                    return new CarLeaseView();
                case ApplicationPage.SearchPage:
                    return new SearchView();
                case ApplicationPage.UserSignUpPage:
                    return  new UserSignUp();
                case ApplicationPage.StartPage:
                    return new StartPage();
                case ApplicationPage.SendRequestPage:
                    return new SendRequestView();
                case ApplicationPage.MessageView:
                    return new MessageView();
                default:
                    throw new ArgumentException("The value to convert was not an ApplicationPage");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ApplicationPageValueConverter();
        }
    }
}
