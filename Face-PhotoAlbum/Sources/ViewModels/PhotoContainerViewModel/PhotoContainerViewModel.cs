﻿using Face_PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class PhotoContainerViewModel : ObservableObject {
        private bool _IsSelected = false;
        private static ObservableCollection<PhotoContainerViewModel> _PhotoViewModelList;
        private ICommand _SelectPhotoCommand;

        public ICommand SelectPhotoCommand {
            get {
                if (this._SelectPhotoCommand == null) {
                    this._SelectPhotoCommand = new CommandProxy(SelectPhoto);
                }
                return this._SelectPhotoCommand;
            }
        }
        public int PhotoNum { private set; get; }
        public byte[] Image { private set; get; }

        private void SelectPhoto(object parameter) {
            try {
                foreach (var Element in _PhotoViewModelList) {
                    if (Element != this && Element.IsSelected) {
                        Element.IsSelected = false;
                    }
                }
                IsSelected = true;
            }
            catch (Exception ex) {
                throw;
            }
        }

        public bool IsSelected {
            get {
                return _IsSelected;
            }
            set {
                if (_IsSelected == value)
                    return;
                _IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #region 数据获取
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<PhotoContainerViewModel> ConvertToViewModelDataList(IEnumerable<PhotoContainerModel> model) {
            _PhotoViewModelList = new ObservableCollection<PhotoContainerViewModel>();
            model.ToList().ForEach(row => {
                PhotoContainerViewModel photoViewModel = new PhotoContainerViewModel();
                photoViewModel.PhotoNum = row.PhotoNum;
                photoViewModel.Image = File.ReadAllBytes(row.ImagePath);
                photoViewModel.IsSelected = false;
                _PhotoViewModelList.Add(photoViewModel);
            });

            return _PhotoViewModelList;
        }
        #endregion
    }

    public class PhotoSelectStatusToBgPathConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value) {
                return @"/Resources/照片背景selected.png";
            }
            else {
                return @"/Resources/照片背景.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}