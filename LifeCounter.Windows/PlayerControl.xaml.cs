using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LifeCounter
{
    public sealed partial class PlayerControl : UserControl
    {
        public PlayerViewModel viewModel { get; set; } = new PlayerViewModel();


        private BackGroundColors BackGroundColor { get; set; } = BackGroundColors.Red;

     

        private int Rotation { get; set; } = 0;

        public PlayerControl()
        {
            this.InitializeComponent();
            UpdateLifeTotal();
            UpdateName();
            UpdateEnergy();
            UpdateCommanderDmg();

            rtAngle.Angle = 0;
            rtPanelOptions.Angle = 0;

            viewModel.PropertyChanged += ViewModel_PropertyChanged;

            this.DataContext = viewModel;    

        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "PlayerName")
            {
                UpdateName();
            }
        }

        internal void Flip()
        {
            ApplyRotation();
        }

        public void SetBackGround(BackGroundColors color)
        {
            this.BackGroundColor = color;

            switch (BackGroundColor)
            {
                case BackGroundColors.Red:
                    BackGroundGradientStart.Color = Colors.Red;
                    BackGroundGradientEnd.Color = Colors.Maroon;
                    break;
                case BackGroundColors.Blue:
                    BackGroundGradientStart.Color = Colors.Blue;
                    BackGroundGradientEnd.Color = Colors.DarkBlue;
                    break;
                case BackGroundColors.Green:
                    BackGroundGradientStart.Color = Colors.ForestGreen;
                    BackGroundGradientEnd.Color = Colors.DarkGreen;
                    break;
                case BackGroundColors.Purple:
                    BackGroundGradientStart.Color = Colors.MediumPurple;
                    BackGroundGradientEnd.Color = Colors.Purple;
                    break;
                case BackGroundColors.Yellow:
                    BackGroundGradientStart.Color = Colors.Goldenrod;
                    BackGroundGradientEnd.Color = Colors.DarkGoldenrod;
                    break;
                default:
                    break;
            }
        }

        Gametypes _lastType = Gametypes.MultiPlayer;
        public void Reset(Gametypes type = Gametypes.MultiPlayer)
        {
            _lastType = type;
            viewModel.LifeTotal = type == Gametypes.MultiPlayer ? 20 : 40;
            viewModel.Energy = 0;
            viewModel.Experience = 0;
            viewModel.Poison = 0;
            viewModel.CmdEnemy1 = 0;
            viewModel.CmdEnemy2 = 0;
            viewModel.CmdEnemy3 = 0;

            if(type == Gametypes.Commander)
                BorderLifebutton.SetValue(Grid.RowSpanProperty, 1);
            else if( type == Gametypes.MultiPlayer)
                BorderLifebutton.SetValue(Grid.RowSpanProperty, 2);

            BorderCmd.Visibility = type == Gametypes.Commander ? Visibility.Visible : Visibility.Collapsed;

            UpdateLifeTotal();
            UpdateEnergy();
            UpdateCommanderDmg();
        }

        private void UpdateName()
        {
            tbPlayerName.Text = viewModel.PlayerName;
        }

        private void BtnDecreaseLife_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LifeTotal--;
            UpdateLifeTotal();
        }

        private void BtnIncreaseLife_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LifeTotal++;
            UpdateLifeTotal();
        }

        public void UpdateLifeTotal()
        {
            string lifeTotalString = viewModel.LifeTotal.ToString();
            string leftTxt = "";
            string rightTxt = "";
            if(viewModel.LifeTotal < 0)
            {
                leftTxt = "-";

                if (lifeTotalString.Length >2)
                {
                    leftTxt += lifeTotalString[1];
                    rightTxt = lifeTotalString.Substring(2);
                }
                else
                {
                    rightTxt = lifeTotalString.Substring(1);
                }

            }
            else
            {
                if (viewModel.LifeTotal <= 9)
                {
                    leftTxt = "0";
                    rightTxt = lifeTotalString;
                }
                else
                {
                    leftTxt += lifeTotalString[0];
                    rightTxt = lifeTotalString.Substring(1);
                }
            }

            tbDecreaseLife.Text = leftTxt;
            tbIncreaseLife.Text = rightTxt;
        }

        private void BtnIncreaseEnergy_Click(object sender, RoutedEventArgs e)
        {
            switch (viewModel.CurrentType)
            {
                case CustomCounterType.Energy:
                    viewModel.Energy++;
                    break;
                case CustomCounterType.Experience:
                    viewModel.Experience++;
                    break;
                case CustomCounterType.Poison:
                    viewModel.Poison++;
                    break;
            }

          
            UpdateEnergy();
        }

        private void BtnDecreaseEnergy_Click(object sender, RoutedEventArgs e)
        {
            switch (viewModel.CurrentType)
            {
                case CustomCounterType.Energy:
                    viewModel.Energy--;
                    if (viewModel.Energy <= 0)
                        viewModel.Energy = 0;
                    break;
                case CustomCounterType.Experience:
                    viewModel.Experience--;
                    if (viewModel.Experience <= 0)
                        viewModel.Experience = 0;
                    break;
                case CustomCounterType.Poison:
                    viewModel.Poison--;
                    if (viewModel.Poison <= 0)
                        viewModel.Poison = 0;
                    break;
            }
                      
            UpdateEnergy();
        }


        private void UpdateEnergy()
        {
            switch(viewModel.CurrentType)
            {
                case CustomCounterType.Energy:
                    tbCounterType.Text = viewModel.Energy.ToString();// $"Energy: {viewModel.Energy}";
                    break;
                case CustomCounterType.Experience:
                    tbCounterType.Text = viewModel.Experience.ToString();// $"Experience: {viewModel.Experience}";
                    break;
                case CustomCounterType.Poison:
                    tbCounterType.Text = viewModel.Poison.ToString();// $"Poison: {viewModel.Poison}";
                    break;
            } 
        }

        private void btnCounterType_Click(object sender, RoutedEventArgs e)
        {
            SwitchCounterType();           
        }

        private void tbEnergy_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            SwitchCounterType();
        }

        private void SwitchCounterType()
        {
            switch (viewModel.CurrentType)
            {
                case CustomCounterType.Energy:
                    viewModel.CurrentType = CustomCounterType.Experience;
                    break;
                case CustomCounterType.Experience:
                    viewModel.CurrentType = CustomCounterType.Poison;
                    break;
                case CustomCounterType.Poison:
                    viewModel.CurrentType = CustomCounterType.Energy;
                    break;
            }
            UpdateEnergy();
           
            imgCountertype.Source = new BitmapImage(new Uri("ms-appx:///" + CounterTypeHelper.CounterTypeImage(viewModel.CurrentType)));
        }

        private void BtnDecreaseLife_Holding(object sender, HoldingRoutedEventArgs e)
        {
            viewModel.LifeTotal -= 5;
            UpdateLifeTotal();
        }

        private void BtnIncreaseLife_Holding(object sender, HoldingRoutedEventArgs e)
        {
            viewModel.LifeTotal += 5;
            UpdateLifeTotal();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset(_lastType);
        }


        private void btnCmdE1_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.CmdEnemy1++;
            UpdateCommanderDmg();
        }

        private void btnCmdE2_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.CmdEnemy2++;
            UpdateCommanderDmg();
        }

        private void btnCmdE3_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.CmdEnemy3++;
            UpdateCommanderDmg();

        }

        private void UpdateCommanderDmg()
        {
            cmdE1TB.Text = this.viewModel.CmdEnemy1.ToString();
            cmdE2TB.Text = this.viewModel.CmdEnemy2.ToString();
            cmdE3TB.Text = this.viewModel.CmdEnemy3.ToString();
        }

        private void BtnReset_Click_1(object sender, RoutedEventArgs e)
        {
            Reset(_lastType);
        }

        private void BtnFlip_Click(object sender, RoutedEventArgs e)
        {
            ApplyRotation();
        }

        private void ApplyRotation()
        {
            Rotation = (Rotation + 180) % 360;
            rtAngle.Angle = Rotation;
            rtPanelOptions.Angle = Rotation;

            playerOption.Hide();
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SetBackGround( BackGroundColors.Red);
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            SetBackGround( BackGroundColors.Green);
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            SetBackGround( BackGroundColors.Blue);
        }

        private void btnPurple_Click(object sender, RoutedEventArgs e)
        {
            SetBackGround( BackGroundColors.Purple);
        }

        private void btnYellow_Click(object sender, RoutedEventArgs e)
        {
            SetBackGround( BackGroundColors.Yellow);
        }
    }
}
