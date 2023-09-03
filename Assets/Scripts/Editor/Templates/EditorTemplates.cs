using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AzurShmup.Core;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

/*
 
    TODO:
    Make a control pannel for the Azur Shmup Framework,
    to streamline game developement and level creation.
 
 */

namespace AzurShmup.Editor
{
    public class EditorTemplates : EditorWindow
    {
        private bool _showTemplates = false;
        private bool _showEnemyTemplates = false;
        private bool _showMovementTemplates = false;
        private bool _showShotTemplates = false;

        private Vector2 _scrollPosition = Vector2.zero;

        public TemplatesScriptableObject templates;

        [MenuItem("Window/Azur Smup Controls")]
        private static void Init()
        {
            GetWindow(typeof(EditorTemplates));
        }

        public void Awake()
        {
            templates = ScriptableObject.CreateInstance<TemplatesScriptableObject>();
        }

        private void OnGUI()
        {
            GUILayout.Label("Azur Shmup Controls", EditorStyles.boldLabel);
            GUILayout.Label("To add new templates, edit the TemplatesScriptableObject script and add the corresponding button", EditorStyles.boldLabel);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            // Reload the Templates if they are not working (temporary solution)
            if (GUILayout.Button("Refresh Templates", GUILayout.Width(240f)))
            {
                templates = ScriptableObject.CreateInstance<TemplatesScriptableObject>();
            }

            // Spell Attack Button
            if (GUILayout.Button("Create Spell"))
            {
                if (templates.spellAttack != null)
                {
                    EditorBase.InstantiatePrefab(templates.spellAttack, Vector3.zero);
                }
            }

            // Templates
            _showTemplates = EditorGUILayout.Foldout(_showTemplates, "Templates");
            if (_showTemplates)
            {
                // Enemy Templates
                _showEnemyTemplates = EditorGUILayout.Foldout(_showEnemyTemplates, "Enemy Templates");
                if (_showEnemyTemplates)
                {
                    // Small Red Ship Button
                    if (GUILayout.Button("Create Small Red Ship"))
                    {
                        if (templates.smallRedShipGameObject != null)
                        {
                            EditorBase.InstantiatePrefab(templates.smallRedShipGameObject, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Medium Purple Ship Button
                    if (GUILayout.Button("Create Medium Purple Ship"))
                    {
                        if (templates.mediumPurpleShipGameObject != null)
                        {
                            EditorBase.InstantiatePrefab(templates.mediumPurpleShipGameObject, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Turret Button
                    if (GUILayout.Button("Create Red Turret"))
                    {
                        if (templates.turretGameObject != null)
                        {
                            EditorBase.InstantiatePrefab(templates.turretGameObject, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Magic Stone Button
                    if (GUILayout.Button("Create Magic Stone"))
                    {
                        if (templates.magicStoneGameObject != null)
                        {
                            EditorBase.InstantiatePrefab(templates.magicStoneGameObject, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Blue Fairy Button
                    if (GUILayout.Button("Create Blue Fairy"))
                    {
                        if (templates.blueFairyGameObject != null)
                        {
                            EditorBase.InstantiatePrefab(templates.blueFairyGameObject, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Red Fairy Button
                    if (GUILayout.Button("Create Red Fairy"))
                    {
                        if (templates.redFairyGameObject != null)
                        {
                            EditorBase.InstantiatePrefab(templates.redFairyGameObject, GameManager.GAME_FIELD_CENTER);
                        }
                    }
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                // Movement Templates
                _showMovementTemplates = EditorGUILayout.Foldout(_showMovementTemplates, "Movement Templates");
                if (_showMovementTemplates)
                {
                    // Linear Movement Button
                    if (GUILayout.Button("Create Linear Movement"))
                    {
                        if (templates.linearMovement != null)
                        {
                            EditorBase.InstantiatePrefab(templates.linearMovement, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Bezier Movement Ship Button
                    if (GUILayout.Button("Create Bezier Movement"))
                    {
                        if (templates.bezierMovement != null)
                        {
                            EditorBase.InstantiatePrefab(templates.bezierMovement, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Random Movement Button
                    if (GUILayout.Button("Create Random Movement"))
                    {
                        if (templates.randomMovement != null)
                        {
                            EditorBase.InstantiatePrefab(templates.randomMovement, GameManager.GAME_FIELD_CENTER);
                        }
                    }
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                // Shot Templates
                _showShotTemplates = EditorGUILayout.Foldout(_showShotTemplates, "Shot Templates");
                if (_showShotTemplates)
                {
                    // Linear Shot Button
                    if (GUILayout.Button("Create Linear Shot"))
                    {
                        if (templates.linearShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.linearShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Random Timed Shot Button
                    if (GUILayout.Button("Create Random Timed Shot"))
                    {
                        if (templates.randomTimedShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.randomTimedShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Ring Shot Button
                    if (GUILayout.Button("Create Ring Shot"))
                    {
                        if (templates.ringShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.ringShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Cone Shot Button
                    if (GUILayout.Button("Create Cone Shot"))
                    {
                        if (templates.coneShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.coneShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Random Position Ring Shot Button
                    if (GUILayout.Button("Create Random Position Ring Shot"))
                    {
                        if (templates.randomPositionRingShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.randomPositionRingShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Random Shot Button
                    if (GUILayout.Button("Create Random Shot"))
                    {
                        if (templates.randomShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.randomShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    // Static Rotation Shot Button
                    if (GUILayout.Button("Create Static Rotation Shot"))
                    {
                        if (templates.staticRotationShot != null)
                        {
                            EditorBase.InstantiatePrefab(templates.staticRotationShot, GameManager.GAME_FIELD_CENTER);
                        }
                    }

                    EditorGUILayout.EndFoldoutHeaderGroup();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            GUILayout.EndScrollView();
        }
    }
}