//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: ConVar List : Parses a given .txt which is passed in from the ServerForm, we display custom convars + tooltips for'em.
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DeadBread.Base;

namespace DeadBread.Controls
{
    public partial class ConVarList : UserControl
    {
        public bool m_bCanOpen;
        private string szCFGPath;
        public ConVarList(string game, string cfgPath)
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;

            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;

            if (!DesignMode)
                BackgroundImage = Globals.GetTextureImage("controls\\LoginBG.png");

            m_bCanOpen = false;
            szCFGPath = cfgPath;
            ParseConVarFile(game);
        }

        public void WriteConVarsToConfig()
        {
            if (!m_bCanOpen)
                return;

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is CheckBoxNew)
                    WriteToConfig(string.Format("{0} {1}", ((CheckBoxNew)Controls[i]).GetConVar(), ((CheckBoxNew)Controls[i]).IsChecked() ? 1 : 0));
                else
                {
                    switch (((NumericVar)Controls[i]).ValueType)
                    {
                        case 0:
                            WriteToConfig(string.Format("{0} {1}", ((NumericVar)Controls[i]).GetConVar(), ((NumericVar)Controls[i]).GetValueInt()));
                            break;

                        case 1:
                            WriteToConfig(string.Format("{0} {1}", ((NumericVar)Controls[i]).GetConVar(), ((NumericVar)Controls[i]).GetValueFloat()));
                            break;

                        case 2:
                            WriteToConfig(string.Format("{0} \"{1}\"", ((NumericVar)Controls[i]).GetConVar(), ((NumericVar)Controls[i]).GetValueString()));
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Write to the server.cfg file...
        /// </summary>
        /// <param name="text"></param>
        private void WriteToConfig(string text)
        {
            using (StreamWriter writer = new StreamWriter(szCFGPath, true))
            {
                writer.Write(text + Environment.NewLine);
            }
        }

        private void ParseConVarFile(string game)
        {
            try
            {
                using (StreamReader reader = new StreamReader(string.Format("{0}\\BaseLauncher\\config\\{1}_convars.txt", Globals.GetAppPath(), game)))
                {
                    m_bCanOpen = true;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        int iValue = 0;
                        string szValue = null;
                        float flValue = 0.0F;
                        int iYPos = 0;
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            if (iYPos < (Controls[i].Height + Controls[i].Bounds.Y))
                                iYPos = (Controls[i].Height + Controls[i].Bounds.Y);
                        }

                        if (line.Contains("BOOL"))
                        {
                            line = line.Replace("BOOL ", "");
                            iValue = int.Parse(line.Substring(0, line.IndexOf(" ")));
                            line = line.Replace(line.Substring(0, line.IndexOf(" ") + 1), "");
                            int indexToNextSpace = line.IndexOf(" ");
                            string szConVar = line.Substring(0, indexToNextSpace);
                            line = line.Replace(line.Substring(0, indexToNextSpace + 1), "");
                            int indexToBracket1 = line.IndexOf(">");

                            CheckBoxNew checkBox = new CheckBoxNew(line.Substring(1, indexToBracket1 - 1), szConVar, (iValue > 0) ? true : false);
                            checkBox.Parent = this;
                            checkBox.Bounds = new Rectangle(20, iYPos + 5, 150, 24);
                            checkBox.BringToFront();

                            line = line.Replace(line.Substring(0, indexToBracket1 + 2), "");
                            line = line.Replace(">", "");
                            line = line.Replace("<", "");

                            infoTip.SetToolTip(checkBox, line);
                        }
                        else if (line.Contains("INT"))
                        {
                            line = line.Replace("INT ", "");

                            iValue = int.Parse(line.Substring(0, line.IndexOf(" ")));
                            line = line.Replace(line.Substring(0, line.IndexOf(" ") + 1), "");
                            int indexToNextSpace = line.IndexOf(" ");
                            string szConVar = line.Substring(0, indexToNextSpace);
                            line = line.Replace(line.Substring(0, indexToNextSpace + 1), "");
                            int indexToBracket1 = line.IndexOf(">");

                            NumericVar numericBox = new NumericVar(line.Substring(1, indexToBracket1 - 1), szConVar, iValue);
                            numericBox.Parent = this;
                            numericBox.Bounds = new Rectangle(20, iYPos + 5, 300, 40);
                            numericBox.BringToFront();

                            line = line.Replace(line.Substring(0, indexToBracket1 + 2), "");
                            line = line.Replace(">", "");
                            line = line.Replace("<", "");

                            infoTip.SetToolTip(numericBox, line);
                        }
                        else if (line.Contains("FLOAT"))
                        {
                            line = line.Replace("FLOAT ", "");

                            flValue = float.Parse(line.Substring(0, line.IndexOf(" ")));
                            line = line.Replace(line.Substring(0, line.IndexOf(" ") + 1), "");
                            int indexToNextSpace = line.IndexOf(" ");
                            string szConVar = line.Substring(0, indexToNextSpace);
                            line = line.Replace(line.Substring(0, indexToNextSpace + 1), "");
                            int indexToBracket1 = line.IndexOf(">");

                            NumericVar numericBox = new NumericVar(line.Substring(1, indexToBracket1 - 1), szConVar, flValue);
                            numericBox.Parent = this;
                            numericBox.Bounds = new Rectangle(20, iYPos + 5, 300, 40);
                            numericBox.BringToFront();

                            line = line.Replace(line.Substring(0, indexToBracket1 + 2), "");
                            line = line.Replace(">", "");
                            line = line.Replace("<", "");

                            infoTip.SetToolTip(numericBox, line);
                        }
                        else if (line.Contains("STRING"))
                        {
                            line = line.Replace("STRING ", "");

                            szValue = line.Substring(0, line.IndexOf(" "));
                            line = line.Replace(line.Substring(0, line.IndexOf(" ") + 1), "");
                            int indexToNextSpace = line.IndexOf(" ");
                            string szConVar = line.Substring(0, indexToNextSpace);
                            line = line.Replace(line.Substring(0, indexToNextSpace + 1), "");
                            int indexToBracket1 = line.IndexOf(">");

                            NumericVar numericBox = new NumericVar(line.Substring(1, indexToBracket1 - 1), szConVar, szValue);
                            numericBox.Parent = this;
                            numericBox.Bounds = new Rectangle(20, iYPos + 5, 300, 40);
                            numericBox.BringToFront();

                            line = line.Replace(line.Substring(0, indexToBracket1 + 2), "");
                            line = line.Replace(">", "");
                            line = line.Replace("<", "");

                            infoTip.SetToolTip(numericBox, line);
                        }
                    }
                }
            }
            catch
            {
                m_bCanOpen = false;
                Visible = false;
            }
        }
    }
}
