using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.UI
{
    public class BulletCountDisplayer : MonoBehaviour
    {
        public static BulletCountDisplayer singleton { get; private set; }
        [SerializeField] private Transform bulletDisplaysParent;
        [SerializeField] private float bulletInitTime;
        private GameObject lastPatronDisplay;
        private List<OneBulletDisplay> allDisplays = new List<OneBulletDisplay>();
        public void Start()
        {
            singleton = this;
        }
        public void UpdateUI()
        {
            if (Character.singleton == null) return;
            WeaponDescription weaponDescription = Character.singleton.weaponController.currentWeaponObject;
            if (weaponDescription == null) return;
            AbstractWeapon weapon = Character.singleton.weaponController.currentWeaponInstance;
            if (weapon is BaseWeapon)
            {
                BaseWeapon displaying = (BaseWeapon)weapon;
                UpdateDisplayForBaseWeapon(displaying, weaponDescription);
            }
            else
            {
                Debug.LogError("No UI display for unknown weapon type");
                Debug.Break();
            }
        }
        public void UpdateDisplayForBaseWeapon(BaseWeapon weap, WeaponDescription descr)
        {
            if (lastPatronDisplay != descr.weaponUIPatronDisplay)
            {
                RemoveBullets(allDisplays.Count);
            }
            int delta = allDisplays.Count - weap.bulletCount;
            if (delta > 0)
            {
                RemoveBullets(delta);
            }
            else if (delta < 0)
            {
                AddBullets(descr.weaponUIPatronDisplay, -delta);
            }
            lastPatronDisplay = descr.weaponUIPatronDisplay;
        }
        public void RemoveBullets(int count)
        {
            if (allDisplays.Count < count) count = allDisplays.Count;
            int endIndex = allDisplays.Count - 1 - count;
            for (int i = allDisplays.Count - 1; i > endIndex; i--)
            {
                allDisplays[i].transform.parent = transform.parent;
                allDisplays[i].PunchOut();
                allDisplays.RemoveAt(i);
            }
        }
        public void AddBullets(GameObject prefab, int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject instance = Instantiate(prefab, bulletDisplaysParent);
                instance.SetActive(false);
                OneBulletDisplay display = instance.GetComponent<OneBulletDisplay>();
                display.Initialize(this, i * bulletInitTime / count);
                allDisplays.Add(display);
            }
        }
    }

}