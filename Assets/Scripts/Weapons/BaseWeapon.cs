using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon: AbstractWeapon
{
    public enum WeaponState { Idle, Reloading, Shooting };
    public int bulletCount { get { return _bulletCount; } private set { _bulletCount = value; } }
    public int maxBulletCount { get { return _maxBulletCount; } private set { _maxBulletCount = value; } }
    public WeaponState currentState { get; private set; }

    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private int _maxBulletCount;
    [SerializeField] private int _bulletCount;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float text;
    [SerializeField] [Min(-1)] private int bulletSeries = -1;
    [SerializeField] [Min(0.0f)] private float reloadAllBulletsTime;
    [SerializeField] [Min(1)] private int bulletCountAtOnce = 1;
    [SerializeField] [Min(0.0f)] private float shootTime;
    [SerializeField] [Min(0.0f)] private float angleScatter;
    [SerializeField] [Min(0.0f)] private float recoilTime;
    [SerializeField] private Vector2 bulletSpeedRange;
    private Coroutine shootCoroutine;
    private bool isHeld = false;
    public override void StartShooting()
    {
        if (currentState == WeaponState.Idle)
        {
            if (bulletCount == 0)
            {
                Reload();
                return;
            }
            currentState = WeaponState.Shooting;
            shootCoroutine = StartCoroutine(ShootCoroutine());
        }
        isHeld = true;
    }
    public override void StopShooting()
    {
        if (currentState == WeaponState.Shooting && shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
            Invoke("SetIdleState", recoilTime);
        }
        isHeld = false;
    }
    public void SetIdleState()
    {
        currentState = WeaponState.Idle;
        if (isHeld) StartShooting();
    }
    public override void Reload()
    {
        StartCoroutine(ReloadCoroutine());
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
        bullet.GetComponent<Rigidbody2D>().velocity = velocity;
        bullet.GetComponent<BaseBullet>().SetBulletOwner(owner.gameObject);
    }
    IEnumerator ShootCoroutine()
    {
        uint i = 0;
        while (bulletCount > 0)
        {
            if (currentState != WeaponState.Shooting) break;
            for (uint j = 0; j < bulletCountAtOnce; j++)
            {
                ShootOneBullet();
            }
            bulletCount--;
            owner.UpdateUI();
            yield return new WaitForSeconds(shootTime);
            if (bulletSeries > 0)
            {
                i++;
                if (i > bulletSeries) break;
            }
        }
        currentState = WeaponState.Idle;
    }
    IEnumerator ReloadCoroutine()
    {
        if (currentState == WeaponState.Idle)
        {
            currentState = WeaponState.Reloading;
            yield return new WaitForSeconds(reloadAllBulletsTime);
            bulletCount = maxBulletCount;
            owner.UpdateUI();
            currentState = WeaponState.Idle;
        }
    }
    
}
