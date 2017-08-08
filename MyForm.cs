using System;
using Eto.Forms;

class MyForm : Form
{
	public MyForm()
	{
		// sets the client (inner) size of the window for your content
		this.ClientSize = new Eto.Drawing.Size(600, 400);

		this.Title = "Hello, Eto.Forms";
Menu = new MenuBar
{
	Items =
	{
		new ButtonMenuItem
		{ 
			Text = "&File",
			Items =
			{ 
				// you can add commands or menu items
				new Command((sender, e) => Application.Instance.Quit())
					{ 
						MenuText = "Quit", 
						Shortcut = Application.Instance.CommonModifier | Keys.Q
				},
				// another menu item, not based off a Command
				new ButtonMenuItem { Text = "Click Me, MenuItem" }
			}
		} 
	}
};
     this.Content = new MyPanel();
	}
}