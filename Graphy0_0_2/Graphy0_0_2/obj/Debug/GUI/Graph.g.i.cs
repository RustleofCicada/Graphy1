﻿#pragma checksum "..\..\..\GUI\Graph.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2073EC61D6AC952E82A5386718F2DC972819B184"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using Graphy0_0_2.GUI;
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


namespace Graphy0_0_2.GUI {
    
    
    /// <summary>
    /// Graph
    /// </summary>
    public partial class Graph : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\GUI\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Graphy0_0_2.GUI.Graph GraphControl;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\GUI\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas DrawingArea;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\GUI\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line Xaxis;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\GUI\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line Yaxis;
        
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
            System.Uri resourceLocater = new System.Uri("/Graphy0_0_2;component/gui/graph.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GUI\Graph.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.GraphControl = ((Graphy0_0_2.GUI.Graph)(target));
            return;
            case 2:
            this.DrawingArea = ((System.Windows.Controls.Canvas)(target));
            
            #line 15 "..\..\..\GUI\Graph.xaml"
            this.DrawingArea.Loaded += new System.Windows.RoutedEventHandler(this.DrawingArea_Loaded);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\GUI\Graph.xaml"
            this.DrawingArea.MouseMove += new System.Windows.Input.MouseEventHandler(this.DrawingArea_MouseMove);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\GUI\Graph.xaml"
            this.DrawingArea.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DrawingArea_MouseDown);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\GUI\Graph.xaml"
            this.DrawingArea.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.DrawingArea_MouseUp);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\GUI\Graph.xaml"
            this.DrawingArea.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.DrawingArea_MouseWheel);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\GUI\Graph.xaml"
            this.DrawingArea.SizeChanged += new System.Windows.SizeChangedEventHandler(this.DrawingArea_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Xaxis = ((System.Windows.Shapes.Line)(target));
            return;
            case 4:
            this.Yaxis = ((System.Windows.Shapes.Line)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
