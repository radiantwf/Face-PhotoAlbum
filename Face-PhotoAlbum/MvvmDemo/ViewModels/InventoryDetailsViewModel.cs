using MVVMDemo.Model;

namespace MVVMDemo.ViewModels
{
    public class InventoryDetailsViewModel : BaseViewModel
    {
        public string TabName
        {
            get;
            private set;
        }

        SampleCustomerModel customerModelObject;
        public SampleCustomerModel CustomerModelObject 
        {
            get
            {
                return customerModelObject;
            }
            set            
            {
                customerModelObject = value;
                RaisePropertyChanged("CustomerModelObject");
            }
        }

        // Default Constructor.
        public InventoryDetailsViewModel()
        {
        }
               
        public InventoryDetailsViewModel(string tabName, int lpCnt): this()
        {
            TabName = tabName;
            //Some dummy data load.
            CustomerModelObject = new SampleCustomerModel { FirstName = "Sai " + lpCnt , LastName = "Sri " + lpCnt };
        }
    }
}