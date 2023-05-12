using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float HP { get { return _HP; } }
    public float maxHP { get { return _maxHP; } }
    public bool isDead { get { return _HP <= 0; } }
    public UnityEvent<float> OnHPChanged;
    public UnityEvent OnDeath;

    [SerializeField] float _HP;
    [SerializeField] float _maxHP;
    public static void Damage(GameObject victim, float damage)
    {
        ChangeHP(victim, -damage);
    }

    public static void ChangeHP(GameObject victim, float delta)
    {
        Health healthComponent = victim.GetComponent<Health>();
        if (healthComponent != null)
        {
            bool wasDead = healthComponent.isDead;
            healthComponent._HP += delta;
            healthComponent.OnHPChanged.Invoke(healthComponent._HP);
            if(healthComponent.isDead && !wasDead)
            {
                healthComponent.OnDeath.Invoke();
            }
        }
    }

}
