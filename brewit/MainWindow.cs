using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    int RemainingTime { get; set; }
    System.Timers.Timer Timer { get; set; }

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        //var fontdesc = PangoContext.FontDescription;
        //fontdesc.Family = "Ubuntu";
        //fontdesc.Size = 14;
        //fontdesc.Weight = Pango.Weight.Bold;
        //ButtonStartTimer.ModifyFont(fontdesc);
        //LabelTimeCountDown.ModifyFont(fontdesc);
        ShowAll();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnStartTimer(object sender, EventArgs e)
    {
        RemainingTime = (int)SpinButtonMinutes.Value;
        UISetupBeforeCountdown();
        Timer = new System.Timers.Timer(1000)
        {
            AutoReset = true,
            Enabled = true
        };
        Timer.Elapsed += (src, evt) => Application.Invoke(OnTimeElapsed);
    }

    protected void OnTimeElapsed(object src, EventArgs evt)
    {
        if (RemainingTime == 0)
        {
            Timer.Enabled = false;
            NotifyUser();
            UISetupAfterCountdown();
        }
        else
        {
            RemainingTime -= 1;
            UpdateDisplay(RemainingTime);
        }
    }

    protected void UISetupBeforeCountdown() 
    {
        //ButtonStartTimer.Sensitive = false;
        //LabelTimeCountDown.Visible = true;
        //EntryMessage.Sensitive = false;
        Fixed.Sensitive = false;
        LabelTimeCountDown.Sensitive = true;
        UpdateDisplay(RemainingTime);
    }

    protected void UISetupAfterCountdown() 
    {
        ButtonStartTimer.Sensitive = true;
        LabelTimeCountDown.Visible = true;
        EntryMessage.Sensitive = true;
    }

    protected void NotifyUser() {
        var dlg = new MessageDialog(this, DialogFlags.Modal,
                                    MessageType.Info, ButtonsType.Close,
                                    EntryMessage.Text);
        dlg.Run();
        dlg.Destroy();
    }

    protected void UpdateDisplay(int remainingTime)
    {
        LabelTimeCountDown.Text = $"{remainingTime}";
    }
}
