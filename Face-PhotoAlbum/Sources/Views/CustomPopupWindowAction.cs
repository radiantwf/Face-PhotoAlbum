using Microsoft.Practices.Prism.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Face_PhotoAlbum.Views {
    public class CustomPopupWindowAction : PopupWindowAction {
        /// <summary>
        /// 通过重写PopupWindowAction中的GetWindow方法，设置Window的Style属性。
        /// 否则打开的只能是默认窗体，无法设置样式。
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        protected override Window GetWindow(INotification notification) {

            Window wrapperWindow = base.GetWindow(notification);
            ResourceDictionary langRd = null;
            try {
                langRd = Application.LoadComponent(new Uri("Face-PhotoAlbum;component/Sources/Views/CustomPopupWindowStyle.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
            }
            catch {
            }
            if (langRd != null) {
                if (wrapperWindow.Resources.MergedDictionaries.Count > 0) {
                    wrapperWindow.Resources.MergedDictionaries.Clear();
                }
                wrapperWindow.Resources.MergedDictionaries.Add(langRd);
            }

            //if (this.WindowContent != null) {
            //    wrapperWindow = new Window();

            //    wrapperWindow.DataContext = notification;
            //    wrapperWindow.Title = notification.Title;

            //    ResourceDictionary langRd = null;
            //    try {
            //        langRd = Application.LoadComponent(new Uri("Face-PhotoAlbum;component/Sources/Views/CustomPopupWindowStyle.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
            //    }
            //    catch {
            //    }
            //    if (langRd != null) {
            //        if (wrapperWindow.Resources.MergedDictionaries.Count > 0) {
            //            wrapperWindow.Resources.MergedDictionaries.Clear();
            //        }
            //        wrapperWindow.Resources.MergedDictionaries.Add(langRd);
            //        wrapperWindow.Resources.MergedDictionaries.Add(langRd2);

            //    }
            //    this.PrepareContentForWindow(notification, wrapperWindow);
            //}
            //else {
            //    wrapperWindow = this.CreateDefaultWindow(notification);
            //}

            return wrapperWindow;
        }
    }
}
