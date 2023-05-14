using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TeamParticipant))]
public class GhostEnemy : InertiaMovementController
{
    [SerializeField] protected GameObject deathParticlesPrefab;
    [SerializeField] protected GameObject donutPrefab;
    [SerializeField] [Range(0, 1)] protected float donutDropPercentrage;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float punchForce;
    [SerializeField] protected float minOpacity;
    [SerializeField] protected float maxSeeingRange = 5;
     
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected Vector3 enemyPosition { get
        {
            TeamParticipant player = TeamManager.GetPlayer(transform.position, maxSeeingRange);
            if (!player) return transform.position;
            return player.transform.position;
        }
    }
    protected Health healthComponent;
    protected TeamParticipant teamParticipant;
    protected override void Awake()
    {
        base.Awake();
        healthComponent = GetComponent<Health>();
        healthComponent.OnHPChanged.AddListener(OnChangeHP);
        teamParticipant = GetComponent<TeamParticipant>();
        TeamManager.singleton.enemyTeam.AssignToTeam(teamParticipant);
    }
    protected virtual void FixedUpdate()
    {
        Vector3 direction = enemyPosition - transform.position;
        MakeInertiaMoveTowards(direction);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TeamParticipant participant = collision.gameObject.GetComponent<TeamParticipant>();
        if (participant == null) return;
        if (participant.currentTeam != teamParticipant.currentTeam)
        {
            Vector3 direction = collision.transform.position - transform.position;
            Health.Damage(collision.gameObject, baseDamage);
            Punch(collision.gameObject, direction.normalized * punchForce);
            KnockMeTowards(-direction.normalized * punchForce);
        }
    }
    protected virtual void OnChangeHP(float hp, float maxHP)
    {
        Color currColor = spriteRenderer.color;
        float range = (1 - minOpacity / 255);
        range *= hp / maxHP;
        currColor.a = minOpacity / 255 + range;
        spriteRenderer.color = currColor;
    }
    protected virtual void OnDeath()
    {
        Destroy(gameObject);
        if (Random.Range(0.0f, 1.0f) <= donutDropPercentrage)
        {
            Instantiate(donutPrefab, transform.position, Quaternion.identity);
        }
        Instantiate(deathParticlesPrefab, transform.position, transform.rotation);
    }
}
