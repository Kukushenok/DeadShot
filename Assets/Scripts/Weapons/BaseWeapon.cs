using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon: AbstractWeapon
{
    public enum WeaponState { Idle, Reloading, Shooting };
    public uint bulletCount { get { return _bulletCount; } private set { _bulletCount = value; } }
    public uint maxBulletCount { get { return _maxBulletCount; } private set { _maxBulletCount = value; } }
    public WeaponState currentState { get; private set; }

    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private uint _maxBulletCount;
    private uint _bulletCount;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float text;
    [SerializeField] [Min(-1)] private int bulletSeries = -1;
    [SerializeField] [Min(0.0f)] private float reloadAllBulletsTime;
    [SerializeField] [Min(1)] private uint bulletCountAtOnce = 1;
    [SerializeField] [Min(0.0f)] private float shootTime;
    [SerializeField] [Min(0.0f)] private float angleScatter;
    [SerializeField] private Vector2 bulletSpeedRange;
    private Coroutine shootCoroutine;
    public override void StartShooting()
    {
        shootCoroutine = StartCoroutine(ShootCoroutine());
    }
    public override void StopShooting()
    {
        StopCoroutine(shootCoroutine);
    }
    public override void Initialize(CharacterWeaponController character)
    {
        owner = character;
        bulletCount = maxBulletCount;
    }
    private void ShootOneBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        bullet.transform.Rotate(new Vector3(0, 0, Random.Range(-angleScatter, angleScatter)));
        float speed = Random.Range(bulletSpeedRange.x, bulletSpeedRange.y);
        Vector2 velocity = bullet.transform.right * speed;

    }
    IEnumerator ShootCoroutine()
    {
        uint i = 0;
        while (bulletCount > 0)
        {
            yield return new WaitForSeconds(shootTime);
            for (uint j = 0; j < bulletCountAtOnce; j++)
            {
                ShootOneBullet();
            }
            bulletCount--;
            owner.UpdateUI();
            if (bulletSeries > 0)
            {
                i++;
                if (i > bulletSeries) break;
            }
        }
    }
    IEnumerator Reload()
    {
        if (currentState == WeaponState.Idle)
        {
            currentState = WeaponState.Reloading;
            yield return new WaitForSeconds(reloadAllBulletsTime);
            bulletCount = maxBulletCount;
        }
    }
    
}
