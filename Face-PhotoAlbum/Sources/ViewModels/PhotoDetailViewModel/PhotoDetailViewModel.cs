using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class PhotoDetailViewModel : BindableBase, IConfirmation, IInteractionRequestAware {

        public INotification Notification {
            get; set;
        }

        public Action FinishInteraction {
            get; set;
        }

        public bool Confirmed {
            get; set;
        }

        public string Title {
            get; set;
        }

        public object Content {
            get; set;
        }
        public byte[] CurrentImage {
            get {
                return _PhotoViewModelList[CurrentIndex].Image;
            }
        }
        public int CurrentIndex;

        public ObservableCollection<PhotoContainerViewModel> _PhotoViewModelList;
        public PhotoDetailViewModel() : base() {
            Title = "PhotoDetailWindow";
        }
        public PhotoDetailViewModel(ObservableCollection<PhotoContainerViewModel> photoViewModelList, PhotoContainerViewModel currentPhotoContainerViewModel) : this() {
            _PhotoViewModelList = photoViewModelList;
            CurrentIndex = photoViewModelList.IndexOf(currentPhotoContainerViewModel);
            this.OnPropertyChanged(() => this.CurrentImage);
        }
        private ICommand _CloseWindowCommand;
        private ICommand _NextPhotoCommand;
        private ICommand _PreviousPhotoCommand;
        public ICommand CloseWindowCommand {
            get {
                if (this._CloseWindowCommand == null) {
                    this._CloseWindowCommand = new CommandProxy(CloseWindow);
                }
                return this._CloseWindowCommand;
            }
        }
        public ICommand NextPhotoCommand {
            get {
                if (this._NextPhotoCommand == null) {
                    this._NextPhotoCommand = new CommandProxy(NextPhoto);
                }
                return this._NextPhotoCommand;
            }
        }
        public ICommand PreviousPhotoCommand {
            get {
                if (this._PreviousPhotoCommand == null) {
                    this._PreviousPhotoCommand = new CommandProxy(PreviousPhoto);
                }
                return this._PreviousPhotoCommand;
            }
        }
        private void CloseWindow(object parameter) {
            try {
                this.FinishInteraction();
            }
            catch (Exception ex) {
                throw;
            }
        }
        private void NextPhoto(object parameter) {
            try {
                if (CurrentIndex < _PhotoViewModelList.Count - 1) {
                    _PhotoViewModelList[CurrentIndex].IsSelected = false;
                    CurrentIndex++;
                }
                _PhotoViewModelList[CurrentIndex].IsSelected = true;
                this.OnPropertyChanged(() => this.CurrentImage);
            }
            catch (Exception ex) {
                throw;
            }
        }
        private void PreviousPhoto(object parameter) {
            try {
                if (CurrentIndex > 0) {
                _PhotoViewModelList[CurrentIndex].IsSelected = false;
                    CurrentIndex--;
                }
                _PhotoViewModelList[CurrentIndex].IsSelected = true;
                this.OnPropertyChanged(() => this.CurrentImage);
            }
            catch (Exception ex) {
                throw;
            }
        }

    }
}
