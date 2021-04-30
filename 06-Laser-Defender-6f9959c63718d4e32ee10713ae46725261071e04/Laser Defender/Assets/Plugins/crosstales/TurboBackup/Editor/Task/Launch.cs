#if UNITY_EDITOR
using UnityEditor;
using Enumerable = System.Linq.Enumerable;

namespace Crosstales.TB.Task
{
   /// <summary>Show the configuration window on the first launch.</summary>
   public class Launch : AssetPostprocessor
   {
      public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
      {
         if (Enumerable.Any(importedAssets, str => str.Contains(Util.Constants.ASSET_UID.ToString())))
         {
            Common.EditorTask.SetupResources.Setup();
            SetupResources.Setup();

            EditorIntegration.ConfigWindow.ShowWindow(3);
         }
      }
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)