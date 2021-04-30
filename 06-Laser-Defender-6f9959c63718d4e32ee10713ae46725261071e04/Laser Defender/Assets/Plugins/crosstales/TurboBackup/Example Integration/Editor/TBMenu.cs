#if false || CT_DEVELOP
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crosstales.TB.Example
{
   /// <summary>Example editor integration of Turbo Backup for your own scripts.</summary>
   public static class TBMenu
   {
      [MenuItem("Tools/Backup #&b", false, 20)]
      public static void Backup()
      {
         Debug.Log("Backup project");

         if (!BAR.Backup())
            Debug.LogError($"Could not backup project!");
      }

      [MenuItem("Tools/Restore #&r", false, 40)]
      public static void Restore()
      {
         if (EditorUtility.DisplayDialog("Restore the project?", "Restore the project from the latest backup?", "Yes", "No"))
         {
            Debug.Log("Restore project");

            if (!BAR.Restore())
               Debug.LogError($"Could not restore project!");
         }
      }
   }
}
#endif
#endif
// © 2019-2021 crosstales LLC (https://www.crosstales.com)