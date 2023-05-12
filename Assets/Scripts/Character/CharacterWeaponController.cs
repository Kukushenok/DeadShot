using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
public class CharacterWeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponDisplayTransform;
    public AbstractWeapon currentWeaponInstance { get; private set; }
    public WeaponDescription currentWeaponObject { get; private set; }
    public WeaponDescription weaponDefault;
    private bool isMouseBeenDown;
    //UI
    public void Start()
    {
        SetWeaponToMe(weaponDefault);
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
    public void UpdateUI()
    {
        BulletCountDisplayer.singleton.UpdateUI();
    }
    public void Update()
    {
        if (currentWeaponInstance != null)
        {
            Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = cursorWorldPos - weaponDisplayTransform.position;
            weaponDisplayTransform.right = delta.normalized;
            bool isMouseDown = Input.GetMouseButton(0);
            if(!isMouseBeenDown && isMouseDown) currentWeaponInstance.StartShooting();
            if (isMouseBeenDown && !isMouseDown) currentWeaponInstance.StopShooting();
            isMouseBeenDown = isMouseDown;
            if (Input.GetKeyDown(KeyCode.R)) currentWeaponInstance.Reload();
        }
    }
}
