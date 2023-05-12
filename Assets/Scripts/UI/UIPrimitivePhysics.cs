using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIPrimitivePhysics : MonoBehaviour
{
    public Vector2 velocity;
    public float gravityScale = 1;
    public float angularVelocity;
    [SerializeField] private float drag;
    [SerializeField] private float angularDrag;
    private void FixedUpdate()
    {
        velocity = velocity * (1 - drag * Time.deltaTime);
        transform.position += new Vector3(velocity.x, velocity.y);
        velocity += Physics2D.gravity * gravityScale * Time.fixedDeltaTime;
        angularVelocity = angularVelocity * (1 - angularDrag * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, angularVelocity));
        if (velocity.magnitude < float.Epsilon) velocity = Vector2.zero;
        if (angularVelocity < float.Epsilon) angularVelocity = 0;
    }
}
