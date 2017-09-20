/*
Created By: David Zhang
Description: ScriptableObject that contains data for a general role which can be created as an asset file when we need more roles.
Requirements: None.
*/

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Role", menuName = "Role", order = 1)]
public class Roles : ScriptableObject
{
    public string RoleName;
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public GameObject SideScrollerModel;
    public GameObject TopDownHuntingModel;
    public GameObject TopDownHorseModel;
}
