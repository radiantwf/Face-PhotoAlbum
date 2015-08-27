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
    public class AlbumWindowViewModel : ObservableObject, IAlbumWindowViewModel {
        private ObservableCollection<AlbumContainerViewModel> _FaceAlbums = null;
        private ObservableCollection<PhotoContainerViewModel> _Photos = null;
        private int _CurrentAlbumNum = -1;

        public enum ContentType { FaceAlbum, Photo }
        public ContentType _CurrentContentType = ContentType.FaceAlbum;

        private AlbumWindowModel model = new AlbumWindowModel();
        private ICommand _ReadFaceAlbumsCommand;
        private ICommand _BackToAlbumsCommand;
        private ICommand _SearchPhotosCommand;

        public ContentType CurrentContentType {
            get {
                return _CurrentContentType;
            }
            set {
                _CurrentContentType = value;
                if (_CurrentContentType == ContentType.FaceAlbum)
                    _CurrentAlbumNum = -1;
                RaisePropertyChanged(() => CurrentContentType);
            }
        }
        public ObservableCollection<AlbumContainerViewModel> FaceAlbums {
            get {
                return _FaceAlbums;
            }
            set {
                _FaceAlbums = value;
                RaisePropertyChanged(() => FaceAlbums);
            }
        }
        public ObservableCollection<PhotoContainerViewModel> Photos {
            get {
                return _Photos;
            }
            set {
                _Photos = value;
                RaisePropertyChanged(() => Photos);
            }
        }

        public ICommand ReadFaceAlbumsCommand {
            get {
                if (this._ReadFaceAlbumsCommand == null) {
                    this._ReadFaceAlbumsCommand = new CommandProxy(ReadFaceAlbums);
                }
                return this._ReadFaceAlbumsCommand;
            }
        }
        public ICommand BackToAlbumsCommand {
            get {
                if (this._BackToAlbumsCommand == null) {
                    this._BackToAlbumsCommand = new CommandProxy(BackToAlbums);
                }
                return this._BackToAlbumsCommand;
            }
        }
        public ICommand SearchPhotosCommand {
            get {
                if (this._SearchPhotosCommand == null) {
                    this._SearchPhotosCommand = new CommandProxy(SearchPhotos);
                }
                return this._SearchPhotosCommand;
            }
        }
        private void ReadFaceAlbums(object parameter) {
            try {
                var modelData = model.GetFaceAlbumList();
                FaceAlbums = AlbumContainerViewModel.ConvertToViewModelDataList(modelData);
            }
            catch (Exception ex) {
                throw;
            }
        }
        private void SearchPhotos(object parameter) {
            try {
                SearchPhotosBLL bll = new SearchPhotosBLL();
                bll.Run();
                ReadFaceAlbums(null);
                if (_CurrentAlbumNum != -1)
                    ReadPhotos(_CurrentAlbumNum);
            }
            catch (Exception ex) {
                throw;
            }
        }
        private void BackToAlbums(object parameter) {
            try {
                CurrentContentType = ContentType.FaceAlbum;
            }
            catch (Exception ex) {
                throw;
            }
        }
        public void ReadPhotos(int AlbumNum) {
            try {
                _CurrentAlbumNum = AlbumNum;
                var modelData = model.GetPhotoList(AlbumNum);
                Photos = PhotoContainerViewModel.ConvertToViewModelDataList(modelData);
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
    public class ContentTypeToFaceAlbumsShownFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((AlbumWindowViewModel.ContentType)value == AlbumWindowViewModel.ContentType.FaceAlbum) {
                    return System.Windows.Visibility.Visible;
                }
                else {
                    return System.Windows.Visibility.Collapsed;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ContentTypeToPhotosShownFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((AlbumWindowViewModel.ContentType)value == AlbumWindowViewModel.ContentType.Photo) {
                    return System.Windows.Visibility.Visible;
                }
                else {
                    return System.Windows.Visibility.Collapsed;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ContentTypeToBackButtonEnableFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((AlbumWindowViewModel.ContentType)value == AlbumWindowViewModel.ContentType.Photo) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}