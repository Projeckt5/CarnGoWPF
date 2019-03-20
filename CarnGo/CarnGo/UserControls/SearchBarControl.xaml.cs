﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    /// <summary>
    /// Interaction logic for SearchBarControl.xaml
    /// </summary>
    public partial class SearchBarControl : UserControl
    {
        public SearchBarControl()
        {
            InitializeComponent();
        }

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty,value);
        }
        public static DependencyProperty SearchCommandProperty = DependencyProperty.Register(nameof(SearchCommand),typeof(ICommand),typeof(SearchBarControl),new PropertyMetadata(null));
    }
}
