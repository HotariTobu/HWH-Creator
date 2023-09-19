using HWH_Creator.TagControls;
using System;

namespace HWH_Creator
{
    public enum TagType
    {
        RedPen,
        ExplainList,
        Element,
        Supplement,
        ChangeAndInfluence,
        Period,
        Event,
        Block,
        Headline,
        Page,
        Picture,
        TextBox,
        Count,
    }

    public abstract class BaseTag
    {
        public static Type[] Tags =
    {
            typeof(RedPenTag),
            typeof(ExplainListTag),
            typeof(ElementTag),
            typeof(SupplementTag),
            typeof(ChangeAndInfluenceTag),
            typeof(PeriodTag),
            typeof(EventTag),
            typeof(BlockTag),
            typeof(HeadlineTag),
            typeof(PageTag),
            typeof(PictureTag),
            typeof(TextBoxTag),
    };

        public abstract string Name { get; }
        public abstract TagType Type { get; }
        public abstract string DialogText { get; }
        public abstract string Data { get; set; }

        /// <summary>
        /// フォームの内容をタグのデータに適用させます。
        /// </summary>
        /// <returns>
        /// 適用できた場合にtrue、それ以外はfalseです。
        /// </returns>
        public abstract bool ApplyContents();
        public abstract System.Windows.Forms.Control InitializeControl();

        public string Text { get; set; }
        public System.Drawing.RectangleF Rectangle { get; set; }
    }
}
