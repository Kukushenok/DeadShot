using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponDisplay;
    [SerializeField] private BaseWeapon currentWeaponInstance;
    //UI
    public void SetWeaponToMe(WeaponScriptableObject weaponToSet)
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance.gameObject);
        }
        GameObject weaponObject = Instantiate(weaponToSet.weaponPrefab, weaponDisplay);
        currentWeaponInstance = weaponObject.GetComponent<BaseWeapon>();
    }
    public void UpdateUI()
    {

    }
}
