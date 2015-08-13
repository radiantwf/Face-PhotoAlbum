using MVVMDemo.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MVVMDemo.ViewModels
{
    public class InventorySummaryViewModel :BaseViewModel
    {
        private CustomDelegateCommand loadInventoryDetailsCommand;

        private ObservableCollection<InventoryDetailsViewModel> inventoryDetailsViewModelCollection = new ObservableCollection<InventoryDetailsViewModel>();

        public ObservableCollection<InventoryDetailsViewModel> InventoryDetailsViewModelCollection
        {
            
            get
            {
                return inventoryDetailsViewModelCollection;
            }
        }

        public ICommand LoadInventoryDetailsCommand
        {
            get
            {
                if (loadInventoryDetailsCommand == null)
                {
                    loadInventoryDetailsCommand = new CustomDelegateCommand(new Action<object>(LoadInventoryDetailsCommandExecuted),
                                                                            new Func<bool>(CanLoadInventoryDetailsCommandExecute), false);
                }
                return loadInventoryDetailsCommand;
            }
        }

        public InventorySummaryViewModel()
        {
            // Work around for the know bug. If the below code is comented then first tab wont get the view popualted.
            //InventoryDetailsViewModelCollection.Add(new InventoryDetailsViewModel("Inventory " + ++lpCnt, lpCnt));
        }

        public bool CanLoadInventoryDetailsCommandExecute()
        {
            //No logic at this moment to disable the command
            return true;
        }

        int lpCnt = 0;
        public void LoadInventoryDetailsCommandExecuted(object testVal)
        {
            InventoryDetailsViewModelCollection.Add(new InventoryDetailsViewModel("Inventory " + ++lpCnt, lpCnt));
        }
    }
}