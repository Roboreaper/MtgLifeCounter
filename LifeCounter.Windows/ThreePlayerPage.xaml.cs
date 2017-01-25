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
    public sealed partial class ThreePlayerPage : Page
    {
        GameManager _manager = new GameManager(3);


        public ThreePlayerPage()
        {
            this.InitializeComponent();


            Player2.Init(_manager, _manager.Player2);
            Player3.Init(_manager, _manager.Player3);
            Player4.Init(_manager, _manager.Player4);        

            Player2.SetBackGround(BackGroundColors.Green);
            Player3.SetBackGround(BackGroundColors.Purple);
            Player4.SetBackGround(BackGroundColors.Blue);
        }

        private void BtnReset3P_Click(object sender, RoutedEventArgs e)
        {
            Player2.Reset();
            Player3.Reset();
            Player4.Reset();
        }

        private void BtnResetMP_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);

        }

        private void BtnResetCommander_Click(object sender, RoutedEventArgs e)
        {
            Player2.Reset(Gametypes.Commander);
            Player3.Reset(Gametypes.Commander);
            Player4.Reset(Gametypes.Commander);
        }

        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
            CmdBar.IsOpen = true;
        }

        private void BtnReset2P_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TwoPlayerPage), null);

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
