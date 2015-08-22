﻿using Face_PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class AlbumWindow : ObservableObject/*, IAlbumWindowViewModel*/ {
        private ObservableCollection<FaceAlbumViewModel> _FaceAlbums;

        private AlbumWindowModel model = new AlbumWindowModel();
        private ICommand _ReadFaceAlbumsCommand;
        private ICommand _SelectFaceAlbumCommand;
        private ICommand _EnterFaceAlbumCommand;

        public ObservableCollection<FaceAlbumViewModel> FaceAlbums { get; set; }

        public ICommand ReadFaceAlbumsCommand
        {
            get
            {
                if (this._ReadFaceAlbumsCommand == null)
                {
                    this._ReadFaceAlbumsCommand = new CommandProxy(ReadFaceAlbums);
                }
                return this._ReadFaceAlbumsCommand;
            }
        }
        public ICommand SelectFaceAlbumCommand
        {
            get
            {
                if (this._SelectFaceAlbumCommand == null)
                {
                    this._SelectFaceAlbumCommand = new CommandProxy(SelectFaceAlbum);
                }
                return this._SelectFaceAlbumCommand;
            }
        }
        public ICommand EnterFaceAlbumCommand
        {
            get
            {
                if (this._EnterFaceAlbumCommand == null)
                {
                    this._EnterFaceAlbumCommand = new CommandProxy(EnterFaceAlbum);
                }
                return this._EnterFaceAlbumCommand;
            }
        }

        private void ReadFaceAlbums(object parameter)
        {

        }
        private void SelectFaceAlbum(object parameter)
        {

        }
        private void EnterFaceAlbum(object parameter)
        {

        }

    }
}