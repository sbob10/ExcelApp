using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourenVerwaltung
{
    class FirmaWindowsViewModel : ViewModelBase
    {
        #region Properties

        private Firma _AddFirmaValue;

        public Firma AddFirmaValue
        {
            get { return _AddFirmaValue; }
            set { SetProperty(ref _AddFirmaValue, value, () => AddFirmaValue); }
        }

        private Firma _EditFirmaValue;

        public Firma EditFirmaValue
        {
            get { return _EditFirmaValue; }
            set { SetProperty(ref _EditFirmaValue, value, () => EditFirmaValue); }
        }

        #endregion Properties

        #region Commands&Services

        public Func<int, string> CloseDialogAddFirmaFunc { get; set; }
        public Func<int, string> CloseDialogEditFirmaFunc { get; set; }

        public ICommand AddFirmaCommand { get; set; }
        public ICommand CloseDialogAddFirmaCommand { get; set; }

        public ICommand EditFirmaCommand { get; set; }
        public ICommand CloseDialogEditFirmaCommand { get; set; }

        public IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } set { } }
        public IServiceContainer IServiceContainer { get; set; }

        #endregion Commands

        #region Initialisations

        public FirmaWindowsViewModel()
        {
            InitCommandsAndServices();
            InitValuesAndCollections();
        }

        private void InitValuesAndCollections()
        {
            AddFirmaValue = new Firma();
        }

        private void InitCommandsAndServices()
        {
            AddFirmaCommand = new DelegateCommand(AddFirma);
            CloseDialogAddFirmaCommand = new DelegateCommand(CloseAddFirmaDialog);

            EditFirmaCommand = new DelegateCommand(EditFirma);
            CloseDialogEditFirmaCommand = new DelegateCommand(CloseEditFirmaDialog);

            IServiceContainer = new ServiceContainer(this);
            ServiceContainer.RegisterService(new DevExpress.Mvvm.UI.MessageBoxService());
        }

        #endregion Initialisations

        #region Private Methods


        #endregion Private Methods

        #region Command Methods

        private void AddFirma()
        {
            if (string.IsNullOrEmpty(AddFirmaValue.Name))
                MessageBoxService.ShowMessage("Name darf nicht leer sein!", "Fehler", MessageButton.OK, MessageIcon.Information);
            else
                CloseDialogAddFirmaFunc.Invoke(1);
        }

        private void CloseAddFirmaDialog()
        {
            CloseDialogAddFirmaFunc.Invoke(0);
        }

        private void EditFirma()
        {
            CloseDialogEditFirmaFunc.Invoke(1);
        }

        private void CloseEditFirmaDialog()
        {
            CloseDialogEditFirmaFunc.Invoke(0);
        }

        #endregion Command Methods


    }
}
