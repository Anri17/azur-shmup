using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AzurProject
{
    [CustomEditor(typeof(SpellAttacks.SpellAttack))]
    public class SpellAttackEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SpellAttacks.SpellAttack spellAttack = (SpellAttacks.SpellAttack) target;

            DrawDefaultInspector();
            
            if (GUILayout.Button("(Debug) Start Spell"))
            {
                spellAttack.StartSpell();
            }
        }
    }
}
