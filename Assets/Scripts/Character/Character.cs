using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character singleton { get; private set; }
    public CharacterMovementController controller;
    public CharacterWeaponController weaponController;
    public uint score;
    // Start is called before the first frame update
    public void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }
    public static void AssignWeaponToCharater(WeaponDescription weaponObject)
    {
        if (singleton == null) return;
        singleton.weaponController.SetWeaponToMe(weaponObject);
    }
}
