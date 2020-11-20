using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PlaninarskoDrustvo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       public App()
        {
           // ChangeLanguage("sr-Latn-BA");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-Latn-BA");
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); 
        }

        /// <summary>
        /// Switches to language german.
        /// </summary>
        //public void SwitchToLanguageSerbian()
        //{
        //    ChangeLanguage("sr-Latn-BA");
        //}

        ///// <summary>
        ///// Switches to language english.
        ///// </summary>
        //public void SwitchToLanguageEnglish()
        //{
        //    ChangeLanguage("en-US");
        //}

        ///// <summary>
        ///// Changes the language according to the given culture.
        ///// </summary>
        ///// <param name="culture">The culture.</param>
        //private void ChangeLanguage(string culture)
        //{
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
        //    PlaninarskoDrustvo.Properties.Resources.Culture = new CultureInfo(culture);
        //}

    }
}
