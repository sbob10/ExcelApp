using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourenVerwaltung
{
    class FahrerWindowsViewModel : ViewModelBase
    {

        #region Properties

        private Fahrer _AddFahrerValue;

        public Fahrer AddFahrerValue
        {
            get { return _AddFahrerValue; }
            set { SetProperty(ref _AddFahrerValue, value, () => AddFahrerValue); }
        }

        private Fahrer _EditFahrerValue;

        public Fahrer EditFahrerValue
        {
            get { return _EditFahrerValue; }
            set { SetProperty(ref _EditFahrerValue, value, () => EditFahrerValue); }
        }
      
        #endregion Properties

        #region Commands&Services

        public Func<int, string> CloseDialogAddFahrerFunc { get; set; }
        public Func<int, string> CloseDialogEditFahrerFunc { get; set; }

        public ICommand AddFahrerCommand { get; set; }
        public ICommand CloseDialogAddFahrerCommand { get; set; }

        public ICommand EditFahrerCommand { get; set; }
        public ICommand CloseDialogEditFahrerCommand { get; set; }

        public IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } set { } }
        public IServiceContainer IServiceContainer { get; set; }

        #endregion Commands

        #region Initialisations

        public FahrerWindowsViewModel()
        {
            InitCommandsAndServices();
            InitValuesAndCollections();
        }

        private void InitValuesAndCollections()
        {
            AddFahrerValue = new Fahrer();
        }

        private void InitCommandsAndServices()
        {
            AddFahrerCommand = new DelegateCommand(AddFahrer);
            CloseDialogAddFahrerCommand = new DelegateCommand(CloseAddFahrerDialog);

            EditFahrerCommand = new DelegateCommand(EditFahrer);
            CloseDialogEditFahrerCommand = new DelegateCommand(CloseEditFahrerDialog);

            IServiceContainer = new ServiceContainer(this);
            ServiceContainer.RegisterService(new DevExpress.Mvvm.UI.MessageBoxService());
        }

        #endregion Initialisations

        #region Private Methods


        #endregion Private Methods

        #region Command Methods

        private void AddFahrer()
        {
            if (string.IsNullOrEmpty(AddFahrerValue.NameVorname))
                MessageBoxService.ShowMessage("Name darf nicht leer sein!", "Fehler", MessageButton.OK, MessageIcon.Information);
            else
                CloseDialogAddFahrerFunc.Invoke(1);
        }

        private void CloseAddFahrerDialog()
        {
            CloseDialogAddFahrerFunc.Invoke(0);
        }

        private void EditFahrer()
        {
            CloseDialogEditFahrerFunc.Invoke(1);
        }

        private void CloseEditFahrerDialog()
        {
            CloseDialogEditFahrerFunc.Invoke(0);
        }

        #endregion Command Methods
    }
}
