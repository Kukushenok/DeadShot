using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Контроль
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementController : InertiaMovementController
{
    public bool turnedLeft { get; private set; }
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction.x > float.Epsilon)
        {
            turnedLeft = direction.x > 0;
        }

        MakeInertiaMoveTowards(direction);
    }
}
