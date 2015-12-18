﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SettingsEditor.ViewModels;
using WinFormsMvvm.DialogService;

namespace SettingsEditor.Views
{
    public partial class FrmUidAbfrageSettingsView : FormService
    {
        public FrmUidAbfrageSettingsView(UidAbfrageSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            bindingSource1.DataSource = ViewModel;
        }
    }
}
