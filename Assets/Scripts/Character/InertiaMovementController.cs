using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertiaMovementController : MonoBehaviour
{
    [SerializeField] protected float characterSpeed;
    [SerializeField] protected float knockInertiaSet = 20;
    [SerializeField] protected float knockInertiaDecreaseSpeed = 10;
    [SerializeField] protected float minimumInertia = 0;
    protected float inertia = 0;
    protected Rigidbody2D rg;
    protected Vector2 velocityChange;

    // Start is called before the first frame update
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    public static void Punch(GameObject gm, Vector2 punch)
    {
        InertiaMovementController controller = gm.GetComponent<InertiaMovementController>();
        if (controller != null)
        {
            controller.KnockMeTowards(punch);
        }
    }
    public void KnockMeTowards(Vector2 punch)
    {
        rg.velocity += punch;
        inertia = knockInertiaSet;
    }
    protected void MakeInertiaMoveTowards(Vector2 direction)
    {
        if (direction.magnitude > 1) direction.Normalize();
        if (inertia > 0)
        {
            inertia -= Time.fixedDeltaTime * knockInertiaDecreaseSpeed;
        }
        else if (inertia != minimumInertia)
        {
            inertia = minimumInertia;
        }
        direction *= characterSpeed;
        Vector2 newVelocity = Vector2.SmoothDamp(rg.velocity, direction, ref velocityChange, Time.fixedDeltaTime * inertia);
        rg.velocity = newVelocity;
    }
}
