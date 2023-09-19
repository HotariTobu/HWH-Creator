using SharedCSharp.Extension;
using SharedWinforms.Extension;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HWH_Creator
{
    public partial class TextBoxEditor : UserControl
    {
        public TextBoxEditor()
        {
            InitializeComponent();
            BackColor = Color.Transparent;
            foreach (Control control in Controls)
            {
                control.BackColor = Color.Transparent;
            }

            IsClosed = true;
        }

        public bool IsClosed { get; set; }
        public TagControls.TextBoxTag TextBoxTag { get; set; }
        public Size PageSize { get; set; }
        public Control Box { get; set; }
        public Action Finish { get; set; }
        public Action Delete { get; set; }

        public void Initialize()
        {
            if (TextBoxTag != null && Box != null && TextBoxTag.Image != null)
            {
                float hr = (float)Box.Width / PageSize.Width;
                float vr = (float)Box.Height / PageSize.Height;

                Location = new Point((int)(TextBoxTag.X * hr), (int)(TextBoxTag.Y * vr));
                Size = new Size((int)(TextBoxTag.Width * hr), (int)(TextBoxTag.Height * vr));

                IsClosed = false;
            }
            else
            {
                IsClosed = true;
                Finish?.Invoke();
            }
        }

        public void Close()
        {
            if (IsClosed == true)
            {
                return;
            }

            if (TextBoxTag != null && Box != null)
            {
                TextBoxTag.Rectangle = new RectangleF(
                    Location.X * (float)PageSize.Width / Box.Width,
                    Location.Y * (float)PageSize.Height / Box.Height,
                    TextBoxTag.Rectangle.Width,
                    TextBoxTag.Rectangle.Height
                    );
            }

            IsClosed = true;

            Finish?.Invoke();
        }

        protected override void OnLeave(EventArgs e)
        {
            Close();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Close();
                        break;
                    case Keys.Delete:
                        Delete();
                        break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (TextBoxTag.Image != null)
            {
                e.Graphics.DrawImage(TextBoxTag.Image, new Rectangle(0, 0, Width - 1, Height - 1));
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }

        private Point LastClientCursorPosition { get; set; }
        private Point LastLocation { get; set; }
        private long LastMoveTime { get; set; }
        private int Span => 40;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            LastClientCursorPosition = Parent?.PointToClient(Cursor.Position) ?? new Point();
            LastLocation = Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                if (new Rectangle(new Point(), Size).Contains(e.Location))
                {
                    Cursor = Cursors.SizeAll;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                long moveTime = DateTime.Now.GetMilli();
                if (moveTime - LastMoveTime > Span)
                {
                    Parent?.SuspendDrawing();
                    Rectangle lastBounds = Bounds;
                    Point dif = (Parent?.PointToClient(Cursor.Position) ?? LastClientCursorPosition).Sub(LastClientCursorPosition);

                    if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        dif.Y = 0;
                    }
                    if ((ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        dif.X = 0;
                    }
                    if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        dif.Y = Math.Abs(dif.X) * Math.Sign(dif.Y);
                    }

                    Location = LastLocation.Add(dif);
                    Parent?.ResumeDrawing(false);
                    Parent?.Invalidate(lastBounds);
                    Invalidate();

                    LastMoveTime = moveTime;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {

        }
    }
}
