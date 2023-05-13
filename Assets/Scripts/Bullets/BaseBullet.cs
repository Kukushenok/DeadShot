using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject owner;
    [SerializeField] private float baseDamage;
    [SerializeField] private float punchForce;
    [SerializeField] private bool friendlyFire;
    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
    public void SetBulletOwner(GameObject owner)
    {
        this.owner = owner;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (!friendlyFire)
        {
            if (TeamManager.IsInTheSameTeam(collision.gameObject, owner)) return;
        }
        if (collision.gameObject != owner.gameObject)
        {
            Health.Damage(collision.gameObject, baseDamage);
            InertiaMovementController.Punch(collision.gameObject, transform.right * punchForce);
            Destroy(gameObject);
        }
    }
}
