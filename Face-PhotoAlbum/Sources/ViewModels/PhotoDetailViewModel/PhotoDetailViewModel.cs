using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.ViewModels {
    public class PhotoDetailViewModel : BindableBase, INotification {
        public object Content { get; set; }
        public string Title { get; set; }

        public ObservableCollection<PhotoContainerViewModel> _PhotoViewModelList;
        public PhotoContainerViewModel _CurrentPhotoContainerViewModel;
        public PhotoDetailViewModel(ObservableCollection<PhotoContainerViewModel> photoViewModelList, PhotoContainerViewModel currentPhotoContainerViewModel) : base() {
            Title = "PhotoDetailWindow";
            _PhotoViewModelList = photoViewModelList;
            _CurrentPhotoContainerViewModel = currentPhotoContainerViewModel;
        }

    }
}
