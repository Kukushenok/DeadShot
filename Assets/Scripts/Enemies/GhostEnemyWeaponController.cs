using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyWeaponController : WeaponController
{
    [SerializeField] private Vector2 shootHoldMinMax;
    [SerializeField] private Vector2 shootDelayMinMax;
    [SerializeField] private float minShootRange;
    private bool canShootAgain = true;
    public void SpotTarget(Vector3 position)
    {
        if ((transform.position - position).magnitude < minShootRange)
        {
            
            if (canShootAgain)
            {
                StartCoroutine(ShootingCoroutine());
                canShootAgain = false;
            }
        }
        PointWeaponAtPoint(position);
    }
    public IEnumerator ShootingCoroutine()
    {
        currentWeaponInstance.StartShooting();
        yield return new WaitForSeconds(Random.Range(shootHoldMinMax.x, shootHoldMinMax.y));
        currentWeaponInstance.StopShooting();
        yield return new WaitForSeconds(Random.Range(shootDelayMinMax.x, shootDelayMinMax.y));
        canShootAgain = true;
    }
    
}
