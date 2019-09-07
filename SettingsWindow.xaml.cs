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

namespace 匈牙利回归
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class SettingsWindow : BaseDialogWindow
	{
		private List<MutableKeyValuePair> prefixRules;

		public SettingsWindow()
		{
			InitializeComponent();
		}


		private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			prefixRules = new List<MutableKeyValuePair>();
			prefixRules.Add(new MutableKeyValuePair("int", "n"));
			prefixRules.Add(new MutableKeyValuePair("string", "s"));
			prefixRules.Add(new MutableKeyValuePair("bool", "b"));
			prefixRules.Add(new MutableKeyValuePair("date", "d"));
			prefixRules.Sort((a, b) => a.Key.CompareTo(b.Key));

			PrefixList.ItemsSource = prefixRules;
		}

		private void AddRuleButton_OnClick(object sender, RoutedEventArgs e)
		{
			prefixRules.Add(new MutableKeyValuePair("",""));
			PrefixList.ItemsSource = prefixRules;
			PrefixList.Items.Refresh();
		}

		private void RemoveRuleButton_OnClick(object sender, RoutedEventArgs e)
		{
			prefixRules.Remove((MutableKeyValuePair) PrefixList.SelectedValue);
			PrefixList.ItemsSource = prefixRules;
			PrefixList.Items.Refresh();
		}

		private void OtherTypeCheckBox_OnChecked(object sender, RoutedEventArgs e)
		{
			OtherTypePanel.IsEnabled = !OtherTypeCheckBox.IsChecked.GetValueOrDefault(false);
		}
	}

	class MutableKeyValuePair
	{
		public MutableKeyValuePair(string key, string value)
		{
			Key = key;
			Value = value;
		}
		public string Key { get; set; }
		public string Value { get; set; }


	}
}
