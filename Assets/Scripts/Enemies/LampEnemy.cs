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
    public void SetEnable(bool enable)
    {
        animator.SetBool("enabled", enable);
        enabled = enable;
    }
    public void LateUpdate()
    {
        if (animationDamageTrigger && !damagedBefore)
        {
            TeamParticipant damagedPlayer = TeamManager.GetPlayer(damagePoint.position, damageRadius);
            if (damagedPlayer != null)
            {
                Health.Damage(damagedPlayer.gameObject, damage);
                damagedBefore = true;
            }
        }
        else if(!animationDamageTrigger && damagedBefore)
        {
            damagedBefore = false;
        }
    }
}
