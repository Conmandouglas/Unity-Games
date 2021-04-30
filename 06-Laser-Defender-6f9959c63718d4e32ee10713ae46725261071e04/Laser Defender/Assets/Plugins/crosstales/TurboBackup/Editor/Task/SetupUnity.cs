#if UNITY_EDITOR
using UnityEditor;

namespace Crosstales.TB.Task
{
   /// <summary>Setup Unity after a restore.</summary>
   [InitializeOnLoad]
   public static class SetupUnity
   {
      #region Constructor

      static SetupUnity()
      {
         if (Util.Config.USE_LEGACY && Util.Config.SETUP_DATE < Util.Config.RESTORE_DATE)
         {
            Util.Helper.RefreshAssetDatabase();

            Util.Config.SETUP_DATE = System.DateTime.Now;
            Util.Config.Save();
         }
      }

      #endregion
   }
}
#endif
// © 2019-2021 crosstales LLC (https://www.crosstales.com)