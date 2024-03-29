﻿using System;
using UnityEditor;
using UnityEngine;

namespace SuperUnityBuild.BuildTool
{
    [CustomPropertyDrawer(typeof(ProductParameters))]
    public class ProductParametersDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            EditorGUILayout.BeginHorizontal();

            bool show = property.isExpanded;
            UnityBuildGUIUtility.DropdownHeader("Product Parameters", ref show, false, GUILayout.ExpandWidth(true));
            property.isExpanded = show;

            UnityBuildGUIUtility.HelpButton("Parameter-Details#product-parameters");
            EditorGUILayout.EndHorizontal();

            if (show)
            {
                EditorGUILayout.BeginVertical(UnityBuildGUIUtility.dropdownContentStyle);

                SerializedProperty autoGenerate = property.FindPropertyRelative("autoGenerate");
                SerializedProperty syncWithPlayerSettings = property.FindPropertyRelative("syncWithPlayerSettings");

                EditorGUI.BeginDisabledGroup(syncWithPlayerSettings.boolValue);
                EditorGUILayout.PropertyField(property.FindPropertyRelative("version"));

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(property.FindPropertyRelative("lastGeneratedVersion"));
                EditorGUI.EndDisabledGroup();

                autoGenerate.boolValue = EditorGUILayout.ToggleLeft("Auto-Generate Version", autoGenerate.boolValue);
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginDisabledGroup(autoGenerate.boolValue);
                syncWithPlayerSettings.boolValue = EditorGUILayout.ToggleLeft("Sync Version with Player Settings", syncWithPlayerSettings.boolValue);
                EditorGUI.EndDisabledGroup();

                if (syncWithPlayerSettings.boolValue)
                {
                    property.FindPropertyRelative("version").stringValue = PlayerSettings.bundleVersion;
                }

                EditorGUILayout.PropertyField(property.FindPropertyRelative("buildCounter"));

                if (GUILayout.Button("Reset Build Counter", GUILayout.ExpandWidth(true)))
                {
                    property.FindPropertyRelative("buildCounter").intValue = 0;
                }

                if (!autoGenerate.boolValue && !syncWithPlayerSettings.boolValue && GUILayout.Button("Generate Version String Now", GUILayout.ExpandWidth(true)))
                {
                    BuildProject.GenerateVersionString(BuildSettings.productParameters, DateTime.Now);
                }

                property.serializedObject.ApplyModifiedProperties();

                EditorGUILayout.EndVertical();
            }

            EditorGUI.EndProperty();
        }
    }
}