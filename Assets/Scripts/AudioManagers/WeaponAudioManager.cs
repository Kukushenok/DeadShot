using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class WeaponAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayShotSound()
    {
        audioSource.PlayOneShot(shootClip);
    }
    public void PlayReloadSound()
    {
        audioSource.PlayOneShot(reloadClip);
    }
}
