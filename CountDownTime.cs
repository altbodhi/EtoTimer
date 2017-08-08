using System;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using System.Media;
using System.IO;
using System.Resources;

public class CountDownTime
{
    [STAThread]
    public static void Main()
    {
        MainForm.Run();
    }
}

public class MainForm : Form
{
    protected CheckBox periodic = new CheckBox();
    protected NumericUpDown minutes=new NumericUpDown();
    protected NumericUpDown seconds = new NumericUpDown();
    protected UITimer  timer = new UITimer();
    protected Button startButton = new Button();
    protected Button stopButton = new Button();
    ProgressBar progress = new ProgressBar();
    Label updateInfo = new Label();
    bool isRun = false;
    bool isBreak = false;
    private void ChangeState()
    {
        stopButton.Enabled = isRun;
        startButton.Enabled = !isRun;
    }
    public MainForm()
    {
        Icon = Icon.FromResource("icon.ico",typeof(MainForm).Assembly);
        ClientSize = new Size(640, 120);
        Title = "Таймер .Net";
        startButton.Text ="Пуск";
        stopButton.Text = "Стоп";
        ChangeState();
        minutes.MinValue = 0;
        minutes.Value = 0;
        minutes.MaxValue = 99;
        seconds.MinValue = 0;
        seconds.MaxValue = 60;
        seconds.Value = 15;
        periodic.Text = "Повторять";
        var layout = new DynamicLayout();
updateInfo.Text = "00:00";
layout.BeginVertical (); // fields section
layout.AddRow (updateInfo," ",progress);
layout.EndVertical ();
layout.EndVertical ();
        Content = new TableLayout
{
	Padding = new Padding(10), // padding around cells
	Spacing = new Size(5, 5),   // spacing between each cell
	Rows =
	{
      new TableRow(  new StackLayout
        {
            Spacing = 5,
            Orientation = Orientation.Horizontal,
            Items = {
               new Label {Text = "Минуты:"}, minutes, new Label {Text = "Секунды:"}, seconds, periodic, startButton,stopButton
            }
        }),
        layout
    }
};
        startButton.Click += StartTimer;
        stopButton.Click += (o,e) => {isBreak = true; Finish();};
        timer.Elapsed += Update_Elapsed;
        timer.Interval = 1;
    }
    
    int  countSeconds;
    protected void StartTimer(object sender, EventArgs e)
    {
        isBreak = false;
        isRun = true;
        ChangeState();
        countUp = 0;
        countSeconds = Convert.ToInt32(minutes.Value)*60+ Convert.ToInt32(seconds.Value);
        progress.MaxValue = countSeconds;
        progress.MinValue = 0;
        progress.Value = 0;
        timer.Start();
    }
 
    int countUp = 0;

    protected void Update_Elapsed(object sender, EventArgs e)
    {
        countUp++;
        progress.Value = countUp;
        var delta = countSeconds - countUp;
        int min = delta/60;
        int sec = delta%60;
        updateInfo.Text = string.Format("{0:00}:{1:00}",min,sec);
        if( countUp >( countSeconds-1) )
            Finish();
    }
    protected void Finish()
    {
        isRun = false;
        ChangeState();
        timer.Stop();
        if(isBreak || !periodic.Checked.Value)
        {
            new SoundPlayer("sound.wav").Play();
            MessageBox.Show("Обратный отсчет завершен.");
        }else
        {
            new SoundPlayer("sound.wav").PlaySync();
            this.StartTimer(this,EventArgs.Empty);
        }
            
            
    }

    public static void  Run()
    {
        new Application().Run(new MainForm());
    }
}