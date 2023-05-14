using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameplayMusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot intenceMusicSnapshot;
    [SerializeField] private AudioMixerSnapshot normalMusicSnapshot;
    [SerializeField] private float transitionTime;
    [SerializeField] [Range(0, 1)] private float healthCriticalPercents = 0.3f;
    [SerializeField] private Health hpToSubscribe;
    private bool isIntence;
    // Start is called before the first frame update
    void Start()
    {
        normalMusicSnapshot.TransitionTo(0);
        hpToSubscribe.OnHPChanged.AddListener(OnChangeHP);
    }

    void OnChangeHP(float currHP, float maxHP)
    {
        bool shouldBeIntence = currHP / maxHP <= healthCriticalPercents;
        if (shouldBeIntence && !isIntence)
        {
            intenceMusicSnapshot.TransitionTo(transitionTime);
        }
        if (!shouldBeIntence && isIntence)
        {
            normalMusicSnapshot.TransitionTo(transitionTime);
        }
        isIntence = shouldBeIntence;
    }
}
