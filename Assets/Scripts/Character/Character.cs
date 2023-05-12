using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character singleton;
    public CharacterMovementController controller;
    public CharacterWeaponController weaponController;
    // Start is called before the first frame update
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }
    public static void AssignWeaponToCharater(WeaponScriptableObject weaponObject)
    {
        singleton.weaponController.SetWeaponToMe(weaponObject);
    }
}
