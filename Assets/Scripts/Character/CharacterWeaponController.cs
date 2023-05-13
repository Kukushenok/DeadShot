using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
public class CharacterWeaponController : WeaponController
{
    public WeaponDescription weaponDefault;
    //UI
    public void Start()
    {
        SetWeaponToMe(weaponDefault);
    }
    public override void UpdateUI()
    {
        BulletCountDisplayer.singleton.UpdateUI();
    }
    public void Update()
    {
        if (currentWeaponInstance != null)
        {
            Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PointWeaponAtPoint(cursorWorldPos);
            if (Input.GetMouseButtonUp(0)) currentWeaponInstance.StopShooting();
            if (Input.GetMouseButtonDown(0)) currentWeaponInstance.StartShooting();
            if (Input.GetKeyDown(KeyCode.R)) currentWeaponInstance.Reload();
        }
    }
}
