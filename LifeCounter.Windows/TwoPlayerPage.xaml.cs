using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LifeCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TwoPlayerPage : Page
    {
        GameManager _manager = new GameManager(2);

        public TwoPlayerPage()
        {
            this.InitializeComponent();

            Player1.Init(_manager, _manager.Player2);
            Player2.Init(_manager, _manager.Player2);

            Player1.SetBackGround(BackGroundColors.Red);
            Player2.SetBackGround(BackGroundColors.Green);
           
        }

        private void BtnReset2P_Click(object sender, RoutedEventArgs e)
        {
            Player1.Reset();
            Player2.Reset();
        }

        private void BtnResetCommander_Click(object sender, RoutedEventArgs e)
        {
            Player1.Reset(Gametypes.Commander);
            Player2.Reset(Gametypes.Commander);

        }

        private void BtnReset3P_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ThreePlayerPage), null);

        }

        private void BtnResetMP_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);

        }

        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
            CmdBar.IsOpen = true;
        }

        private DisplayRequest KeepScreenOnRequest;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (KeepScreenOnRequest == null)
                KeepScreenOnRequest = new DisplayRequest();

            KeepScreenOnRequest.RequestActive();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            KeepScreenOnRequest.RequestRelease();
        }
    }
}
