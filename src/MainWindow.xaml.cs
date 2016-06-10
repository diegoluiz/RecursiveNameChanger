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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolutionNameChanger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Members
		public static readonly DependencyProperty OldPathProperty =DependencyProperty.Register("FilesPath", typeof(string), typeof(MainWindow), new PropertyMetadata(""));
		public static readonly DependencyProperty NewTextProperty = DependencyProperty.Register("NewText", typeof(string), typeof(MainWindow), new PropertyMetadata(""));
		public static readonly DependencyProperty OldTextProperty = DependencyProperty.Register("OldText", typeof(string), typeof(MainWindow), new PropertyMetadata(""));
		public static readonly DependencyProperty OutputTextProperty = DependencyProperty.Register("OutputText", typeof(string), typeof(MainWindow), new PropertyMetadata(""));
		#endregion

		#region Properties
		public string FilePath
		{
			get { return (string)GetValue(OldPathProperty); }
			set { SetValue(OldPathProperty, value); }
		}

		public string NewText
		{
			get { return (string)GetValue(NewTextProperty); }
			set { SetValue(NewTextProperty, value); }
		}

		public string OldText
		{
			get { return (string)GetValue(OldTextProperty); }
			set { SetValue(OldTextProperty, value); }
		}

		public string OutputText
		{
			get { return (string)GetValue(OutputTextProperty); }
			set { SetValue(OutputTextProperty, value); }
		}
		#endregion

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!Directory.Exists(FilePath))
			{
				MessageBox.Show(string.Format("Invalid path [{0}]", FilePath), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var files = Directory.GetFiles(FilePath, "*", SearchOption.AllDirectories);

			foreach (var file in files)
			{
				string filePath = file;

				string content;
				content = File.ReadAllText(filePath);

				content = content.Replace(OldText, NewText);

				File.Delete(filePath);
				if (System.IO.Path.GetFileNameWithoutExtension(file).Trim() == OldText.Trim())
					filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(file), NewText + System.IO.Path.GetExtension(file));

				File.WriteAllText(filePath, content);

				OutputText += string.Format("{0}File [{1}] changed.", Environment.NewLine, filePath);
			}
		}
	}
}
