using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AzurProject.Core;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TemplateEditor : EditorWindow
{
    private bool _showEnemyTemplates = true;
    private bool _showMovementTemplates = true;
    private bool _showShotTemplates = true;

    private Vector2 _scrollPosition = Vector2.zero;
    
    public TemplatesScriptableObject templates;
    
    [MenuItem("Window/Templates")]
    private static void Init()
    {
        GetWindow(typeof(TemplateEditor));
    }

    public void Awake()
    {
        templates = ScriptableObject.CreateInstance<TemplatesScriptableObject>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Templates", EditorStyles.boldLabel);
        GUILayout.Label("To add new templates, edit the TemplatesScriptableObject script and add the corresponding button", EditorStyles.boldLabel);
        
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        
        // Reload the Templates if they are not working (temporary solution)
        if (GUILayout.Button("Refresh Templates", GUILayout.Width(240f)))
        {
            templates = ScriptableObject.CreateInstance<TemplatesScriptableObject>();
        }
        
        // Spell Attack Button
        if (GUILayout.Button("Create Spell Attack"))
        {
            if (templates.spellAttack != null)
            {
                InstantiatePrefab(templates.spellAttack, Vector3.zero);
            }
        }
        
        // Enemy Templates
        _showEnemyTemplates = EditorGUILayout.Foldout(_showEnemyTemplates, "Enemy Templates");
        if (_showEnemyTemplates)
        {
            // Small Red Ship Button
            if (GUILayout.Button("Create Small Red Ship"))
            {
                if (templates.smallRedShipGameObject != null)
                {
                    InstantiatePrefab(templates.smallRedShipGameObject, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Medium Purple Ship Button
            if (GUILayout.Button("Create Medium Purple Ship"))
            {
                if (templates.mediumPurpleShipGameObject != null)
                {
                    InstantiatePrefab(templates.mediumPurpleShipGameObject, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Turret Button
            if (GUILayout.Button("Create Red Turret"))
            {
                if (templates.turretGameObject != null)
                {
                    InstantiatePrefab(templates.turretGameObject, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Magic Stone Button
            if (GUILayout.Button("Create Magic Stone"))
            {
                if (templates.magicStoneGameObject != null)
                {
                    InstantiatePrefab(templates.magicStoneGameObject, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Blue Fairy Button
            if (GUILayout.Button("Create Blue Fairy"))
            {
                if (templates.blueFairyGameObject != null)
                {
                    InstantiatePrefab(templates.blueFairyGameObject, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Red Fairy Button
            if (GUILayout.Button("Create Red Fairy"))
            {
                if (templates.redFairyGameObject != null)
                {
                    InstantiatePrefab(templates.redFairyGameObject, GameManager.GAME_FIELD_CENTER);
                }
            }
        }
        
        // Movement Templates
        _showMovementTemplates = EditorGUILayout.Foldout(_showMovementTemplates, "Movement Templates");
        if (_showMovementTemplates)
        {
            // Linear Movement Button
            if (GUILayout.Button("Create Linear Movement"))
            {
                if (templates.linearMovement != null)
                {
                    InstantiatePrefab(templates.linearMovement, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Bezier Movement Ship Button
            if (GUILayout.Button("Create Bezier Movement"))
            {
                if (templates.bezierMovement != null)
                {
                    InstantiatePrefab(templates.bezierMovement, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Random Movement Button
            if (GUILayout.Button("Create Random Movement"))
            {
                if (templates.randomMovement != null)
                {
                    InstantiatePrefab(templates.randomMovement, GameManager.GAME_FIELD_CENTER);
                }
            }
        }
        
        // Shot Templates
        _showShotTemplates = EditorGUILayout.Foldout(_showShotTemplates, "Shot Templates");
        if (_showShotTemplates)
        {
            // Linear Shot Button
            if (GUILayout.Button("Create Linear Shot"))
            {
                if (templates.linearShot != null)
                {
                    InstantiatePrefab(templates.linearShot, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Random Timed Shot Button
            if (GUILayout.Button("Create Random Timed Shot"))
            {
                if (templates.randomTimedShot != null)
                {
                    InstantiatePrefab(templates.randomTimedShot, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Ring Shot Button
            if (GUILayout.Button("Create Ring Shot"))
            {
                if (templates.ringShot != null)
                {
                    InstantiatePrefab(templates.ringShot, GameManager.GAME_FIELD_CENTER);
                }
            }

            // Cone Shot Button
            if (GUILayout.Button("Create Cone Shot"))
            {
                if (templates.coneShot != null)
                {
                    InstantiatePrefab(templates.coneShot, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Random Position Ring Shot Button
            if (GUILayout.Button("Create Random Position Ring Shot"))
            {
                if (templates.randomPositionRingShot != null)
                {
                    InstantiatePrefab(templates.randomPositionRingShot, GameManager.GAME_FIELD_CENTER);
                }
            }
            
            // Random Shot Button
            if (GUILayout.Button("Create Random Shot"))
            {
                if (templates.randomShot != null)
                {
                    InstantiatePrefab(templates.randomShot, GameManager.GAME_FIELD_CENTER);
                }
            }

            // Static Rotation Shot Button
            if (GUILayout.Button("Create Static Rotation Shot"))
            {
                if (templates.staticRotationShot != null)
                {
                    InstantiatePrefab(templates.staticRotationShot, GameManager.GAME_FIELD_CENTER);
                }
            }

            GUILayout.EndScrollView();
        }
    }

    private void InstantiatePrefab(GameObject prefab, Vector3 pos)
    {
        GameObject gameObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (gameObj != null)
        {
            gameObj.transform.position = pos;
        }
    }
}
