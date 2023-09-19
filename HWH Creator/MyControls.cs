using SharedCSharp;
using System;
using System.Windows.Forms;

namespace HWH_Creator
{
    public class MyTextBox : TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                string text = SelectedText;
                FuncCenter.CallFunc((int)MainForm.FuncKeys.AddRedPenToList, text);

                FuncCenter.CallFunc((int)MainForm.FuncKeys.RedPenIndexOf, text, out object result1);
                if (result1 is int index && index != -1)
                {
                    string redPen = $"<{index + 1}>";
                    FuncCenter.CallFunc((int)MainForm.FuncKeys.GetOptions, null, out object result2);
                    if (result2 is OptionForm.Context options)
                    {
                        if (options.RedPenAddConvert)
                        {
                            Text = Text.Replace(text, redPen);
                            SelectionStart = TextLength;
                        }
                        else
                        {
                            SelectedText = redPen;
                        }
                    }
                }

                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (e.KeyChar)
                {
                    case ' ':
                        FuncCenter.CallFunc((int)MainForm.FuncKeys.SearchText, SelectedText);
                        e.Handled = true;
                        break;
                    case '\n':
                        FindForm()?.AcceptButton?.PerformClick();
                        e.Handled = true;
                        break;
                }
            }

            base.OnKeyPress(e);
        }
    }

    public class MyPanel : FlowLayoutPanel
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                VScroll = false;
                HScroll = false;
                base.OnMouseWheel(e);
                VScroll = true;
                HScroll = true;
            }
            else if (VScroll && (ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                VScroll = false;
                base.OnMouseWheel(e);
                VScroll = true;
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }
    }

    public class MyTreeView : TreeView
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203)
            {
                int lParam = m.LParam.ToInt32();
                int x = lParam & 0xFFFF;
                int y = (lParam >> 16) & 0xFFFF;
                base.OnNodeMouseDoubleClick(new TreeNodeMouseClickEventArgs(base.GetNodeAt(x, y), MouseButtons.Left, 2, x, y));
                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    };
}
