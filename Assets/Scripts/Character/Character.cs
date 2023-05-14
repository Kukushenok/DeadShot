using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public CharacterMovementController controller { get => _controller; }
    public CharacterWeaponController weaponController { get => _weaponController; }
    public Health myHP { get => _myHP; }
    [SerializeField] private CharacterMovementController _controller;
    [SerializeField] private CharacterWeaponController _weaponController;
    [SerializeField] private Health _myHP;
    public void OnDeath()
    {
        GameloopManager.singleton.OnCharacterDeath();
    }
    public void SetWeapon(WeaponDescription description)
    {
        if (_weaponController.currentWeaponObject != description)
        {
            _weaponController.SetWeaponToMe(description);
        }
    }
    public void ResetWeapon()
    {
        SetWeapon(_weaponController.weaponDefault);
    }
}
