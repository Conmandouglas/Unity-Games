#if UNITY_EDITOR
namespace Crosstales.TB.Util
{
   /// <summary>Collected constants of very general utility for the asset.</summary>
   public abstract class Constants : Common.Util.BaseConstants
   {
      #region Constant variables

      /// <summary>Name of the asset.</summary>
      public const string ASSET_NAME = "Turbo Backup PRO";

      /// <summary>Short name of the asset.</summary>
      public const string ASSET_NAME_SHORT = "TB PRO";

      /// <summary>Version of the asset.</summary>
      public const string ASSET_VERSION = "2021.1.1";

      /// <summary>Build number of the asset.</summary>
      public const int ASSET_BUILD = 20210226;

      /// <summary>Create date of the asset (YYYY, MM, DD).</summary>
      public static readonly System.DateTime ASSET_CREATED = new System.DateTime(2018, 3, 4);

      /// <summary>Change date of the asset (YYYY, MM, DD).</summary>
      public static readonly System.DateTime ASSET_CHANGED = new System.DateTime(2021, 2, 26);

      /// <summary>URL of the PRO asset in UAS.</summary>
      public const string ASSET_PRO_URL = "https://assetstore.unity.com/packages/slug/98711?aid=1011lNGT";

      /// <summary>URL for update-checks of the asset</summary>
      public const string ASSET_UPDATE_CHECK_URL = "https://www.crosstales.com/media/assets/tb_versions.txt";
      //public const string ASSET_UPDATE_CHECK_URL = "https://www.crosstales.com/media/assets/test/tb_versions_test.txt";

      /// <summary>Contact to the owner of the asset.</summary>
      public const string ASSET_CONTACT = "tb@crosstales.com";

      /// <summary>URL of the asset manual.</summary>
      public const string ASSET_MANUAL_URL = "https://www.crosstales.com/media/data/assets/TurboBackup/TurboBackup-doc.pdf";

      /// <summary>URL of the asset API.</summary>
      public const string ASSET_API_URL = "https://www.crosstales.com/en/assets/TurboBackup/api/";

      /// <summary>URL of the asset forum.</summary>
      public const string ASSET_FORUM_URL = "https://forum.unity.com/threads/turbo-backup-fast-and-save-backup-solution.521731/";

      /// <summary>URL of the asset in crosstales.</summary>
      public const string ASSET_WEB_URL = "https://www.crosstales.com/en/portfolio/TurboBackup/";

/*
        /// <summary>URL of the promotion video of the asset (Youtube).</summary>
        public const string ASSET_VIDEO_PROMO = "https://youtu.be/rb1cqypznEg?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S"; //TODO update
*/
      /// <summary>URL of the tutorial video of the asset (Youtube).</summary>
      public const string ASSET_VIDEO_TUTORIAL = "https://youtu.be/8EJ2H5220R4?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

      // Keys for the configuration of the asset
      public const string KEY_VCS = "CT_CFG_VCS";

      private const string KEY_PREFIX = "TB_CFG_";
      public const string KEY_CUSTOM_PATH_CACHE = KEY_PREFIX + "CUSTOM_PATH_CACHE";

      public const string KEY_PATH_CACHE = KEY_PREFIX + "PATH_CACHE";

      //public const string KEY_VCS = KEY_PREFIX + "VCS";
      public const string KEY_USE_LEGACY = KEY_PREFIX + "USE_LEGACY";
      public const string KEY_BATCHMODE = KEY_PREFIX + "BATCHMODE";
      public const string KEY_QUIT = KEY_PREFIX + "QUIT";
      public const string KEY_NO_GRAPHICS = KEY_PREFIX + "NO_GRAPHICS";
      public const string KEY_EXECUTE_METHOD_PRE_BACKUP = KEY_PREFIX + "EXECUTE_METHOD_PRE_BACKUP";
      public const string KEY_EXECUTE_METHOD_BACKUP = KEY_PREFIX + "EXECUTE_METHOD_BACKUP";
      public const string KEY_EXECUTE_METHOD_PRE_RESTORE = KEY_PREFIX + "EXECUTE_METHOD_PRE_RESTORE";
      public const string KEY_EXECUTE_METHOD_RESTORE = KEY_PREFIX + "EXECUTE_METHOD_RESTORE";
      public const string KEY_DELETE_LOCKFILE = KEY_PREFIX + "DELETE_LOCKFILE";
      public const string KEY_COPY_ASSETS = KEY_PREFIX + "COPY_ASSETS";
      public const string KEY_COPY_LIBRARY = KEY_PREFIX + "COPY_LIBRARY";
      public const string KEY_COPY_SETTINGS = KEY_PREFIX + "COPY_SETTINGS";
      public const string KEY_COPY_PACKAGES = KEY_PREFIX + "COPY_PACKAGES";
      public const string KEY_CONFIRM_BACKUP = KEY_PREFIX + "CONFIRM_BACKUP";
      public const string KEY_CONFIRM_RESTORE = KEY_PREFIX + "CONFIRM_RESTORE";
      public const string KEY_CONFIRM_WARNING = KEY_PREFIX + "CONFIRM_WARNING";
      public const string KEY_DEBUG = KEY_PREFIX + "DEBUG";
      public const string KEY_UPDATE_CHECK = KEY_PREFIX + "UPDATE_CHECK";
      public const string KEY_COMPILE_DEFINES = KEY_PREFIX + "COMPILE_DEFINES";

