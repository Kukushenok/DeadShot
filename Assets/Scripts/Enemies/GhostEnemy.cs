using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health))]
public class GhostEnemy : InertiaMovementController
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject donutPrefab;
    [SerializeField] private float donutDropPercentrage;
    [SerializeField] private float baseDamage;
    [SerializeField] private float punchForce;
    [SerializeField] private float minOpacity;
     
    [SerializeField] private SpriteRenderer spriteRenderer;
    Vector3 enemyPosition { get
        {
            if (Character.singleton == null) return transform.position;
            return Character.singleton.transform.position;
        }
    }
    private Health healthComponent;
    protected override void Awake()
    {
        base.Awake();
        healthComponent = GetComponent<Health>();
        healthComponent.OnHPChanged.AddListener(OnChangeHP);
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
    public void OnChangeHP(float hp, float maxHP)
    {
        Color currColor = spriteRenderer.color;
        float range = (1 - minOpacity / 255);
        range *= hp / maxHP;
        currColor.a = minOpacity / 255 + range;
        spriteRenderer.color = currColor;
    }
    public void OnDeath()
    {
        Destroy(gameObject);
        if (Random.Range(0, 1) <= donutDropPercentrage)
        {
            Instantiate(donutPrefab, transform.position, Quaternion.identity);
        }
        Instantiate(deathParticles, transform.position, transform.rotation);
    }
}
