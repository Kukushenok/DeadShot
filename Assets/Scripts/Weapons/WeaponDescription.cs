using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Roguelike/WeaponObject", order = 51)]
public class WeaponDescription : ScriptableObject
{
    public string localizedName;
    public GameObject weaponPrefab;
    public GameObject weaponUIPatronDisplay;
}
