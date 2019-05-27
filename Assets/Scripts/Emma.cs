using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emma : Player
{
    [Header("Emma")]

    private float       floatVelocity = 200.0f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!canControl)
        {
            return;
        }

        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity.x = hAxis * moveSpeed * Time.fixedDeltaTime;
        currentVelocity.y = vAxis * moveSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Vertical"))
        {
            currentVelocity.y = vAxis * moveSpeed * Time.fixedDeltaTime;
        }

        if (Input.GetButtonUp("Vertical"))
        {
            currentVelocity.y = -floatVelocity;
        }

        rigidBody.velocity = currentVelocity;
    }

    protected override void Update()
    {
        base.Update();

        
        if (!canControl)
        {
            return;
        }

        hAxis = Input.GetAxis("Horizontal");

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        animator.SetFloat("AbsVelocityX", Mathf.Abs(rigidBody.velocity.x));
        
    }


}
