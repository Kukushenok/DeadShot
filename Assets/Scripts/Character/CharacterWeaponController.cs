using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponDisplay;
    [SerializeField] private BaseWeapon currentWeaponInstance;
    public WeaponScriptableObject weaponDefault;
    private bool isMouseBeenDown;
    //UI
    public void Start()
    {
        SetWeaponToMe(weaponDefault);
    }
    public void SetWeaponToMe(WeaponScriptableObject weaponToSet)
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance.gameObject);
        }
        GameObject weaponObject = Instantiate(weaponToSet.weaponPrefab, weaponDisplay);
        currentWeaponInstance = weaponObject.GetComponent<BaseWeapon>();
        currentWeaponInstance.Initialize(this);
    }
    public void UpdateUI()
    {
        
    }
    public void Update()
    {
        if (currentWeaponInstance != null)
        {
            Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = cursorWorldPos - weaponDisplay.position;
            weaponDisplay.right = delta.normalized;
            bool isMouseDown = Input.GetMouseButton(0);
            if(!isMouseBeenDown && isMouseDown) currentWeaponInstance.StartShooting();
            if (isMouseBeenDown && !isMouseDown) currentWeaponInstance.StopShooting();
            isMouseBeenDown = isMouseDown;
            if (Input.GetKeyDown(KeyCode.R)) currentWeaponInstance.Reload();
        }
    }
}
