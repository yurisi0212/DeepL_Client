using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace DeepL_Client {
    /// <summary>
    /// TokenSettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TokenSettingWindow : Window {

       
        public TokenSettingWindow() {
            InitializeComponent();
            Token_TextBox.Text = MainWindow.tokenManager.DeepL_Token;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            MainWindow.tokenManager.SetToken(Token_TextBox.Text);
            Close();
        }
    }
}
