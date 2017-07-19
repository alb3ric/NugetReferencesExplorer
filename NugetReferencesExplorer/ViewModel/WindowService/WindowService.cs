using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NugetReferencesExplorer.ViewModel
{

    class WindowService : IWindowService
    {
        private Color getAccentColor(Window win)
        {
            return (Color)ThemeManager.GetResourceFromAppStyle(win, "AccentColor");
        }

        public bool? ShowDialogWindow(object viewModel)
        {
            var win = new MetroWindow();

            win.Owner = Application.Current.MainWindow;
            win.Content = viewModel;
            win.BorderThickness = new Thickness(1);
            win.GlowBrush = new SolidColorBrush(this.getAccentColor(win));
            win.WindowStyle = WindowStyle.ToolWindow;
            win.SizeToContent = SizeToContent.WidthAndHeight;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowMaxRestoreButton = false;
            win.ShowMinButton = false;
            return win.ShowDialog();
        }
    }
}
