using Common.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for SessionDisplay.xaml
    /// </summary>
    public partial class SessionDisplay : UserControl
    {
        public static readonly DependencyProperty SessionViewModel =
            DependencyProperty.Register("Session", typeof(SessionViewModel), typeof(SessionDisplay));

        public SessionViewModel Session
        {
            get
            {
                return (SessionViewModel)GetValue(SessionViewModel);
            }
            set
            {
                SetValue(SessionViewModel, value);
            }
        }


        public SessionDisplay()
        {
            InitializeComponent();
        }
    }
}
