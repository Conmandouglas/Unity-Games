#if UNITY_EDITOR
using UnityEngine;

namespace Crosstales.TB
{
   /// <summary>Backup and restore methods.</summary>
   public static class BAR
   {
      #region Properties

      /// <summary>True if the BAR is busy.</summary>
      public static bool isBusy { get; private set; }

      #endregion

      #region Events

      public delegate void BackupStart();

      public delegate void BackupComplete(bool success);

      public delegate void RestoreStart();

      public delegate void RestoreComplete(bool success);

      /// <summary>An event triggered whenever the backup is started.</summary>
      public static event BackupStart OnBackupStart;

      /// <summary>An event triggered whenever the backup is completed.</summary>
      public static event BackupComplete OnBackupComplete;

      /// <summary>An event triggered whenever the restore is started.</summary>
      public static event RestoreStart OnRestoreStart;

      /// <summary>An event triggered whenever the restore is completed.</summary>
      public static event RestoreComplete OnRestoreComplete;

      #endregion


      #region Public methods

      /// <summary>Backup the current project via CLI.</summary>
      public static void BackupCLI()
      {
         //TODO add path
         Backup(Util.Helper.getCLIArgument("-tbExecuteMethod"), "true".CTEquals(Util.Helper.getCLIArgument("-tbBatchmode")), !"false".CTEquals(Util.Helper.getCLIArgument("-tbQuit")), "true".CTEquals(Util.Helper.getCLIArgument("-tbNoGraphics")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopyAssets")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopyLibrary")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopySettings")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopyPackages")));
      }

      /// <summary>Restore the current project via CLI.</summary>
      public static void RestoreCLI()
      {
         //TODO add path
         Restore(Util.Helper.getCLIArgument("-tbExecuteMethod"), "true".CTEquals(Util.Helper.getCLIArgument("-tbBatchmode")), !"false".CTEquals(Util.Helper.getCLIArgument("-tbQuit")), "true".CTEquals(Util.Helper.getCLIArgument("-tbNoGraphics")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopyAssets")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopyLibrary")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopySettings")), "true".CTEquals(Util.Helper.getCLIArgument("-tbCopyPackages")));
      }

      /// <summary>Backup the current project.</summary>
      /// <param name="executeMethod">Execute method after backup</param>
      /// <param name="batchmode">Start Unity in batch-mode (default: false, optional)</param>
      /// <param name="quit">Quit Unity in batch-mode (default: true, optional)</param>
      /// <param name="noGraphics">Disable graphic devices in batch-mode (default: false, optional)</param>
      /// <param name="copyAssets">Copy the 'Assets'-folder (default: true, optional)</param>
      /// <param name="copyLibrary">Copy the 'Library'-folder (default: false, optional)</param>
      /// <param name="copySettings">Copy the 'ProjectSettings"-folder (default: true, optional)</param>
      /// <param name="copyPackages">Copy the 'Packages"-folder (default: true, optional)</param>
      /// <returns>True if the backup was successful.</returns>
      public static bool Backup(string executeMethod, bool batchmode = false, bool quit = true, bool noGraphics = false, bool copyAssets = true, bool copyLibrary = false, bool copySettings = true, bool copyPackages = true)
      {
         Util.Config.EXECUTE_METHOD_BACKUP = executeMethod;
         Util.Config.BATCHMODE = batchmode;
         Util.Config.QUIT = quit;
         Util.Config.NO_GRAPHICS = noGraphics;
         Util.Config.COPY_ASSETS = copyAssets;
         Util.Config.COPY_LIBRARY = copyLibrary;
         Util.Config.COPY_SETTINGS = copySettings;
         Util.Config.COPY_PACKAGES = copyPackages;

         return Backup();
      }

      /// <summary>Backup the current project.</summary>
      /// <returns>True if the backup was successful.</returns>
      public static bool Backup()
      {
         isBusy = true;

         OnBackupStart?.Invoke();

         bool success = false;

         if (Util.Config.COPY_ASSETS || Util.Config.COPY_LIBRARY || Util.Config.COPY_SETTINGS || Util.Config.COPY_PACKAGES)
         {
            success = Util.Config.USE_LEGACY ? Util.Helper.Backup() : Util.Helper.BackupNew();
         }
         else
         {
            Debug.LogError("No folders selected - backup not possible!");
#if UNITY_2018_2_OR_NEWER
            if (Application.isBatchMode)
#else
            if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
#endif
               throw new System.Exception("No folders selected - backup not possible!");
            //EditorApplication.Exit(0);
         }

         OnBackupComplete?.Invoke(success);

         isBusy = false;

         return success;
      }

      /// <summary>Restore the current project.</summary>
      /// <param name="executeMethod">Execute method after restore</param>
      /// <param name="batchmode">Start Unity in batch-mode (default: false, optional)</param>
      /// <param name="quit">Quit Unity in batch-mode (default: true, optional)</param>
      /// <param name="noGraphics">Disable graphic devices in batch-mode (default: false, optional)</param>
      /// <param name="restoreAssets">Restore the 'Assets'-folder (default: true, optional)</param>
      /// <param name="restoreLibrary">Restore the 'Library'-folder (default: false, optional)</param>
      /// <param name="restoreSettings">Restore the 'ProjectSettings"-folder (default: true, optional)</param>
      /// <param name="restorePackages">Restore the 'Packages"-folder (default: true, optional)</param>
      /// <returns>True if the restore was successful.</returns>
      public static bool Restore(string executeMethod, bool batchmode = false, bool quit = true, bool noGraphics = false, bool restoreAssets = true, bool restoreLibrary = false, bool restoreSettings = true, bool restorePackages = true)
      {
         Util.Config.EXECUTE_METHOD_RESTORE = executeMethod;
         Util.Config.BATCHMODE = batchmode;
         Util.Config.QUIT = quit;
         Util.Config.NO_GRAPHICS = noGraphics;
         Util.Config.COPY_ASSETS = restoreAssets;
         Util.Config.COPY_LIBRARY = restoreLibrary;
         Util.Config.COPY_SETTINGS = restoreSettings;
         Util.Config.COPY_PACKAGES = restorePackages;

         return Restore();
      }

      /// <summary>Restore the current project.</summary>
      /// <returns>True if the restore was successful.</returns>
      public static bool Restore()
      {
         isBusy = true;

         OnRestoreStart?.Invoke();

         bool success = false;

         if (Util.Config.COPY_ASSETS || Util.Config.COPY_LIBRARY || Util.Config.COPY_SETTINGS || Util.Config.COPY_PACKAGES)
         {
            success = Util.Config.USE_LEGACY ? Util.Helper.Restore() : Util.Helper.RestoreNew();
         }
         else
         {
            Debug.LogError("No folders selected - restore not possible!");
#if UNITY_2018_2_OR_NEWER
            if (Application.isBatchMode)
#else
            if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
#endif
               throw new System.Exception("No folders selected - restore not possible!");
            //EditorApplication.Exit(0);
         }

         OnRestoreComplete?.Invoke(success);

         isBusy = true;

         return success;
      }

      /// <summary>Test the backup/restore with an execute method.</summary>
      public static void SayHello()
      {
         Debug.LogError("Hello everybody, I was called by " + Util.Constants.ASSET_NAME);
      }

      /// <summary>Test method (before backup).</summary>
      public static void MethodBeforeBackup()
      {
         Debug.LogWarning("'MethodBeforeBackup' was called!");
      }

      /// <summary>Test method (after backup).</summary>
      public static void MethodAfterBackup()
      {
         Debug.LogWarning("'MethodAfterBackup' was called");
      }

      /// <summary>Test method (before restore).</summary>
      public static void MethodBeforeRestore()
      {
         Debug.LogWarning("'MethodBeforeRestore' was called!");
      }

      /// <summary>Test method (after restore).</summary>
      public static void MethodAfterRestore()
      {
         Debug.LogWarning("'MethodAfterRestore' was called");
      }

      /// <summary>Default method after backup.</summary>
      public static void DefaultMethodAfterBackup()
      {
         //Debug.LogWarning("'DefaultMethodAfterBackup' was called");
         OnBackupComplete?.Invoke(true);
      }

      /// <summary>Default method after restore.</summary>
      public static void DefaultMethodAfterRestore()
      {
         //Debug.LogWarning("'DefaultMethodAfterRestore' was called");
         OnRestoreComplete?.Invoke(true);
      }

      #endregion
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)