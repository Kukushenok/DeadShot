using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TransferPortal : MonoBehaviour
{
    private const string ENABLED_ANIM_VAR = "enabled";
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource transitionAvailableAudio;
    public void Awake()
    {
        GameloopManager.OnScoreChange.AddListener(OnScoreChanged);
        enabled = false;
    }
    public void OnScoreChanged(int score)
    {
        animator.SetBool(ENABLED_ANIM_VAR, GameloopManager.isLevelCompleted);
        if (!enabled && GameloopManager.isLevelCompleted)
        {
            transitionAvailableAudio.Play();
        }
        enabled = GameloopManager.isLevelCompleted;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameloopManager.singleton.currentScore != GameloopManager.singleton.maxLevelScore) return;
        Character character = collision.gameObject.GetComponent<Character>();
        if (character == null) return;
        GameloopManager.singleton.NextLevel();
        animator.SetBool(ENABLED_ANIM_VAR, false);
        enabled = false;
    }
}
