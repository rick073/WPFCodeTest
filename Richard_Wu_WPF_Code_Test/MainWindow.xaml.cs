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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public DateTime dateNow = DateTime.Now.Date;
		public MainWindow()
		{
			InitializeComponent();

			//See current date in application
			dateLabel.Content = "Current Date: " + dateNow.ToString("d");

			//Set add button click
			addButton.Click += AddButtonClick;
		}
		private void AddButtonClick(object sender, RoutedEventArgs e)
		{
			//Check if task name box has a name
			if(!taskNameBox.Text.Equals("Task Name"))
			{
				DateTime tempDate;
				string tempDateString;
				bool late = false;

				//Parse Date
				if (DateTime.TryParse(taskDateBox.Text, out tempDate))
				{
					tempDateString = tempDate.ToString("d");

					//Check if task due date is before current date
					if (DateTime.Compare(tempDate, dateNow) < 0)
					{
						late = true;
					}
				}
				else
				{
					tempDateString = "";
				}

				//Create new task
				TaskInListControl tempTask = new TaskInListControl(taskNameBox.Text, tempDateString);

				//Change background if the new task is already overdue
				if(late)
				{
					tempTask.grid.Background = new SolidColorBrush(Colors.Red);
				}

				//Add task to list of tasks
				taskPanel.Children.Add(tempTask);

				//Set task button event handlers accordingly
				tempTask.editButtonClicked += EditButtonClick;
				tempTask.doneButtonClicked += DoneButtonClick;
				tempTask.deleteButtonClicked += DeleteButtonClick;

				//Reset text boxes
				taskNameBox.Text = "Task Name";
				taskDateBox.Text = "Task Due Date";
			}
		}
		private void EditButtonClick(object sender, EventArgs e)
		{
			//Get task whose edit button was pressed
			TaskInListControl tempTask = sender as TaskInListControl;
			
			//Check if task name box has a different name to the existing task
			if(!taskNameBox.Text.Equals(tempTask.nameLabel.Content) || !taskDateBox.Text.Equals("Task Due Date"))
			{
				DateTime tempDate;
				string tempDateString;

				//Parse Date, change task due date if applicable
				if(DateTime.TryParse(taskDateBox.Text, out tempDate))
				{
					tempDateString = tempDate.ToString("d");
					tempTask.dateLabel.Content = taskDateBox.Text;

					//If task is already done, don't change color
					if (tempTask.done) ;
					//Change background if the new date makes the task overdue
					else if (DateTime.Compare(tempDate, dateNow) < 0)
					{
						tempTask.grid.Background = new SolidColorBrush(Colors.Red);
					}
					//Change background if the new date makes the task no longer overdue
					else
					{
						tempTask.grid.Background = new SolidColorBrush(Colors.White);
					}
				}

				//Change task name if applicable
				if(!taskNameBox.Text.Equals("Task Name"))
				{
					tempTask.nameLabel.Content = taskNameBox.Text;
				}

				//Reset text boxes
				taskNameBox.Text = "Task Name";
				taskDateBox.Text = "Task Due Date";
			}
		}
		private void DoneButtonClick(object sender, EventArgs e)
		{
			//Get task whose done button was pressed
			TaskInListControl tempTask = sender as TaskInListControl;

			//Change background to green
			tempTask.grid.Background = new SolidColorBrush(Colors.Green);

			tempTask.done = true;
		}
		private void DeleteButtonClick(object sender, EventArgs e)
		{
			//Get task whose delete button was pressed
			TaskInListControl tempTask = sender as TaskInListControl;

			//Remove task from panel
			taskPanel.Children.Remove(tempTask);
		}
	}
}
