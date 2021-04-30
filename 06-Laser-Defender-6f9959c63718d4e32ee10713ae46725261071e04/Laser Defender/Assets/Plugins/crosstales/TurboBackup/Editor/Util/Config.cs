#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crosstales.TB.Util
{
   /// <summary>Configuration for the asset.</summary>
   [InitializeOnLoad]
   public static class Config
   {
      #region Variables

      /// <summary>Enable or disable custom location for the backup.</summary>
      public static bool CUSTOM_PATH_BACKUP = Constants.DEFAULT_CUSTOM_PATH_BACKUP;

      /// <summary>Backup path.</summary>
      private static string pathBackup = Constants.DEFAULT_PATH_CACHE;

      public static string PATH_BACKUP
      {
         get => CUSTOM_PATH_BACKUP && !string.IsNullOrEmpty(pathBackup) ? Helper.ValidatePath(pathBackup) : Constants.DEFAULT_PATH_CACHE;
         set
         {
            if (CUSTOM_PATH_BACKUP)
            {
               string path = value.Substring(0, value.Length - Constants.BACKUP_DIRNAME.Length - 1);

               if (path.CTContains("Library"))
               {
                  Debug.LogError("Backup path can't be inside a path containing 'Library': " + value);
               }
               else if (path.CTContains("Assets"))
               {
                  Debug.LogError("Backup path can't be inside a path containing 'Assets': " + value);
               }
               else if (path.CTContains("ProjectSettings"))
               {
                  Debug.LogError("Cache path can't be inside a path containing 'ProjectSettings': " + value);
               }
               else
               {
                  pathBackup = value;
               }
            }
            else
            {
               pathBackup = value;
            }
         }
      }

      /// <summary>Selected VCS-system (default: 0, 0 = none, 1 = git, 2 = SVN, 3 Mercurial, 4 = Collab, 5 = PlasticSCM).</summary>
      public static int VCS = Constants.DEFAULT_VCS;

      /// <summary>Uses the legacy switch function.</summary>
      public static bool USE_LEGACY = Constants.DEFAULT_USE_LEGACY;

      /// <summary>Enable or disable batch mode for CLI operations.</summary>
      public static bool BATCHMODE = Constants.DEFAULT_BATCHMODE;

      /// <summary>Enable or disable quit Unity Editor for CLI operations.</summary>
      public static bool QUIT = Constants.DEFAULT_QUIT;

      /// <summary>Enable or disable graphics device in Unity Editor for CLI operations.</summary>
      public static bool NO_GRAPHICS = Constants.DEFAULT_NO_GRAPHICS;

      /// <summary>Execute static method 'ClassName.MethodName' in Unity before a backup.</summary>
      public static string EXECUTE_METHOD_PRE_BACKUP = string.Empty;

      /// <summary>Execute static method 'ClassName.MethodName' in Unity after a backup.</summary>
      public static string EXECUTE_METHOD_BACKUP = string.Empty;

      /// <summary>Execute static method 'ClassName.MethodName' in Unity before a restore.</summary>
      public static string EXECUTE_METHOD_PRE_RESTORE = string.Empty;

      /// <summary>Execute static method 'ClassName.MethodName' in Unity after a restore.</summary>
      public static string EXECUTE_METHOD_RESTORE = string.Empty;

      /// <summary>Enable or disable deleting the 'UnityLockfile'.</summary>
      public static bool DELETE_LOCKFILE = Constants.DEFAULT_DELETE_LOCKFILE;

      /// <summary>Enable or disable copying the 'Assets'-folder.</summary>
      public static bool COPY_ASSETS = Constants.DEFAULT_COPY_ASSETS;

      /// <summary>Enable or disable copying the 'Library'-folder.</summary>
      public static bool COPY_LIBRARY = Constants.DEFAULT_COPY_LIBRARY;

      /// <summary>Enable or disable copying the 'ProjectSettings'-folder.</summary>
      public static bool COPY_SETTINGS = Constants.DEFAULT_COPY_SETTINGS;

      /// <summary>Enable or disable copying the 'Packages'-folder.</summary>
      public static bool COPY_PACKAGES = Constants.DEFAULT_COPY_PACKAGES;

      /// <summary>Enable or disable the backup confirmation dialog.</summary>
      public static bool CONFIRM_BACKUP = Constants.DEFAULT_CONFIRM_BACKUP;

      /// <summary>Enable or disable the restore confirmation dialog.</summary>
      public static bool CONFIRM_RESTORE = Constants.DEFAULT_CONFIRM_RESTORE;

      /// <summary>Enable or disable the restore warning confirmation dialog.</summary>
      public static bool CONFIRM_WARNING = Constants.DEFAULT_CONFIRM_WARNING;

      /// <summary>Enable or disable debug logging for the asset.</summary>
      public static bool DEBUG = Constants.DEFAULT_DEBUG;

      /// <summary>Enable or disable update-checks for the asset.</summary>
      public static bool UPDATE_CHECK = Constants.DEFAULT_UPDATE_CHECK;

      /// <summary>Enable or disable adding compile define "CT_TB" for the asset.</summary>
      public static bool COMPILE_DEFINES = Constants.DEFAULT_COMPILE_DEFINES;

      /// <summary>Backup date.</summary>
      public static System.DateTime BACKUP_DATE
      {
         get
         {
            if (backupDate.Year < 2020)
            {
               try
               {
                  if (System.IO.File.Exists(backupFile))
                  {
                     System.DateTime.TryParseExact(System.IO.File.ReadAllText(backupFile).Trim(), "yyyyMMddHHmmsss", null, System.Globalization.DateTimeStyles.None, out System.DateTime result);

                     backupDate = result;
                  }
               }
               catch (System.Exception ex)
               {
                  Debug.LogWarning($"Could not read backup date: {ex}");
               }
            }

            return backupDate;
         }

         set
         {
            try
            {
               System.IO.Directory.CreateDirectory(PATH_BACKUP);

               System.IO.File.WriteAllText(backupFile, value.ToString("yyyyMMddHHmmsss"));

               backupDate = value;
            }
            catch (System.Exception ex)
            {
               Debug.LogWarning($"Could not write backup date: {ex}");
            }
         }
      }

      /// <summary>Backup counter.</summary>
      public static int BACKUP_COUNT;

      /// <summary>Restore date.</summary>
      public static System.DateTime RESTORE_DATE;

      /// <summary>Restore counter.</summary>
      public static int RESTORE_COUNT;

      /// <summary>Last setup date.</summary>
      public static System.DateTime SETUP_DATE;

      /// <summary>Enable or disable automatic saving of all scenes.</summary>
      public static bool AUTO_SAVE = Constants.DEFAULT_AUTO_SAVE;

      /// <summary>Auto backup date.</summary>
      public static System.DateTime AUTO_BACKUP_DATE;

      /// <summary>Auto backup interval.</summary>
      public static int AUTO_BACKUP_INTERVAL;

      /// <summary>Is the configuration loaded?</summary>
      public static bool isLoaded;

      private static string assetPath;
      private const string idPath = "Documentation/id/";
      private static readonly string idName = Constants.ASSET_UID + ".txt";

      private static System.DateTime backupDate;

      #endregion


      #region Constructor

      static Config()
      {
         if (!isLoaded)
         {
            Load();

            if (DEBUG)
               Debug.Log("Config data loaded");
         }
      }

      #endregion


      #region Properties

      /// <summary>Returns the path to the asset inside the Unity project.</summary>
      /// <returns>The path to the asset inside the Unity project.</returns>
      public static string ASSET_PATH
      {
         get
         {
            if (assetPath == null)
            {
               try
               {
                  if (System.IO.File.Exists(Application.dataPath + Constants.DEFAULT_ASSET_PATH + idPath + idName))
                  {
                     assetPath = Constants.DEFAULT_ASSET_PATH;
                  }
                  else
                  {
                     string[] files = System.IO.Directory.GetFiles(Application.dataPath, idName, System.IO.SearchOption.AllDirectories);

                     if (files.Length > 0)
                     {
                        string name = files[0].Substring(Application.dataPath.Length);
                        assetPath = name.Substring(0, name.Length - idPath.Length - idName.Length).Replace("\\", "/");
                     }
                     else
                     {
                        Debug.LogWarning("Could not locate the asset! File not found: " + idName);
                        assetPath = Constants.DEFAULT_ASSET_PATH;
                     }
                  }
               }
               catch (System.Exception ex)
               {
                  Debug.LogWarning("Could not locate asset: " + ex);
               }
            }

            return assetPath;
         }
      }

      private static string backupFile => $"{PATH_BACKUP}/Backup.dat";

      #endregion


      #region Public static methods

      /// <summary>Resets all changeable variables to their default value.</summary>
      public static void Reset()
      {
         assetPath = null;

         CUSTOM_PATH_BACKUP = Constants.DEFAULT_CUSTOM_PATH_BACKUP;
         pathBackup = Constants.DEFAULT_PATH_CACHE;
         VCS = Constants.DEFAULT_VCS;
         USE_LEGACY = Constants.DEFAULT_USE_LEGACY;
         BATCHMODE = Constants.DEFAULT_BATCHMODE;
         QUIT = Constants.DEFAULT_QUIT;
         NO_GRAPHICS = Constants.DEFAULT_NO_GRAPHICS;
         EXECUTE_METHOD_PRE_BACKUP = string.Empty;
         EXECUTE_METHOD_BACKUP = string.Empty;
         EXECUTE_METHOD_PRE_RESTORE = string.Empty;
         EXECUTE_METHOD_RESTORE = string.Empty;
         DELETE_LOCKFILE = Constants.DEFAULT_DELETE_LOCKFILE;
         COPY_ASSETS = Constants.DEFAULT_COPY_ASSETS;
         COPY_LIBRARY = Constants.DEFAULT_COPY_LIBRARY;
         COPY_SETTINGS = Constants.DEFAULT_COPY_SETTINGS;
         COPY_PACKAGES = Constants.DEFAULT_COPY_PACKAGES;
         CONFIRM_BACKUP = Constants.DEFAULT_CONFIRM_BACKUP;
         CONFIRM_RESTORE = Constants.DEFAULT_CONFIRM_RESTORE;
         CONFIRM_WARNING = Constants.DEFAULT_CONFIRM_WARNING;

         if (!Constants.DEV_DEBUG)
            DEBUG = Constants.DEFAULT_DEBUG;

         UPDATE_CHECK = Constants.DEFAULT_UPDATE_CHECK;
         COMPILE_DEFINES = Constants.DEFAULT_COMPILE_DEFINES;
         AUTO_SAVE = Constants.DEFAULT_AUTO_SAVE;
         AUTO_BACKUP_INTERVAL = 0;
      }

      /// <summary>Loads the all changeable variables.</summary>
      public static void Load()
      {
         assetPath = null;

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_CUSTOM_PATH_CACHE))
            CUSTOM_PATH_BACKUP = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_CUSTOM_PATH_CACHE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_PATH_CACHE))
            PATH_BACKUP = Common.Util.CTPlayerPrefs.GetString(Constants.KEY_PATH_CACHE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_VCS))
            VCS = Common.Util.CTPlayerPrefs.GetInt(Constants.KEY_VCS);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_USE_LEGACY))
            USE_LEGACY = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_USE_LEGACY);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_BATCHMODE))
            BATCHMODE = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_BATCHMODE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_QUIT))
            QUIT = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_QUIT);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_NO_GRAPHICS))
            NO_GRAPHICS = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_NO_GRAPHICS);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_BACKUP))
            EXECUTE_METHOD_PRE_BACKUP = Common.Util.CTPlayerPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_BACKUP);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_BACKUP))
            EXECUTE_METHOD_BACKUP = Common.Util.CTPlayerPrefs.GetString(Constants.KEY_EXECUTE_METHOD_BACKUP);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_PRE_RESTORE))
            EXECUTE_METHOD_PRE_RESTORE = Common.Util.CTPlayerPrefs.GetString(Constants.KEY_EXECUTE_METHOD_PRE_RESTORE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_EXECUTE_METHOD_RESTORE))
            EXECUTE_METHOD_RESTORE = Common.Util.CTPlayerPrefs.GetString(Constants.KEY_EXECUTE_METHOD_RESTORE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_DELETE_LOCKFILE))
            DELETE_LOCKFILE = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_DELETE_LOCKFILE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_COPY_ASSETS))
            COPY_ASSETS = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_COPY_ASSETS);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_COPY_LIBRARY))
            COPY_LIBRARY = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_COPY_LIBRARY);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_COPY_SETTINGS))
            COPY_SETTINGS = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_COPY_SETTINGS);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_COPY_PACKAGES))
            COPY_PACKAGES = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_COPY_PACKAGES);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_CONFIRM_BACKUP))
            CONFIRM_BACKUP = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_CONFIRM_BACKUP);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_CONFIRM_RESTORE))
            CONFIRM_RESTORE = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_CONFIRM_RESTORE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_CONFIRM_WARNING))
            CONFIRM_WARNING = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_CONFIRM_WARNING);

         if (!Constants.DEV_DEBUG)
         {
            if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_DEBUG))
               DEBUG = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_DEBUG);
         }
         else
         {
            DEBUG = Constants.DEV_DEBUG;
         }

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_UPDATE_CHECK))
            UPDATE_CHECK = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_UPDATE_CHECK);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_COMPILE_DEFINES))
            COMPILE_DEFINES = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_COMPILE_DEFINES);

         //if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_BACKUP_DATE))
         //   BACKUP_DATE = Common.Util.CTPlayerPrefs.GetDate(Constants.KEY_BACKUP_DATE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_BACKUP_COUNT))
            BACKUP_COUNT = Common.Util.CTPlayerPrefs.GetInt(Constants.KEY_BACKUP_COUNT);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_RESTORE_DATE))
            RESTORE_DATE = Common.Util.CTPlayerPrefs.GetDate(Constants.KEY_RESTORE_DATE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_RESTORE_COUNT))
            RESTORE_COUNT = Common.Util.CTPlayerPrefs.GetInt(Constants.KEY_RESTORE_COUNT);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_SETUP_DATE))
            SETUP_DATE = Common.Util.CTPlayerPrefs.GetDate(Constants.KEY_SETUP_DATE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_AUTO_SAVE))
            AUTO_SAVE = Common.Util.CTPlayerPrefs.GetBool(Constants.KEY_AUTO_SAVE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_AUTO_BACKUP_DATE))
            AUTO_BACKUP_DATE = Common.Util.CTPlayerPrefs.GetDate(Constants.KEY_AUTO_BACKUP_DATE);

         if (Common.Util.CTPlayerPrefs.HasKey(Constants.KEY_AUTO_BACKUP_INTERVAL))
            AUTO_BACKUP_INTERVAL = Common.Util.CTPlayerPrefs.GetInt(Constants.KEY_AUTO_BACKUP_INTERVAL);

         isLoaded = true;
      }

      /// <summary>Saves the all changeable variables.</summary>
      public static void Save()
      {
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_CUSTOM_PATH_CACHE, CUSTOM_PATH_BACKUP);
         Common.Util.CTPlayerPrefs.SetString(Constants.KEY_PATH_CACHE, PATH_BACKUP);
         Common.Util.CTPlayerPrefs.SetInt(Constants.KEY_VCS, VCS);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_USE_LEGACY, USE_LEGACY);

         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_BATCHMODE, BATCHMODE);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_QUIT, QUIT);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_NO_GRAPHICS, NO_GRAPHICS);

         Common.Util.CTPlayerPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_BACKUP, EXECUTE_METHOD_PRE_BACKUP);
         Common.Util.CTPlayerPrefs.SetString(Constants.KEY_EXECUTE_METHOD_BACKUP, EXECUTE_METHOD_BACKUP);
         Common.Util.CTPlayerPrefs.SetString(Constants.KEY_EXECUTE_METHOD_PRE_RESTORE, EXECUTE_METHOD_PRE_RESTORE);
         Common.Util.CTPlayerPrefs.SetString(Constants.KEY_EXECUTE_METHOD_RESTORE, EXECUTE_METHOD_RESTORE);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_DELETE_LOCKFILE, DELETE_LOCKFILE);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_COPY_ASSETS, COPY_ASSETS);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_COPY_LIBRARY, COPY_LIBRARY);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_COPY_SETTINGS, COPY_SETTINGS);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_COPY_PACKAGES, COPY_PACKAGES);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_CONFIRM_BACKUP, CONFIRM_BACKUP);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_CONFIRM_RESTORE, CONFIRM_RESTORE);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_CONFIRM_WARNING, CONFIRM_WARNING);

         if (!Constants.DEV_DEBUG)
            Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_DEBUG, DEBUG);

         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_UPDATE_CHECK, UPDATE_CHECK);
         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_COMPILE_DEFINES, COMPILE_DEFINES);

         //Common.Util.CTPlayerPrefs.SetDate(Constants.KEY_BACKUP_DATE, BACKUP_DATE);
         Common.Util.CTPlayerPrefs.SetInt(Constants.KEY_BACKUP_COUNT, BACKUP_COUNT);
         Common.Util.CTPlayerPrefs.SetDate(Constants.KEY_RESTORE_DATE, RESTORE_DATE);
         Common.Util.CTPlayerPrefs.SetInt(Constants.KEY_RESTORE_COUNT, RESTORE_COUNT);

         Common.Util.CTPlayerPrefs.SetDate(Constants.KEY_SETUP_DATE, SETUP_DATE);

         Common.Util.CTPlayerPrefs.SetBool(Constants.KEY_AUTO_SAVE, AUTO_SAVE);

         Common.Util.CTPlayerPrefs.SetDate(Constants.KEY_AUTO_BACKUP_DATE, AUTO_BACKUP_DATE);
         Common.Util.CTPlayerPrefs.SetInt(Constants.KEY_AUTO_BACKUP_INTERVAL, AUTO_BACKUP_INTERVAL);

         Common.Util.CTPlayerPrefs.Save();
      }

      #endregion
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)