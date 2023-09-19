using SharedCSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HWH_Creator
{
    partial class MainForm
    {
        /// <summary>
        /// HWHファイルを読み込み、内容から文章を構成します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>完了したらtrueを、それ以外はfalseを返します。</returns>
        private bool Read(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            Reset();
            MainPanel.Controls.Clear();

            try
            {
                XDocument document = XDocument.Load(path);
                if (document == null)
                {
                    throw new IOException("documentがnullでした。");
                }

                string formatVersion = document?.Root?.Attribute("Version")?.Value;
                if (formatVersion == null)
                {
                    formatVersion = document?.Root?.Element("Version")?.Value;
                    document?.Root?.Element("Version")?.Remove();
                }

                if (formatVersion == null)
                {
                    UpdateStatus("バージョン情報がないためファイルを読み込めませんでした。");
                    return false;
                }

                int tagTypeCount = (int)TagType.Count;
                Func<BaseTag>[] constructors = new Func<BaseTag>[tagTypeCount];
                for (int i = 0; i < tagTypeCount; i++)
                {
                    constructors[i] = Expression.Lambda<Func<BaseTag>>(Expression.New(BaseTag.Tags[i])).Compile();
                }

                switch (formatVersion)
                {
                    case "1.0":
                        foreach (XElement pageElement in document.Root.Elements())
                        {
                            TreeNode pageNode = new TreeNode(pageElement.Name.LocalName.Replace("_", string.Empty));
                            TagControls.PageTag pageTag = new TagControls.PageTag
                            {
                                Data = string.Join("\r\t\n", new string[] {
                                    $"Text = {pageElement.Attribute("Data").Value}",
                                    $"RedPenList = {pageElement.Attribute("RedPenList").Value.Replace(",", "\r,\n")}",
                                })
                            };
                            pageNode.Tag = pageTag;

                            void addNode(TreeNode node, XElement element)
                            {
                                foreach (XElement childElement in element.Elements())
                                {
                                    TreeNode childNode = new TreeNode(childElement.Name.LocalName.Replace("_", string.Empty));
                                    if (int.TryParse(childElement.Attribute("TagType")?.Value, out int childTagTypeIndex))
                                    {
                                        try
                                        {
                                            if (constructors[childTagTypeIndex]() is BaseTag childTag)
                                            {
                                                string data = childElement.Attribute("Data")?.Value;

                                                if ((TagType)childTagTypeIndex == TagType.Event)
                                                {
                                                    string[] dataList = data.Split(',');
                                                    if (dataList.Length >= 4)
                                                    {
                                                        data = string.Join("\r\t\n", new string[] {
                                                $"Text = {string.Join(",", dataList, 3, dataList.Length - 3)}",
                                                $"IsBC = {dataList[0]}",
                                                $"Year = {dataList[1]}",
                                                $"IsCentury = {false}",
                                                $"IsAbout = {dataList[2]}",
                                                $"IsYearRed = {false}",
                                            });
                                                    }
                                                }
                                                else if ((TagType)childTagTypeIndex == TagType.Period)
                                                {
                                                    string[] dataList = data.Split(',');
                                                    if (dataList.Length >= 7)
                                                    {
                                                        data = string.Join("\r\t\n", new string[] {
                                                $"Text = {string.Join(",", dataList, 6, dataList.Length - 6)}",
                                                $"IsBC1 = {dataList[0]}",
                                                $"IsBC2 = {dataList[3]}",
                                                $"Year1 = {dataList[1]}",
                                                $"Year2 = {dataList[4]}",
                                                $"IsCentury1 = {false}",
                                                $"IsCentury2 = {false}",
                                                $"IsAbout1 = {dataList[2]}",
                                                $"IsAbout2 = {dataList[5]}",
                                                $"IsYearRed1 = {false}",
                                                $"IsYearRed2 = {false}",
                                            });
                                                    }
                                                }
                                                else if ((TagType)childTagTypeIndex == TagType.RedPen
                                                         || (TagType)childTagTypeIndex == TagType.ExplainList
                                                         || (TagType)childTagTypeIndex == TagType.Element
                                                         || (TagType)childTagTypeIndex == TagType.ChangeAndInfluence
                                                         || (TagType)childTagTypeIndex == TagType.Supplement
                                                         || (TagType)childTagTypeIndex == TagType.Block
                                                         || (TagType)childTagTypeIndex == TagType.Headline)
                                                {
                                                    data = $"Text = {data}";
                                                }

                                                childTag.Data = data;
                                                childNode.Tag = childTag;
                                                node.Nodes.Add(childNode);
                                                childNode.ImageIndex = childTagTypeIndex;
                                                childNode.SelectedImageIndex = childTagTypeIndex;
                                                addNode(childNode, childElement);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            ExportException(e);
                                        }
                                    }
                                }
                            }

                            ContentsTree.Nodes.Add(pageNode);
                            addNode(pageNode, pageElement);

                            AddPageBox();
                            UpdateImage(ContentsTree.Nodes.Count - 1);
                        }
                        break;

                    case "1.1":
                        string[] lines = document?.Root?.Element("Options")?.Value?.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string line = lines[i];
                            int index = line.IndexOf('=');
                            if (index == -1)
                            {
                                continue;
                            }

                            string left = line.Substring(0, index).Trim();
                            string right = line.Substring(index).TrimStart('=', ' ');

                            if (left.Equals("Browser") && bool.TryParse(right, out bool browser))
                            {
                                lines[i] = $"Browser = {(browser ? 0 : 1)}";
                                break;
                            }
                        }
                        OptionForm.OptionsPort = lines;
                        document?.Root?.Element("Options")?.Remove();
                        ApplyOptions();

                        foreach (XElement pageElement in document.Root.Elements())
                        {
                            TreeNode pageNode = new TreeNode(pageElement.Name.LocalName.Replace("_", string.Empty));
                            TagControls.PageTag pageTag = new TagControls.PageTag
                            {
                                Data = string.Join("\r\t\n", new string[] {
                                    $"Text = {pageElement?.Attribute("Data")?.Value}",
                                    $"RedPenList = {pageElement?.Attribute("RedPenList")?.Value?.Replace(",", "\r,\n")}",
                                })
                            };
                            pageNode.Tag = pageTag;

                            void addNode(TreeNode node, XElement element)
                            {
                                foreach (XElement childElement in element.Elements())
                                {
                                    TreeNode childNode = new TreeNode(childElement.Name.LocalName.Replace("_", string.Empty));
                                    if (int.TryParse(childElement.Attribute("TagType")?.Value, out int childTagTypeIndex))
                                    {
                                        try
                                        {
                                            if (constructors[childTagTypeIndex]() is BaseTag childTag)
                                            {
                                                string data = childElement.Attribute("Data")?.Value;

                                                if ((TagType)childTagTypeIndex == TagType.Event)
                                                {
                                                    string[] dataList = data.Split(',');
                                                    if (dataList.Length >= 5)
                                                    {
                                                        data = string.Join("\r\t\n", new string[] {
                                                $"Text = {string.Join(",", dataList, 4, dataList.Length - 4)}",
                                                $"IsBC = {dataList[0]}",
                                                $"Year = {dataList[1]}",
                                                $"IsCentury = {dataList[2].Equals("1")}",
                                                $"IsAbout = {dataList[3]}",
                                                $"IsYearRed = {false}",
                                            });
                                                    }
                                                }
                                                else if ((TagType)childTagTypeIndex == TagType.Period)
                                                {
                                                    string[] dataList = data.Split(',');
                                                    if (dataList.Length >= 9)
                                                    {
                                                        data = string.Join("\r\t\n", new string[] {
                                                $"Text = {string.Join(",", dataList, 8, dataList.Length - 9)}",
                                                $"IsBC1 = {dataList[0]}",
                                                $"IsBC2 = {dataList[4]}",
                                                $"Year1 = {dataList[1]}",
                                                $"Year2 = {dataList[5]}",
                                                $"IsCentury1 = {dataList[2].Equals("1")}",
                                                $"IsCentury2 = {dataList[6].Equals("1")}",
                                                $"IsAbout1 = {dataList[3]}",
                                                $"IsAbout2 = {dataList[7]}",
                                                $"IsYearRed1 = {false}",
                                                $"IsYearRed2 = {false}",
                                            });
                                                    }
                                                }
                                                else if ((TagType)childTagTypeIndex == TagType.RedPen
                                                         || (TagType)childTagTypeIndex == TagType.ExplainList
                                                         || (TagType)childTagTypeIndex == TagType.Element
                                                         || (TagType)childTagTypeIndex == TagType.ChangeAndInfluence
                                                         || (TagType)childTagTypeIndex == TagType.Supplement
                                                         || (TagType)childTagTypeIndex == TagType.Block
                                                         || (TagType)childTagTypeIndex == TagType.Headline)
                                                {
                                                    data = $"Text = {data}";
                                                }

                                                childTag.Data = data;
                                                childNode.Tag = childTag;
                                                node.Nodes.Add(childNode);
                                                childNode.ImageIndex = childTagTypeIndex;
                                                childNode.SelectedImageIndex = childTagTypeIndex;
                                                addNode(childNode, childElement);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            ExportException(e);
                                        }
                                    }
                                }
                            }

                            ContentsTree.Nodes.Add(pageNode);
                            pageNode.ImageKey = "Page.png";
                            pageNode.SelectedImageKey = "Page.png";
                            addNode(pageNode, pageElement);

                            AddPageBox();
                            UpdateImage(ContentsTree.Nodes.Count - 1);
                        }
                        break;

                    case "1.2":
                        OptionForm.OptionsPort = document?.Root?.Attribute("Options")?.Value?.Split(new string[] { "\r\t\n" }, StringSplitOptions.None);
                        ApplyOptions();

                        foreach (XElement pageElement in document.Root.Elements())
                        {
                            if (!int.TryParse(pageElement.Attribute("TagType")?.Value, out int result) || result != (int)TagType.Page)
                            {
                                continue;
                            }

                            TreeNode pageNode = new TreeNode(pageElement.Name.LocalName.Replace("_", string.Empty))
                            {
                                Tag = new TagControls.PageTag { Data = pageElement.Attribute("Data")?.Value }
                            };

                            void addNode(TreeNode node, XElement element)
                            {
                                foreach (XElement childElement in element.Elements())
                                {
                                    TreeNode childNode = new TreeNode(childElement.Name.LocalName.Replace("_", string.Empty));
                                    if (int.TryParse(childElement.Attribute("TagType")?.Value, out int childTagTypeIndex))
                                    {
                                        try
                                        {
                                            if (constructors[childTagTypeIndex]() is BaseTag childTag)
                                            {
                                                string data = childElement.Attribute("Data")?.Value;

                                                if ((TagType)childTagTypeIndex == TagType.RedPen
                                                         || (TagType)childTagTypeIndex == TagType.ExplainList
                                                         || (TagType)childTagTypeIndex == TagType.Element
                                                         || (TagType)childTagTypeIndex == TagType.ChangeAndInfluence
                                                         || (TagType)childTagTypeIndex == TagType.Supplement
                                                         || (TagType)childTagTypeIndex == TagType.Block
                                                         || (TagType)childTagTypeIndex == TagType.Headline)
                                                {
                                                    data = $"Text = {data}";
                                                }

                                                childTag.Data = data;
                                                childNode.Tag = childTag;
                                                node.Nodes.Add(childNode);
                                                childNode.ImageIndex = childTagTypeIndex;
                                                childNode.SelectedImageIndex = childTagTypeIndex;
                                                addNode(childNode, childElement);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            ExportException(e);
                                        }
                                    }
                                }
                            }

                            ContentsTree.Nodes.Add(pageNode);
                            pageNode.ImageKey = "Page.png";
                            pageNode.SelectedImageKey = "Page.png";
                            addNode(pageNode, pageElement);

                            AddPageBox();
                            UpdateImage(ContentsTree.Nodes.Count - 1);
                        }
                        break;

                    case "1.3":
                        OptionForm.OptionsPort = document?.Root?.Attribute("Options")?.Value?.Split(new string[] { "\r\t\n" }, StringSplitOptions.None);
                        ApplyOptions();

                        foreach (XElement pageElement in document.Root.Elements())
                        {
                            if (!int.TryParse(pageElement.Attribute("TagType")?.Value, out int result) || result != (int)TagType.Page)
                            {
                                continue;
                            }

                            TreeNode pageNode = new TreeNode(pageElement.Name.LocalName.Replace("_", string.Empty))
                            {
                                Tag = new TagControls.PageTag { Data = pageElement.Attribute("Data")?.Value }
                            };

                            void addNode(TreeNode node, XElement element)
                            {
                                foreach (XElement childElement in element.Elements())
                                {
                                    TreeNode childNode = new TreeNode(childElement.Name.LocalName.Replace("_", string.Empty));
                                    if (int.TryParse(childElement.Attribute("TagType")?.Value, out int childTagTypeIndex))
                                    {
                                        try
                                        {
                                            if (constructors[childTagTypeIndex]() is BaseTag childTag)
                                            {
                                                childTag.Data = childElement.Attribute("Data").Value;
                                                childNode.Tag = childTag;
                                                node.Nodes.Add(childNode);
                                                childNode.ImageIndex = childTagTypeIndex;
                                                childNode.SelectedImageIndex = childTagTypeIndex;
                                                addNode(childNode, childElement);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            ExportException(e);
                                        }
                                    }
                                }
                            }

                            ContentsTree.Nodes.Add(pageNode);
                            pageNode.ImageKey = "Page.png";
                            pageNode.SelectedImageKey = "Page.png";
                            addNode(pageNode, pageElement);

                            AddPageBox();
                            UpdateImage(ContentsTree.Nodes.Count - 1);
                        }
                        break;
                }

                this.FilePath = path;
                Text += $" - {Path.GetFileName(path)}";

                UpdateStatus("ファイルを読み込みました。");

                return true;
            }
            catch (Exception e)
            {
                ExportException(e);
            }

            return false;
        }

        private bool Write()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                return false;
            }

            try
            {
                XElement root = new XElement("Root");
                XDocument document = new XDocument(root);

                root.SetAttributeValue("Version", Properties.Resources.FormatVersion);
                root.SetAttributeValue("Options", string.Join("\r\t\n", OptionForm.OptionsPort));

                foreach (TreeNode pageNode in ContentsTree.Nodes)
                {
                    if (pageNode.Tag is BaseTag pageTag)
                    {
                        XElement pageElement = new XElement($"_{pageNode.Text}");
                        pageElement.SetAttributeValue("TagType", (int)pageTag.Type);
                        pageElement.SetAttributeValue("Data", pageTag.Data ?? string.Empty);

                        void addNode(XElement element, TreeNode node)
                        {
                            foreach (TreeNode childNode in node.Nodes)
                            {
                                if (childNode.Tag is BaseTag childTag)
                                {
                                    XElement childElement = new XElement($"_{childNode.Text}");
                                    childElement.SetAttributeValue("TagType", (int)childTag.Type);
                                    childElement.SetAttributeValue("Data", childTag?.Data ?? string.Empty);
                                    element.Add(childElement);
                                    addNode(childElement, childNode);
                                }
                            }
                        }

                        addNode(pageElement, pageNode);
                        root.Add(pageElement);
                    }
                }

                document.Save(FilePath);

                Edited = false;

                UpdateStatus("ファイルに保存しました。");

                return true;
            }
            catch (Exception e)
            {
                ExportException(e);
            }

            return false;
        }
    }
}
