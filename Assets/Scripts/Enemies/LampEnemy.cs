using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LampEnemy : MonoBehaviour
{
    [SerializeField] private bool animationDamageTrigger;
    [SerializeField] private Transform damagePoint;
    [SerializeField] private float damageRadius;
    [SerializeField] private float damage;
    [SerializeField] private Animator animator;
    private bool damagedBefore = false;
    public void EnableMyself()
    {
        animator.Play("enabled");
    }
    public void LateUpdate()
    {
        if (animationDamageTrigger && !damagedBefore)
        {
            if (Character.singleton == null) return;
            Vector3 delta = Character.singleton.transform.position - damagePoint.position;
            if (delta.magnitude < damageRadius)
            {
                Health.Damage(Character.singleton.gameObject, damage);
                damagedBefore = true;
            }
        }
        else if(!animationDamageTrigger && damagedBefore)
        {
            damagedBefore = false;
        }
    }
}
