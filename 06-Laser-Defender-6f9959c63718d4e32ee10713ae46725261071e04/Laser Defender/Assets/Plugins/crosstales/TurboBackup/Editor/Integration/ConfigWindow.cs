﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crosstales.TB.EditorIntegration
{
   /// <summary>Editor window extension.</summary>
   //[InitializeOnLoad]
   public class ConfigWindow : ConfigBase
   {
      #region Variables

      private int tab;
      private int lastTab;

      #endregion


      #region EditorWindow methods

      [MenuItem("Window/" + Util.Constants.ASSET_NAME, false, 1020)]
      public static void ShowWindow()
      {
         GetWindow(typeof(ConfigWindow));
      }

      public static void ShowWindow(int tab)
      {
         ConfigWindow window = GetWindow(typeof(ConfigWindow)) as ConfigWindow;
         if (window != null) window.tab = tab;
      }

      public void OnEnable()
      {
         titleContent = new GUIContent(Util.Constants.ASSET_NAME_SHORT, Util.Helper.Logo_Asset_Small);
      }

      public void OnDestroy()
      {
         save();
      }

      public void OnLostFocus()
      {
         save();
      }

      public void OnGUI()
      {
         tab = GUILayout.Toolbar(tab, new[] {"B&R", "Config", "Help", "About"});

         if (tab != lastTab)
         {
            lastTab = tab;
            GUI.FocusControl(null);
         }

         switch (tab)
         {
            case 0:
               showBAR();
               break;
            case 1:
            {
               showConfiguration();

               Util.Helper.SeparatorUI();

               GUILayout.BeginHorizontal();
               {
                  if (GUILayout.Button(new GUIContent(" Save", Util.Helper.Icon_Save, "Saves the configuration settings for this project.")))
                  {
                     save();
                  }

                  if (GUILayout.Button(new GUIContent(" Reset", Util.Helper.Icon_Reset, "Resets the configuration settings for this project.")))
                  {
                     if (EditorUtility.DisplayDialog("Reset configuration?", "Reset the configuration of " + Util.Constants.ASSET_NAME + "?", "Yes", "No"))
                     {
                        Util.Config.Reset();
                        save();
                     }
                  }
               }
               GUILayout.EndHorizontal();

               GUILayout.Space(6);
               break;
            }
            case 2:
               showHelp();
               break;
            default:
               showAbout();
               break;
         }
      }

      public void OnInspectorUpdate()
      {
         Repaint();
      }

      #endregion
   }
}
#endif
// © 2018-2021 crosstales LLC (https://www.crosstales.com)