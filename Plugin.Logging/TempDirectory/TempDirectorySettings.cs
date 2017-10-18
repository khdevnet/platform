using System;
using System.IO;
using Plugin.Core.Extensibility;
using Plugin.Logging.Extensibility;
using FileInfo = System.IO.FileInfo;

namespace Plugin.Core
{
    public class TempDirectorySettings : ITempDirectorySettings
    {
        private const string TmpRootDirectorySettingKey = "TmpRootDirectory";

        private readonly ILogger logger;
        private readonly IAppSettingsProvider applicationSettings;
        private readonly Lazy<string> tmpRootDirectory;

        public TempDirectorySettings(IAppSettingsProvider applicationSettings, ILogger logger)
        {
            this.applicationSettings = applicationSettings;
            this.logger = logger;
            tmpRootDirectory = new Lazy<string>(CalculateTmpRootPath);
        }

        public string TempRootDirectory => tmpRootDirectory.Value;

        private static bool IsDirectoryValid(string tmpRootPath, out Exception exception)
        {
            exception = null;
            try
            {
                var tmpRootDirectoryInfo = new DirectoryInfo(tmpRootPath);
                if (!tmpRootDirectoryInfo.Exists)
                {
                    tmpRootDirectoryInfo.Create();
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
            return true;
        }

        private static bool CanWriteToDirectory(string directory, out Exception exception)
        {
            exception = null;
            try
            {
                string testFileName = Path.Combine(directory, new Guid() + ".testfile");
                var testFileInfo = new FileInfo(testFileName);
                using (FileStream fileStream = testFileInfo.Create())
                {
                    fileStream.WriteByte(0);
                }
                testFileInfo.Delete();
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }

            return true;
        }

        private string CalculateTmpRootPath()
        {
            string usedTmpRootPath = Path.GetTempPath();
            if (applicationSettings.ContainsKey(TmpRootDirectorySettingKey))
            {
                string tmpRootPath = applicationSettings.GetValue(TmpRootDirectorySettingKey);

                Exception exception;
                if (IsDirectoryValid(tmpRootPath, out exception)
                    && CanWriteToDirectory(tmpRootPath, out exception))
                {
                    usedTmpRootPath = tmpRootPath;
                }
                else
                {
                    logger.Warn($"Tmp Directory {tmpRootPath} does not exist and can not be created. Using fallback directory {usedTmpRootPath}", exception);
                }
            }

            return usedTmpRootPath;
        }
    }
}