using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutInstance : MonoBehaviour
{
    const float REMOVE_ANIM_TIME = 3;
    const string REMOVE_ANIM_KEY = "removing";
    [SerializeField] GameObject lightSource;
    [SerializeField] float removingTime;
    [SerializeField] Animator myAnimator;
    private void Awake()
    {
        Invoke("StartRemoving", removingTime - REMOVE_ANIM_TIME);
        lightSource.SetActive(GameloopManager.singleton.currentScore < GameloopManager.singleton.maxLevelScore);
    }
    private void StartRemoving()
    {
        Destroy(gameObject, REMOVE_ANIM_TIME);
        myAnimator.SetTrigger(REMOVE_ANIM_KEY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character == null) return;
        GameloopManager.singleton.AddOneScore();
        Destroy(gameObject);
    }
}
