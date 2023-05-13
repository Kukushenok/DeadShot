using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public CharacterMovementController controller;
    public CharacterWeaponController weaponController;
    // Start is called before the first frame update
    public void Start()
    {
        //if (singleton == null)
        //{
        //    singleton = this;
        //}
    }
    //public static void AssignWeaponToCharater(WeaponDescription weaponObject)
    //{
    //    if (singleton == null) return;
    //    singleton.weaponController.SetWeaponToMe(weaponObject);
    //}
}
