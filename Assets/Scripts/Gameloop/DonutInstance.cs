using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutInstance : MonoBehaviour
{
    const float REMOVE_ANIM_TIME = 3;
    const string REMOVE_ANIM_KEY = "removing";
    [SerializeField] private float removingTime;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private Animator myAnimator;
    private void Awake()
    {
        Invoke("StartRemoving", removingTime - REMOVE_ANIM_TIME);
        GameloopManager.singleton.currentLevelInstance.AddObjectToLevel(gameObject);
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
        if (!GameloopManager.isLevelCompleted) Instantiate(pickupEffect, transform.position, Quaternion.identity);
        GameloopManager.singleton.AddOneScore();
        character.ResetWeapon();
        Destroy(gameObject);
    }
}
