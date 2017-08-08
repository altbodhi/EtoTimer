using System;

class Startup
{
	[STAThread]
	public static void Main(string[] args)
	{
		new Eto.Forms.Application().Run(new MyForm());
	}
}