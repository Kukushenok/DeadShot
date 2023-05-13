using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected Transform weaponDisplayTransform;
    public AbstractWeapon currentWeaponInstance { get; private set; }
    public WeaponDescription currentWeaponObject { get; private set; }
    public virtual void UpdateUI()
    {

    }
    public void SetWeaponToMe(WeaponDescription weaponToSet)
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance.gameObject);
        }
        if (weaponToSet != null)
        {
            GameObject weaponObject = Instantiate(weaponToSet.weaponPrefab, weaponDisplayTransform);
            currentWeaponInstance = weaponObject.GetComponent<AbstractWeapon>();
            currentWeaponInstance.Initialize(this);
        }
        currentWeaponObject = weaponToSet;
    }
    public void PointWeaponAtPoint(Vector3 point)
    {
        Vector2 delta = point - weaponDisplayTransform.position;
        weaponDisplayTransform.right = delta.normalized;
        if (delta.x < 0) weaponDisplayTransform.Rotate(new Vector3(180, 0, 0));
    }
}
