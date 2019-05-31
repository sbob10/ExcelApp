using System;
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
using System.Windows.Shapes;

namespace TourenVerwaltung
{
    /// <summary>
    /// Interaktionslogik für AddFahrerWindow.xaml
    /// </summary>
    public partial class AddFahrerWindow : Window
    {
        FahrerWindowsViewModel vm;
        Action<Fahrer> callbackFromParentWindow;

        public AddFahrerWindow(Action<Fahrer> addingAction)
        {
            InitializeComponent();
            vm = (FahrerWindowsViewModel)this.DataContext;
            vm.CloseDialogAddFahrerFunc = new Func<int, string>(ShowAddFahrerDialogFunc);

            callbackFromParentWindow = addingAction;
        }

        private void PassFahrerToParentWindowCallback(object sender, EventArgs e)
        {
            callbackFromParentWindow(vm.AddFahrerValue);
        }

        private String ShowAddFahrerDialogFunc(int statusCode)
        {
            if (statusCode == 1)
            {
                Closed += PassFahrerToParentWindowCallback;
            }
            this.Close();

            return ""; //just pseudo
        }

    }
}