      //public const string KEY_BACKUP_DATE = KEY_PREFIX + "BACKUP_DATE";
      public const string KEY_BACKUP_COUNT = KEY_PREFIX + "BACKUP_COUNT";
      public const string KEY_RESTORE_DATE = KEY_PREFIX + "RESTORE_DATE";
      public const string KEY_RESTORE_COUNT = KEY_PREFIX + "RESTORE_COUNT";

      public const string KEY_SETUP_DATE = KEY_PREFIX + "SETUP_DATE";

      public const string KEY_UPDATE_DATE = KEY_PREFIX + "UPDATE_DATE";

      public const string KEY_AUTO_SAVE = KEY_PREFIX + "AUTO_SAVE";

      public const string BACKUP_DIRNAME = "TB_backup";

      public const string KEY_AUTO_BACKUP_DATE = KEY_PREFIX + "AUTO_BACKUP_DATE";
      public const string KEY_AUTO_BACKUP_INTERVAL = KEY_PREFIX + "AUTO_BACKUP_INTERVAL";

      // Default values
      public const string DEFAULT_ASSET_PATH = "/Plugins/crosstales/TurboBackup/";
      public static readonly string DEFAULT_PATH_CACHE = Helper.ValidatePath(APPLICATION_PATH + BACKUP_DIRNAME);
      public const bool DEFAULT_CUSTOM_PATH_BACKUP = false;
      public const int DEFAULT_VCS = 1; //git
      public const bool DEFAULT_USE_LEGACY = false;
      public const bool DEFAULT_BATCHMODE = false;
      public const bool DEFAULT_QUIT = true;
      public const bool DEFAULT_NO_GRAPHICS = false;
#if UNITY_2018_1_OR_NEWER && !UNITY_2019_1_OR_NEWER && UNITY_EDITOR_OSX
        public const bool DEFAULT_DELETE_LOCKFILE = true;
#else
      public const bool DEFAULT_DELETE_LOCKFILE = false;
#endif
      public const bool DEFAULT_COPY_ASSETS = true;
      public const bool DEFAULT_COPY_LIBRARY = false;
      public const bool DEFAULT_COPY_SETTINGS = true;
#if UNITY_2018_1_OR_NEWER
      public const bool DEFAULT_COPY_PACKAGES = true;
#else
      public const bool DEFAULT_COPY_PACKAGES = false;
#endif
      public const bool DEFAULT_CONFIRM_BACKUP = true;
      public const bool DEFAULT_CONFIRM_RESTORE = true;
      public const bool DEFAULT_CONFIRM_WARNING = true;
      public const bool DEFAULT_UPDATE_CHECK = false;
      public const bool DEFAULT_COMPILE_DEFINES = true;

      public const bool DEFAULT_AUTO_SAVE = false;

      public const string TEXT_NO_BACKUP = "no backup";

      #endregion


      #region Properties

      /// <summary>Returns the URL of the asset in UAS.</summary>
      /// <returns>The URL of the asset in UAS.</returns>
      public static string ASSET_URL => ASSET_PRO_URL;

      /// <summary>Returns the ID of the asset in UAS.</summary>
      /// <returns>The ID of the asset in UAS.</returns>
      public static string ASSET_ID => "98711";

      /// <summary>Returns the UID of the asset.</summary>
      /// <returns>The UID of the asset.</returns>
      public static System.Guid ASSET_UID => new System.Guid("32aa0df4-78bf-4548-9476-8df979f8a49c");

      #endregion
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)