﻿#pragma checksum "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "669AD8F67B62B87A7B0B84776D5D1025DA3C0D6AA57AE84DA3250EE3768B71A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Views.SystemManage;


namespace Xh.FastTrading.Wpf.Views.SystemManage {
    
    
    /// <summary>
    /// UserManageUnitPowerView
    /// </summary>
    public partial class UserManageUnitPowerView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 66 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstPower;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectAll;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClean;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstUnitPower;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfirm;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Sup-Trade;component/views/systemmanage/usermanageunitpowerview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lstPower = ((System.Windows.Controls.ListBox)(target));
            
            #line 75 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            this.lstPower.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstPower_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnSelectAll = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            this.btnSelectAll.Click += new System.Windows.RoutedEventHandler(this.btnSelectAll_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClean = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            this.btnClean.Click += new System.Windows.RoutedEventHandler(this.btnClean_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lstUnitPower = ((System.Windows.Controls.ListBox)(target));
            
            #line 111 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            this.lstUnitPower.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstUnitPower_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 116 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\..\..\Views\SystemManage\UserManageUnitPowerView.xaml"
            this.btnConfirm.Click += new System.Windows.RoutedEventHandler(this.btnConfirm_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

