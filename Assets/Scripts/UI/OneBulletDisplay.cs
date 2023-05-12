using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBulletDisplay : MonoBehaviour
{
    private BulletCountDisplayer displayer;
    [SerializeField] Animator myAnimator;
    [SerializeField] private UIPrimitivePhysics physics;
    
    [SerializeField] private Vector2 verticalVelocityRange;
    [SerializeField] Vector2 horizontalVelocityRange;
    [SerializeField] float lifetime;
    public void Initialize(BulletCountDisplayer displayer, float init_duration = 0)
    {
        this.displayer = displayer;
        Invoke("SetVisible", init_duration);
    }
    public void SetVisible()
    {
        gameObject.SetActive(true);
    }
    public void PunchOut()
    {
        if (!gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }
        physics.enabled = true;
        physics.velocity = new Vector2(Random.Range(horizontalVelocityRange.x, horizontalVelocityRange.y),
            Random.Range(verticalVelocityRange.x, verticalVelocityRange.y));
        Destroy(gameObject, lifetime);
    }
}
