using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlaninarskoDrustvo
{
    class MkThemeSelector: DependencyObject
    {
        public static readonly DependencyProperty CurrentThemeDictionaryProperty =
             DependencyProperty.RegisterAttached("CurrentThemeDictionary", typeof(Uri),
             typeof(MkThemeSelector),
             new UIPropertyMetadata(null, CurrentThemeDictionaryChanged));

        public static Uri GetCurrentThemeDictionary(DependencyObject obj)
        {
            return (Uri)obj.GetValue(CurrentThemeDictionaryProperty);
        }

        public static void SetCurrentThemeDictionary(DependencyObject obj, Uri value)
        {
            obj.SetValue(CurrentThemeDictionaryProperty, value);
        }

        private static void CurrentThemeDictionaryChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is FrameworkElement) 
            {
                ApplyTheme(obj as FrameworkElement, GetCurrentThemeDictionary(obj));
            }
        }

        private static void ApplyTheme(FrameworkElement targetElement, Uri dictionaryUri)
        {
            sbyte theme = 0;
            if (dictionaryUri.ToString() == "Theme1.xaml")
                theme = 1;
            else if (dictionaryUri.ToString() == "Theme2.xaml")
                theme = 2;
            else
                theme = 3;
            using (Model1 model = new Model1())
            {
                var user = (from c in model.accounts
                            where c.username == CurrentSession.User.username
                            select c).FirstOrDefault();
                user.theme = theme;
                model.SaveChanges();
            }
            CurrentSession.User.theme = theme;
            if (targetElement == null) return;

            try
            {
                ThemeResourceDictionary themeDictionary = null;
                if (dictionaryUri != null)
                {
                    themeDictionary = new ThemeResourceDictionary();
                    themeDictionary.Source = dictionaryUri;

                    targetElement.Resources.MergedDictionaries.Insert(0, themeDictionary);
                }

                List<ThemeResourceDictionary> existingDictionaries =
                    (from dictionary in targetElement.Resources.MergedDictionaries.OfType<ThemeResourceDictionary>() //prepravka
                     select dictionary).ToList();

                foreach (ThemeResourceDictionary thDictionary in existingDictionaries)
                {
                    if (themeDictionary == thDictionary) continue; 
                    targetElement.Resources.MergedDictionaries.Remove(thDictionary);
                }
            }
            finally { }
        }
    }
}
