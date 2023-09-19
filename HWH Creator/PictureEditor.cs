using SharedCSharp;
using SharedCSharp.Extension;
using SharedWinforms;
using SharedWinforms.Extension;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HWH_Creator
{
    public partial class PictureEditor : UserControl
    {
        public PictureEditor()
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
        public TagControls.PictureTag PictureTag { get; set; }
        public Size PageSize { get; set; }
        public Control Box { get; set; }
        public Action Finish { get; set; }
        public Action Delete { get; set; }

        private Image Image { get; set; }

        public void Initialize()
        {
            if (PictureTag != null && Box != null)
            {
                float hr = (float)Box.Width / PageSize.Width;
                float vr = (float)Box.Height / PageSize.Height;

                Location = new Point((int)(PictureTag.X * hr), (int)(PictureTag.Y * vr));
                Size = new Size((int)(PictureTag.Width * hr), (int)(PictureTag.Height * vr));

                try
                {
                    Image = ImageReader.ReadImage(PictureTag.Path);
                }
                catch (Exception e)
                {
                    FuncCenter.CallFunc((int)MainForm.FuncKeys.ExportException, e);
                    IsClosed = true;
                    Finish?.Invoke();
                    return;
                }

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

            if (PictureTag != null && Box != null)
            {
                float hr = (float)PageSize.Width / Box.Width;
                float vr = (float)PageSize.Height / Box.Height;

                PictureTag.Rectangle = new RectangleF(
                    Location.X * hr,
                    Location.Y * vr,
                    Width * hr,
                    Height * vr
                    );
            }

            IsClosed = true;

            Image?.Dispose();
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

        private int EdgeNarrowness => 4;
        private enum Edges
        {
            TopLeft,
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft,
            Left,
            None,
        }
        private (Rectangle, Edges, Cursor)[] EdgeRects => new (Rectangle, Edges, Cursor)[]
        {
            (new Rectangle(0, 0, EdgeNarrowness, EdgeNarrowness), Edges.TopLeft, Cursors.SizeNWSE),
            (new Rectangle(EdgeNarrowness, 0, Width - EdgeNarrowness * 2, EdgeNarrowness), Edges.Top, Cursors.SizeNS),
            (new Rectangle(Width - EdgeNarrowness, 0, EdgeNarrowness, EdgeNarrowness), Edges.TopRight, Cursors.SizeNESW),
            (new Rectangle(Width - EdgeNarrowness, EdgeNarrowness, EdgeNarrowness, Height - EdgeNarrowness * 2), Edges.Right, Cursors.SizeWE),
            (new Rectangle(Width - EdgeNarrowness, Height - EdgeNarrowness, EdgeNarrowness, EdgeNarrowness), Edges.BottomRight, Cursors.SizeNWSE),
            (new Rectangle(EdgeNarrowness, Height - EdgeNarrowness, Width - EdgeNarrowness * 2, EdgeNarrowness), Edges.Bottom, Cursors.SizeNS),
            (new Rectangle(0, Height - EdgeNarrowness, EdgeNarrowness, EdgeNarrowness), Edges.BottomLeft, Cursors.SizeNESW),
            (new Rectangle(0, EdgeNarrowness, EdgeNarrowness, Height - EdgeNarrowness * 2), Edges.Left, Cursors.SizeWE),
            (new Rectangle(EdgeNarrowness, EdgeNarrowness, Width - EdgeNarrowness * 2, Height - EdgeNarrowness * 2), Edges.None, Cursors.SizeAll),
        };
        private int EdgeBreadth => EdgeNarrowness * 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image != null)
            {
                e.Graphics.DrawImage(Image, new Rectangle(0, 0, Width - 1, Height - 1));

                Pen pen = Pens.Black.Clone() as Pen;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                Brush brush = Brushes.LightGray;
                Size size = new Size(EdgeBreadth * 2, EdgeBreadth * 2);

                e.Graphics.DrawEllipse(pen, -EdgeBreadth + EdgeNarrowness, -EdgeBreadth + EdgeNarrowness, size);
                e.Graphics.FillEllipse(brush, -EdgeBreadth + EdgeNarrowness, -EdgeBreadth + EdgeNarrowness, size);

                e.Graphics.DrawEllipse(pen, Width / 2 - EdgeBreadth, -EdgeBreadth + EdgeNarrowness, size);
                e.Graphics.FillEllipse(brush, Width / 2 - EdgeBreadth, -EdgeBreadth + EdgeNarrowness, size);

                e.Graphics.DrawEllipse(pen, Width - EdgeBreadth - EdgeNarrowness, -EdgeBreadth + EdgeNarrowness, size);
                e.Graphics.FillEllipse(brush, Width - EdgeBreadth - EdgeNarrowness, -EdgeBreadth + EdgeNarrowness, size);

                e.Graphics.DrawEllipse(pen, Width - EdgeBreadth - EdgeNarrowness, Height / 2 - EdgeBreadth, size);
                e.Graphics.FillEllipse(brush, Width - EdgeBreadth - EdgeNarrowness, Height / 2 - EdgeBreadth, size);

                e.Graphics.DrawEllipse(pen, Width - EdgeBreadth - EdgeNarrowness, Height - EdgeBreadth - EdgeNarrowness, size);
                e.Graphics.FillEllipse(brush, Width - EdgeBreadth - EdgeNarrowness, Height - EdgeBreadth - EdgeNarrowness, size);

                e.Graphics.DrawEllipse(pen, Width / 2 - EdgeBreadth, Height - EdgeBreadth - EdgeNarrowness, size);
                e.Graphics.FillEllipse(brush, Width / 2 - EdgeBreadth, Height - EdgeBreadth - EdgeNarrowness, size);

                e.Graphics.DrawEllipse(pen, -EdgeBreadth + EdgeNarrowness, Height - EdgeBreadth - EdgeNarrowness, size);
                e.Graphics.FillEllipse(brush, -EdgeBreadth + EdgeNarrowness, Height - EdgeBreadth - EdgeNarrowness, size);

                e.Graphics.DrawEllipse(pen, -EdgeBreadth + EdgeNarrowness, Height / 2 - EdgeBreadth, size);
                e.Graphics.FillEllipse(brush, -EdgeBreadth + EdgeNarrowness, Height / 2 - EdgeBreadth, size);

                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }

        private Edges Edge { get; set; }
        private Point LastClientCursorPosition { get; set; }
        private Point LastLocation { get; set; }
        private Size LastSize { get; set; }
        private long LastMoveTime { get; set; }
        private int Span => 40;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            LastClientCursorPosition = Parent?.PointToClient(Cursor.Position) ?? new Point();
            LastLocation = Location;
            LastSize = Size;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                foreach ((Rectangle, Edges, Cursor) edgeRect in EdgeRects)
                {
                    if (edgeRect.Item1.Contains(e.Location))
                    {
                        Edge = edgeRect.Item2;
                        Cursor = edgeRect.Item3;
                        return;
                    }
                }

                Edge = Edges.None;
                Cursor = Cursors.Default;
            }
            else
            {
                long moveTime = DateTime.Now.GetMilli();
                if (moveTime - LastMoveTime > Span)
                {
                    Parent?.SuspendDrawing();
                    Rectangle lastBounds = Bounds;
                    Point dif = (Parent?.PointToClient(Cursor.Position) ?? LastClientCursorPosition).Sub(LastClientCursorPosition);
                    Point point1 = LastLocation;
                    Point point2 = point1.Add(new Point(LastSize));

                    switch (Edge)
                    {
                        case Edges.TopLeft:
                            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                dif.Y = dif.X * Height / Width;
                            }
                            if ((ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                point2 = point2.Sub(dif);
                            }
                            if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                            {
                                dif.Y = dif.X;
                            }
                            point1 = point1.Add(dif);
                            break;
                        case Edges.Top:
                            point1.Y += dif.Y;
                            break;
                        case Edges.TopRight:
                            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                dif.Y = -dif.X * Height / Width;
                            }
                            if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                            {
                                dif.Y = -dif.X;
                            }
                            if ((ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                point1.X -= dif.X;
                                point2.Y -= dif.Y;
                            }
                            point1.Y += dif.Y;
                            point2.X += dif.X;
                            break;
                        case Edges.Right:
                            point2.X += dif.X;
                            break;
                        case Edges.BottomRight:
                            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                dif.Y = dif.X * Height / Width;
                            }
                            if ((ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                point1 = point1.Sub(dif);
                            }
                            if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                            {
                                dif.Y = dif.X;
                            }
                            point2 = point2.Add(dif);
                            break;
                        case Edges.Bottom:
                            point2.Y += dif.Y;
                            break;
                        case Edges.BottomLeft:
                            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                            {
                                dif.Y = -dif.X * Height / Width;
                            }
                            if ((ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                point1.Y -= dif.Y;
                                point2.X -= dif.X;
                            }
                            if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                            {
                                dif.Y = -dif.X;
                            }
                            point1.X += dif.X;
                            point2.Y += dif.Y;
                            break;
                        case Edges.Left:
                            point1.X += dif.X;
                            break;
                        case Edges.None:
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
                            point1 = point1.Add(dif);
                            point2 = point2.Add(dif);
                            break;
                        default:
                            break;
                    }

                    Bounds = point1.GetRectangle(point2);
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
