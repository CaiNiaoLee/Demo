using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int _groupANums = 3;
        private readonly int _groupBNums = 5;
        private readonly int _groupCNums = 7;
        private bool _userAPlaying;
        private bool _userBPlaying;
        private string lastStartUser;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            for (int i = 0; i < _groupCNums + _groupBNums + _groupANums; i++)
            {
                TextBlock tb = new TextBlock {Text = "牙签", Foreground = new SolidColorBrush(Colors.Green), Margin = new Thickness(10, 10, 10, 10)};
                tb.MouseLeftButtonDown += Tb_MouseLeftButtonDown;
                if (this.GB1SPanel.Children.Count < _groupANums)
                    this.GB1SPanel.Children.Add(tb);
                else if (this.GB2SPanel.Children.Count < _groupBNums)
                    this.GB2SPanel.Children.Add(tb);
                else 
                    this.GB3SPanel.Children.Add(tb);
            }
        }

        private void Tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_userAPlaying == false && _userBPlaying == false)
            {
                MessageBox.Show("请选择一名用户点击开始游戏。");
                return;
            }

            if (sender is TextBlock tb)
            {
                if (tb.Parent is StackPanel stackPanel)
                {
                    if (stackPanel == this.GB1SPanel)
                    {
                        this.GB2SPanel.IsEnabled = false;
                        this.GB2SPanel.Opacity = 0.2;
                        this.GB3SPanel.IsEnabled = false;
                        this.GB3SPanel.Opacity = 0.2;
                    }
                    else if (stackPanel == this.GB2SPanel)
                    {
                        this.GB1SPanel.IsEnabled = false;
                        this.GB1SPanel.Opacity = 0.2;
                        this.GB3SPanel.IsEnabled = false;
                        this.GB3SPanel.Opacity = 0.2;
                    }
                    else
                    {
                        this.GB2SPanel.IsEnabled = false;
                        this.GB2SPanel.Opacity = 0.2;
                        this.GB1SPanel.IsEnabled = false;
                        this.GB1SPanel.Opacity = 0.2;
                    }

                    stackPanel.Children.Remove(tb);
                    if (!CheckGameIsOver()) 
                        return;
                    var msg = _userAPlaying ? "用户B获胜" : "用户A获胜";
                    MessageBox.Show(msg);
                }
            }
        }

        private void UserABtn_OnClick(object sender, RoutedEventArgs e)
        {
            if ((string)this.UserABtn.Content == "用户A开始")
            {
                if (lastStartUser == "A")
                {
                    MessageBox.Show("同一用户不能连续开始.");
                    return;
                }

                _userAPlaying = true;
                _userBPlaying = false;
                this.UserBBtn.IsEnabled = false;
                this.UserBBtn.Opacity = 0.3;
                this.UserABtn.Content = "结束";
                lastStartUser = "A";
            }
            else
            {
                _userAPlaying = false;
                this.UserBBtn.IsEnabled = true;
                this.UserABtn.IsEnabled = true;
                this.UserBBtn.Opacity = 1;
                this.UserABtn.Content = "用户A开始";

                this.GB1SPanel.IsEnabled = true;
                this.GB1SPanel.Opacity = 1;
                this.GB2SPanel.IsEnabled = true;
                this.GB2SPanel.Opacity = 1;
                this.GB3SPanel.IsEnabled = true;
                this.GB3SPanel.Opacity = 1;
            }
        }

        private void UserBBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if ((string)this.UserBBtn.Content == "用户B开始")
            {
                if (lastStartUser == "B")
                {
                    MessageBox.Show("同一用户不能连续开始.");
                    return;
                }

                _userBPlaying = true;
                _userAPlaying = false;
                this.UserABtn.IsEnabled = false;
                this.UserABtn.Opacity = 0.3;
                this.UserBBtn.Content = "结束";
                lastStartUser = "B";
            }
            else
            {
                _userBPlaying = false;
                this.UserBBtn.IsEnabled = true;
                this.UserABtn.IsEnabled = true;
                this.UserABtn.Opacity = 1;
                this.UserBBtn.Content = "用户B开始";

                this.GB1SPanel.IsEnabled = true;
                this.GB1SPanel.Opacity = 1;
                this.GB2SPanel.IsEnabled = true;
                this.GB2SPanel.Opacity = 1;
                this.GB3SPanel.IsEnabled = true;
                this.GB3SPanel.Opacity = 1;
            }
        }


        private bool CheckGameIsOver()
        {
            return this.GB1SPanel.Children.Count == 0 && this.GB2SPanel.Children.Count == 0 && this.GB3SPanel.Children.Count == 0;
        }
    }
}
