﻿#pragma checksum "C:\Users\Hang BY\Documents\Visual Studio 2013\Projects\SeminarNhung\Calendar\EventData.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8DAA211DFD471FFC6FBC9830E993F37C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Calendar {
    
    
    public partial class EventData : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox txtSubject;
        
        internal System.Windows.Controls.TextBox txtLocal;
        
        internal System.Windows.Controls.TextBox txtDate;
        
        internal System.Windows.Controls.Button btSave;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Calendar;component/EventData.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.txtSubject = ((System.Windows.Controls.TextBox)(this.FindName("txtSubject")));
            this.txtLocal = ((System.Windows.Controls.TextBox)(this.FindName("txtLocal")));
            this.txtDate = ((System.Windows.Controls.TextBox)(this.FindName("txtDate")));
            this.btSave = ((System.Windows.Controls.Button)(this.FindName("btSave")));
        }
    }
}

