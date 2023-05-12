using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon: MonoBehaviour 
{
    [SerializeField] public CharacterWeaponController owner;
    public abstract void StartShooting();
    public abstract void StopShooting();
    public abstract void Reload();
    public abstract void Initialize(CharacterWeaponController character);
}
