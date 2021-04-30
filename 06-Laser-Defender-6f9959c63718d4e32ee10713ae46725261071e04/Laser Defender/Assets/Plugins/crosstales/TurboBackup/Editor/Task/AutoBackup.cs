#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crosstales.TB.EditorTask
{
   /// <summary>Automatically backup in a set interval (in minutes).</summary>
   [InitializeOnLoad]
   public class AutoBackup
   {
      private static bool isWorking;

      public static int BackupInterval
      {
         get => Util.Config.AUTO_BACKUP_INTERVAL;

         set
         {
            if (value != Util.Config.AUTO_BACKUP_INTERVAL)
            {
               Util.Config.AUTO_BACKUP_INTERVAL = Mathf.Abs(value);
               Util.Config.AUTO_BACKUP_DATE = BackupInterval > 0 ? System.DateTime.Now.AddMinutes(BackupInterval) : System.DateTime.Now.AddYears(1976);
            }
         }
      }

      static AutoBackup()
      {
         EditorApplication.update += update;
         EditorApplication.quitting += onQuitting;

         BAR.OnBackupStart += onBackupStart;
         BAR.OnBackupComplete += onBackupComplete;
         BAR.OnRestoreStart += onRestoreStart;
         BAR.OnRestoreComplete += onRestoreComplete;

         if (BackupInterval > 0)
         {
            if (Util.Config.AUTO_BACKUP_DATE < System.DateTime.Now)
               Util.Config.AUTO_BACKUP_DATE = System.DateTime.Now.AddMinutes(BackupInterval);

            //Debug.Log($"Auto backup enabled: {BackupInterval} - {Util.Config.AUTO_BACKUP_DATE}");
         }

         /*
         else
         {
            Debug.Log("Auto backup disabled!");
         }
         */
      }


      private static void onQuitting()
      {
         //Common.Util.CTPlayerPrefs.DeleteKey(Util.Constants.KEY_AUTO_BACKUP_DATE);
         //Common.Util.CTPlayerPrefs.Save();

         Util.Config.Save();
      }

      private static void onBackupStart()
      {
         //Debug.Log("+++ Auto backup started: " + System.DateTime.Now);
         isWorking = true;
      }

      private static void onBackupComplete(bool success)
      {
         //Debug.Log("--- Auto backup ended: " + System.DateTime.Now);
         isWorking = false;

         Util.Config.AUTO_BACKUP_DATE = System.DateTime.Now.AddMinutes(BackupInterval);
         Util.Config.Save();
      }

      private static void onRestoreStart()
      {
         //Debug.Log("+++ Restore started: " + System.DateTime.Now);
         isWorking = true;
      }

      private static void onRestoreComplete(bool success)
      {
         //Debug.Log("--- Restore ended: " + System.DateTime.Now);
         isWorking = false;

         Util.Config.AUTO_BACKUP_DATE = System.DateTime.Now.AddMinutes(BackupInterval);
         Util.Config.Save();
      }

      private static void update()
      {
         if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying || BackupInterval < 1 || Util.Config.AUTO_BACKUP_DATE > System.DateTime.Now)
            return;

         if (!isWorking)
         {
            BAR.Backup();
         }
      }
   }
}
#endif