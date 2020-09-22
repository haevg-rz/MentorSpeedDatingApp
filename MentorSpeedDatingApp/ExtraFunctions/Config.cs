using System;
using System.IO;

namespace MentorSpeedDatingApp.ExtraFunctions
{
    public class Config
    {
        #region Properties

        public string AppSaveFileFolder { get; set; }
        public string AppSaveFileName { get; set; }

        public string AppConfigPath { get; } =
            Path.Combine(
                Path.Combine(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "MentorSpeedDatingApp"), "Config"),
                "config.json");

        public string AppConfigFolderPath { get; } =
            Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "MentorSpeedDatingApp"), "Config");

        public string AppExportFolderPath { get; } =
            Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MentorSpeedDatingApp"),
                "Exports");

        #endregion

        public Config()
        {
            this.AppSaveFileFolder =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MentorSpeedDatingApp");
            this.AppSaveFileName = "myMentorSpeedDating.json";
        }

        public string CombineAppPaths()
        {
            return Path.Combine(this.AppSaveFileFolder, this.AppSaveFileName);
        }

        public void ResetToDefault()
        {
            this.AppSaveFileFolder =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MentorSpeedDatingApp");
            this.AppSaveFileName = "myMentorSpeedDating.json";
        }

        public override string ToString()
        {
            return this.AppSaveFileFolder + this.AppSaveFileName;
        }
    }
}