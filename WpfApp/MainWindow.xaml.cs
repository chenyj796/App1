using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
using Briver.Framework;

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = this;

            }
        }

        public string HttpMessage
        {
            get => (string)GetValue(HttpMessageProperty);
            set => SetValue(HttpMessageProperty, value);
        }

        // Using a DependencyProperty as the backing store for HttpMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HttpMessageProperty =
            DependencyProperty.Register("HttpMessage", typeof(string), typeof(MainWindow), new PropertyMetadata(""));

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var start = DateTime.Now.ToString("HH:mm:ss.fffffff");
            var api = SystemContext.GetHeadExport<IWebApi>();
            var response = await api.Client.GetAsync("test");
            var content = await response.Content.ReadAsStringAsync();
            this.HttpMessage += $@"
开始：{start}
处理：{content}
完成：{DateTime.Now:HH:mm:ss.fffffff}
";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox)?.ScrollToEnd();
        }
    }
}
