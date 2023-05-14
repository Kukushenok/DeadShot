using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Rigidbody2D), typeof(TeamParticipant))]
public class GhostEnemyWithWeapon : GhostEnemy
{
    private GhostEnemyWeaponController weaponController;
    [SerializeField] private WeaponDescription weaponDescription;
    protected override void Awake()
    {
        base.Awake();
        weaponController = GetComponent<GhostEnemyWeaponController>();
        weaponController.SetWeaponToMe(weaponDescription);
    }
    protected override void FixedUpdate()
    {
        TeamParticipant p = TeamManager.GetPlayer(transform.position, maxSeeingRange);
        if (p != null)
        {
            MakeInertiaMoveTowards(p.transform.position - transform.position);
            weaponController.SpotTarget(p.transform.position);
        }
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        GameloopManager.mainCharacter.SetWeapon(weaponDescription);
    }
}
