using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using UnityApp = UnityPlayer.UnityApp;
using UnityBridge = WinRTBridge.WinRTBridge;
using Microsoft.Phone.Marketplace;

namespace InApp
{
	public partial class MainPage : PhoneApplicationPage
	{
		private bool _unityStartedLoading;

		// Constructor
		public MainPage()
		{
			var bridge = new UnityBridge();
			UnityApp.SetBridge(bridge);            
			InitializeComponent();
			bridge.Control = DrawingSurfaceBackground;
            UpdateInformation();
		}
        public void UpdateInformation()
        {
            LicenseInformation licenseInfo = new LicenseInformation();
            inappControl.isTrial = licenseInfo.IsTrial();
            //IO LI HO MESSI QUI PER SEMPLICITà, MA PER FARE UNA COSA CORRETTA VA FATTO COSì: (NELLA CLASSE APP.CS)
        // public PhoneApplicationFrame RootFrame { get; private set; }
        //public Boolean isTrial = false;
        //private LicenseInformation licenseInfo = new LicenseInformation();
        ///// <summary>
        ///// Constructor for the Application object.
        ///// </summary>
        //public App()
        //{
        //    // Global handler for uncaught exceptions. 
        //    UnhandledException += Application_UnhandledException;

        //    // Standard Silverlight initialization
        //    InitializeComponent();

        //    // Phone-specific initialization
        //    InitializePhoneApplication();

        //    // Show graphics profiling information while debugging.
        //    if (System.Diagnostics.Debugger.IsAttached)
        //    {
        //        // Display the current frame rate counters
        //        Application.Current.Host.Settings.EnableFrameRateCounter = true;

        //        // Show the areas of the app that are being redrawn in each frame.
        //        //Application.Current.Host.Settings.EnableRedrawRegions = true;

        //        // Enable non-production analysis visualization mode, 
        //        // which shows areas of a page that are handed off to GPU with a colored overlay.
        //        //Application.Current.Host.Settings.EnableCacheVisualization = true;

        //        // Disable the application idle detection by setting the UserIdleDetectionMode property of the
        //        // application's PhoneApplicationService object to Disabled.
        //        // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
        //        // and consume battery power when the user is not using the phone.
        //        PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
        //    }
        //}

        //// Code to execute when the application is launching (eg, from Start)
        //// This code will not execute when the application is reactivated
        //private void Application_Launching(object sender, LaunchingEventArgs e)
        //{
        //    isTrial = licenseInfo.IsTrial();
        //}

        //// Code to execute when the application is activated (brought to foreground)
        //// This code will not execute when the application is first launched
        //private void Application_Activated(object sender, ActivatedEventArgs e)
        //{
        //    isTrial = licenseInfo.IsTrial();
        //}
        
        }
		private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
		{
            
			if (!_unityStartedLoading)
			{
				_unityStartedLoading = true;

				UnityApp.SetLoadedCallback(() => { Dispatcher.BeginInvoke(Unity_Loaded); });

				var content = Application.Current.Host.Content;
				var width = (int)Math.Floor(content.ActualWidth * content.ScaleFactor / 100.0 + 0.5);
				var height = (int)Math.Floor(content.ActualHeight * content.ScaleFactor / 100.0 + 0.5);

				UnityApp.SetNativeResolution(width, height);
				UnityApp.SetRenderResolution(width, height);
				UnityPlayer.UnityApp.SetOrientation((int)Orientation);

				DrawingSurfaceBackground.SetBackgroundContentProvider(UnityApp.GetBackgroundContentProvider());
				DrawingSurfaceBackground.SetBackgroundManipulationHandler(UnityApp.GetManipulationHandler());
			}
		}

		private void Unity_Loaded()
		{
		}

		private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
		{
			e.Cancel = UnityApp.BackButtonPressed();
		}

		private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
		{
			UnityApp.SetOrientation((int)e.Orientation);
		}
	}
}