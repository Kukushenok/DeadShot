using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : InertiaMovementController
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float baseDamage;
    [SerializeField] private float punchForce;
    Vector3 enemyPosition { get
        {
            if (Character.singleton == null) return transform.position;
            return Character.singleton.transform.position;
        }
    }
    private void FixedUpdate()
    {
        Vector3 direction = enemyPosition - transform.position;
        MakeInertiaMoveTowards(direction);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Character.singleton == null) return;
        if (collision.gameObject == Character.singleton.gameObject)
        {
            Vector3 direction = enemyPosition - transform.position;
            Health.Damage(collision.gameObject, baseDamage);
            InertiaMovementController.Punch(collision.gameObject, direction.normalized * punchForce);
            KnockMeTowards(-direction.normalized * punchForce);
        }
    }
    public void OnDeath()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
    }
}
