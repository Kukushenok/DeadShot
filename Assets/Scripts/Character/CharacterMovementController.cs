using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Контроль
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float characterSpeed;
    [SerializeField] private float knockInertiaSet = 20;
    private float inertia = 0;
    public bool turnedLeft { get; private set; }
    private Rigidbody2D rg;
    private Vector2 velocityChange;
   
    // Start is called before the first frame update
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    public void KnockAtDirection(Vector2 punch)
    {
        rg.velocity += punch;
        inertia = knockInertiaSet;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction.magnitude > 1) direction.Normalize();
        direction *= characterSpeed;
        if (direction.x > float.Epsilon)
        {
            turnedLeft = direction.x > 0;
        }
        Vector2 newVelocity = Vector2.SmoothDamp(rg.velocity, direction, ref velocityChange, Time.fixedDeltaTime * inertia);
        rg.velocity = newVelocity;
    }
}
