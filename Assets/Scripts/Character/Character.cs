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
    // Start is called before the first frame update
    public void Start()
    {
        //if (singleton == null)
        //{
        //    singleton = this;
        //}
    }
}
