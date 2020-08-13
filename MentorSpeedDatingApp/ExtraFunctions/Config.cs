﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using Telerik.Windows.Controls;

namespace MentorSpeedDatingApp.ExtraFunctions
{
    public class Config
    {
        #region Properties

        public string AppSaveFileFolder { get; set; }
        public string AppSaveFileName { get; set; }

        public string AppDefaultFolder { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MSDAPP");
        public string AppDefaultFileName { get; } = "savedData";
        public string AppDefaultCombination { get; } =
            Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MSDAPP") , "savedData.json");

        public string AppConfigPath { get; } =
            Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MSDAPP"), "config.json");

        #endregion


        public Config()
        {
            this.AppSaveFileFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MSDAPP");
            this.AppSaveFileName = "savedData.json";
        }

        public string CombineAppPaths()
        {
            return Path.Combine(this.AppSaveFileFolder, this.AppSaveFileName);
        }

        public void ResetToDefault()
        {
            this.AppSaveFileFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MSDAPP");
            this.AppSaveFileName = "savedData.json";
        }

        public override string ToString()
        {
            return this.AppSaveFileFolder + this.AppSaveFileName;
        }
    }
}