using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace DeepL_Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public static readonly TokenManager tokenManager = new();

        public MainWindow() {
            InitializeComponent();

        }

        private async void Translate_Click(object sender, RoutedEventArgs e) {
            changeEnable(false);
            using (var httpClient = new HttpClient()) {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api-free.deepl.com/v2/translate")) {
                    ResetTextBox();
                    var contentList = new List<string>();
                    contentList.Add("auth_key=" + tokenManager.DeepL_Token);
                    contentList.Add("text=" + BeforeTextBox.Text);
                    var isJapaneseToEnglish = JapaneseToggleButton.IsChecked != null && (bool)JapaneseToggleButton.IsChecked;
                    var lang = isJapaneseToEnglish ? "EN" : "JA";

                    contentList.Add("target_lang=" + lang);

                    request.Content = new StringContent(string.Join("&", contentList));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);

                    var resBodyStr = response.Content.ReadAsStringAsync().Result;
                    JObject deserial = (JObject)JsonConvert.DeserializeObject(resBodyStr);

                    if (deserial == null) {
                        AfterTextBox.Text = "取得できませんでした。\nトークンが無効か、DeepLサーバーが落ちている可能性があります。";
                        AfterSnakecaseTextBox.Text = "";
                        changeEnable(true);
                        return;
                    }

                    var text = deserial["translations"][0]["text"].ToString();

                    AfterTextBox.Text = text;

                    if (isJapaneseToEnglish) {
                        var snakecase_text = text.ToLower().Replace(' ', '_');
                        AfterSnakecaseTextBox.Text = snakecase_text;
                    }
                    
                }
            }
            changeEnable(true);
        }

        private void Setting_Token_Click(object sender, RoutedEventArgs e) {
            var window = new TokenSettingWindow();
            window.ShowDialog();
        }


        private void Exit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void changeEnable(bool enable = true) {
            Status.Text = enable ? "" : "翻訳中...";
            BeforeTextBox.IsEnabled = enable;
            AfterTextBox.IsEnabled = enable;
            AfterSnakecaseTextBox.IsEnabled = enable;
            Translate_Button.IsEnabled = enable;
        }

        private void ResetTextBox() {
            AfterTextBox.Text = "";
            AfterSnakecaseTextBox.Text = "";
        }
    }
}
