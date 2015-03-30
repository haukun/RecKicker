using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace RecKicker
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TargetDatePicker.SelectedDate = DateTime.Now;

            MoviePathText.Text = Properties.Settings.Default.MoviePathText;
            PrefixTitleText.Text = Properties.Settings.Default.PrefixText;
            SuffixTitleText.Text = Properties.Settings.Default.SuffixText;
        }

        private void PrefixTitleText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TargetTitleFixLabel.Text = CreateTitle();
        }

        private void SuffixTitleText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TargetTitleFixLabel.Text = CreateTitle();
        }

        private void TargetDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TargetTitleFixLabel.Text = CreateTitle();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.MoviePathText = MoviePathText.Text;
            Properties.Settings.Default.PrefixText = PrefixTitleText.Text;
            Properties.Settings.Default.SuffixText = SuffixTitleText.Text;

            Properties.Settings.Default.Save();
        }

        private void PrevDayButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime tempDate = TargetDatePicker.SelectedDate.GetValueOrDefault();
            tempDate = tempDate.AddDays(-1);
            TargetDatePicker.SelectedDate = tempDate;
        }

        private void NextDayButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime tempDate = TargetDatePicker.SelectedDate.GetValueOrDefault();
            tempDate = tempDate.AddDays(1);
            TargetDatePicker.SelectedDate = tempDate;
        }

        private void TransfarButton_Click(object sender, RoutedEventArgs e)
        {
            //  INIファイル更新
            RewriteIniFile();
        }


        /// <summary>
        /// 番組タイトルの作成
        /// </summary>
        /// <returns></returns>
        private string CreateTitle()
        {
            return PrefixTitleText.Text + TargetDatePicker.SelectedDate.Value.ToString("MMdd") + SuffixTitleText.Text;
        }

        /// <summary>
        /// 番組ファイル名の作成
        /// </summary>
        /// <returns></returns>
        private string CreateFileName()
        {
            return string.Format("{0}\\{1}2258_1seg.TS", MoviePathText.Text, CreateTitle()); 
        }

        //  "WBS_0311" "" 71 2013/02/05 22:58 2013/02/05 23:59 "E:\TV-Record\WBS_03112258_1seg.TS" ""

        private bool RewriteIniFile()
        {
            bool result = false;

            string textline = string.Format("\"{0}\" \"\" 71 2013/02/05 22:58 2013/02/05 23:59 \"{1}\" \"\"", CreateTitle(), CreateFileName());

            MessageBox.Show(textline);
            return result;
        }

    }
}
