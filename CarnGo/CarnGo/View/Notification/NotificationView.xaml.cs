﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Events;

namespace CarnGo
{
    /// <summary>
    /// Interaction logic for NotifikationView.xaml
    /// </summary>
    public partial class NotificationView  : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(IoCContainer.Resolve<IApplication>(), IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IQueryDatabase>());
        }
    }
}
