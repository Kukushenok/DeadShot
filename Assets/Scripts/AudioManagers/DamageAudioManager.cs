using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health))]
public class DamageAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource damageAudioSource;
    private Health hp;
    private float wasHP = -1;

    private void Awake()
    {
        hp = GetComponent<Health>();
        wasHP = hp.HP;
        hp.OnHPChanged.AddListener(OnChangeHP);
    }
    private void OnChangeHP(float newHP, float maxHP)
    {
        if (newHP < wasHP)
        {
            damageAudioSource.Play();
        }
        wasHP = newHP;
    }
}
