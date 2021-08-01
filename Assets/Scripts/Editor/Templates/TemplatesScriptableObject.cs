using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Templates", menuName = "Templates")]
public class TemplatesScriptableObject : ScriptableObject
{
    [Header("Enemy Templates")]
    public GameObject smallRedShipGameObject;
    public GameObject mediumPurpleShipGameObject;
    public GameObject turretGameObject;
    public GameObject magicStoneGameObject;
    public GameObject blueFairyGameObject;
    public GameObject redFairyGameObject;

    [Header("Movement Templates")]
    public GameObject linearMovement;
    public GameObject bezierMovement;
    public GameObject randomMovement;

    [Header("Shot Templates")]
    public GameObject linearShot;
    public GameObject randomTimedShot;
    public GameObject ringShot;
    public GameObject coneShot;
    public GameObject randomPositionRingShot;
    public GameObject randomShot;
    public GameObject staticRotationShot;

    [Header("Spell Attack Template")]
    public GameObject spellAttack;
}
