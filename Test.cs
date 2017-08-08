using System;
using Eto.Forms;
using Eto.Serialization.Xaml;
using System.Windows.Input;
using System.Diagnostics;


	public class MyPanel : Panel
	{
		public class MyModel
		{
			string someText;
			public string SomeText
			{
				get { return someText; }
				set
				{
					someText = value;
					Debug.WriteLine(string.Format("Changed to {0}", someText));
				}
			}

			public ICommand ClickMe
			{
				get { return new Command((sender, e) => MessageBox.Show("Clicked!")); }
			}
		}

		protected CheckBox MyCheckBox { get; set; }
		protected TextBox textBoxName { get; set; }
        protected MyModel model;
		public MyPanel()
		{
			
			XamlReader.Load(this);


			model = new MyModel { SomeText = "Text from data model" };
			DataContext =  model;
		}

		protected void HandleButtonClick(object sender, EventArgs e)
		{
			MessageBox.Show(this, "I was clicked from Xaml!");
		}

		public void HandleTextChanged(object sender, EventArgs e)
		{
			Console.WriteLine(model.SomeText);
		}
	}
