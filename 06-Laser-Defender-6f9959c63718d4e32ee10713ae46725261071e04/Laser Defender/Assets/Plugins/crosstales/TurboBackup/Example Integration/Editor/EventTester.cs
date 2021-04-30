#if false || CT_DEVELOP
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crosstales.TB.Example
{
   /// <summary>Simple test script for all callbacks.</summary>
   [InitializeOnLoad]
   public abstract class EventTester
   {
      #region Constructor

      static EventTester()
      {
         BAR.OnBackupStart += onBackupStart;
         BAR.OnBackupComplete += onBackupComplete;
         BAR.OnRestoreStart += onRestoreStart;
         BAR.OnRestoreComplete += onRestoreComplete;
      }

      private static void onBackupStart()
      {
         Debug.Log("Backup start");
      }

      private static void onBackupComplete(bool success)
      {
         Debug.Log("Backup complete: " + success);
      }

      private static void onRestoreStart()
      {
         Debug.Log("Restore start");
      }

      private static void onRestoreComplete(bool success)
      {
         Debug.Log("Restore complete: " + success);
      }

      #endregion
   }
}
#endif
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)