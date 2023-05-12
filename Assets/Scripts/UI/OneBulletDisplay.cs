using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBulletDisplay : MonoBehaviour
{
    private BulletCountDisplayer displayer;
    [SerializeField] private UIPrimitivePhysics physics;
    
    [SerializeField] private Vector2 verticalVelocityRange;
    [SerializeField] Vector2 horizontalVelocityRange;
    [SerializeField] float lifetime;
    public void Initialize(BulletCountDisplayer displayer)
    {
        this.displayer = displayer;
    }
    public void PunchOut()
    {
        physics.enabled = true;
        physics.velocity = new Vector2(Random.Range(horizontalVelocityRange.x, horizontalVelocityRange.y),
            Random.Range(verticalVelocityRange.x, verticalVelocityRange.y));
        Destroy(gameObject, lifetime);
    }
}
