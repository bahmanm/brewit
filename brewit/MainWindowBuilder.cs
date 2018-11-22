using Gtk;

namespace Brewit
{
    public partial class MainWindow : Gtk.Window
    {
        Fixed Fixed { get; set; }
        SpinButton SpinButtonMinutes { get; set; }
        SpinButton SpinButtonSeconds { get; set; }
        Button ButtonStartTimer { get; set; }
        Entry EntryMessage { get; set; }
        Label LabelTimeCountDown { get; set; }
        Table Layout { get; set; }

        protected void Build()
        {
            Title = Mono.Unix.Catalog.GetString("Brew It!");
            WindowPosition = ((WindowPosition)(4));
            DeleteEvent += OnDeleteEvent;
            //
            SpinButtonMinutes = new SpinButton(0D, 100D, 1D)
            {
                CanFocus = true,
                ClimbRate = 1D,
                Numeric = true,
                Value = 0D
            };
            SpinButtonMinutes.Adjustment.PageIncrement = 10D;
            //
            var labelMinutes = new Label
            {
                LabelProp = Mono.Unix.Catalog.GetString("Minutes")
            };
            //
            SpinButtonSeconds= new SpinButton(0D, 100D, 1D)
            {
                CanFocus = true,
                ClimbRate = 1D,
                Numeric = true,
                Value = 5D
            };
            SpinButtonMinutes.Adjustment.PageIncrement = 10D;
            //
            var labelSeconds = new Label
            {
                LabelProp = Mono.Unix.Catalog.GetString("Seconds")
            };
            //
            ButtonStartTimer = new Button
            {
                CanFocus = true,
                UseUnderline = true,
                Label = Mono.Unix.Catalog.GetString("Start")
            };
            ButtonStartTimer.Clicked += OnStartTimer;
            // 
            EntryMessage = new Entry
            {
                CanFocus = true,
                Text = Mono.Unix.Catalog.GetString("Brew time!"),
                IsEditable = true,
                InvisibleChar = '•'
            };
            // 
            LabelTimeCountDown = new Label()
            {
                Visible = false
            };
            var fdesc = LabelTimeCountDown.PangoContext.FontDescription;
            fdesc.Weight = Pango.Weight.Bold;
            fdesc.Family = "Monospace";
            fdesc.Size = (int)(18 * Pango.Scale.PangoScale);
            LabelTimeCountDown.ModifyFont(fdesc);
            //
            Layout = new Table(4, 5, false)
            {
                ColumnSpacing = 2,
                RowSpacing = 2
            };
            Layout.Attach(labelMinutes, 0, 1, 0, 1);
            Layout.Attach(SpinButtonMinutes, 1, 2, 0, 1);
            Layout.Attach(labelSeconds, 3, 4, 0, 1);
            Layout.Attach(SpinButtonSeconds, 4, 5, 0, 1);
            Layout.Attach(EntryMessage, 1, 4, 1, 2);
            Layout.Attach(ButtonStartTimer, 2, 3, 2, 3);
            Layout.Attach(LabelTimeCountDown, 1, 4, 3, 4);
            //
            Add(Layout);
            if ((Child != null))
            {
                Child.ShowAll();
            }
            DefaultWidth = 347;
            DefaultHeight = 263;
        }
    }
}
