using System;
using Gtk;
using Gdk;

namespace Brewit
{
    public partial class MainWindow : Gtk.Window
    {
        int RemainingTime { get; set; }
        System.Timers.Timer Timer { get; set; }

        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            Build();
            ShowAll();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        protected void OnStartTimer(object sender, EventArgs e)
        {
            RemainingTime = (int)(SpinButtonMinutes.Value * 60 + SpinButtonSeconds.Value);
            UISetupBeforeCountdown();
            Timer = new System.Timers.Timer(1000)
            {
                AutoReset = true
            };
            Timer.Elapsed += (src, evt) => Application.Invoke(OnTimeElapsed);
            Timer.Start();
        }

        protected void OnTimeElapsed(object src, EventArgs evt)
        {
            if (RemainingTime <= 0)
            {
                Timer.Stop();
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
            ButtonStartTimer.Sensitive = false;
            EntryMessage.Sensitive = false;
            SpinButtonMinutes.Sensitive = false;
            SpinButtonSeconds.Sensitive = false;
            //
            UpdateDisplay(RemainingTime);
            //
            LabelTimeCountDown.Show();
        }

        protected void UISetupAfterCountdown()
        {
            LabelTimeCountDown.Hide();
            //
            ButtonStartTimer.Sensitive = true;
            EntryMessage.Sensitive = true;
            SpinButtonMinutes.Sensitive = true;
            SpinButtonSeconds.Sensitive = true;
        }

        protected void NotifyUser()
        {
            var dlg = new MessageDialog(this, DialogFlags.Modal,
                                        MessageType.Info, ButtonsType.Close,
                                        EntryMessage.Text);
            dlg.Run();
            dlg.Destroy();
        }

        protected void UpdateDisplay(int remainingTime)
        {
            var mins = remainingTime / 60;
            var secs = remainingTime - mins * 60;
            LabelTimeCountDown.Text = $"{mins.ToString("D2")}:{secs.ToString("D2")}";
        }
    }
}

