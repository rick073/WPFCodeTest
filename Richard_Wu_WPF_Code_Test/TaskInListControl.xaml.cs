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

namespace Richard_Wu_WPF_Code_Test
{
	/// <summary>
	/// Interaction logic for TaskInListControl.xaml
	/// </summary>
	public partial class TaskInListControl : UserControl
	{
		public string taskName = "";
		public string taskDate = "";
		public bool done = false;
		public event EventHandler editButtonClicked;
		public event EventHandler doneButtonClicked;
		public event EventHandler deleteButtonClicked;
		public TaskInListControl(string name, string date)
		{
			InitializeComponent();

			//Set variables
			this.taskName = name;
			this.taskDate = date;
			this.nameLabel.Content = taskName;
			this.dateLabel.Content = taskDate;

			//Raise events to host
			editButton.Click += (s, e) =>
			{
				if (editButtonClicked != null)
					editButtonClicked(this, e);
			};
			doneButton.Click += (s, e) =>
			{
				if (doneButtonClicked != null)
					doneButtonClicked(this, e);
			};
			deleteButton.Click += (s, e) =>
			{
				if (deleteButtonClicked != null)
					deleteButtonClicked(this, e);
			};
		}
	}
}
