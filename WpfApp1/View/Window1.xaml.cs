﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Model;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public BoardGame_ViewModel Board { get; set; }

        public Window1(string n = "player")
        {
            Board = new BoardGame_ViewModel(n);
            DataContext = Board;
            InitializeComponent();
        }

        private void SlectCard(object sender, SelectionChangedEventArgs e)
        {
            Card carte = (Card)(sender as ListBox).SelectedItem;
            Board.text = "PLAY:" + carte.Num;
        }
        private void SlectLine(object sender, SelectionChangedEventArgs e)
        {
            int line = (sender as ListBox).SelectedIndex;
            Board.text = "Line:" + line;
        }
    }
}
