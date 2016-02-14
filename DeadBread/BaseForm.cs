//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Main Form - Invisible in order to prevent thread issues related to closing/opening the main form.
//
//=============================================================================================//

using DeadBread.Base;
using DeadBread.Controls;
using DeadBread.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread
{
    public partial class BaseForm : Form
    {
        public GameForm GetGameForm() { return _GameForm; }
        private GameForm _GameForm = null;
        public BaseForm()
        {
            InitializeComponent();

            UpdateForm run_update = new UpdateForm();
            run_update.Show();
        }

        public void InitBaseLauncher()
        {
            _GameForm = new GameForm(this);
            _GameForm.Show();
        }
    }
}
