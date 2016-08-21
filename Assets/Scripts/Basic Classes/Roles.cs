using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Role", menuName = "Role", order = 1)]
public class Roles : ScriptableObject {

    public string roleName;
    public int strength;
    public int dexterity;
    public int intelligence;
}
