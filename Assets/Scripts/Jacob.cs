using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jacob : Player
{
    [Header("Jacob")]

    private float       jumpVelocity = 200.0f;
    private float       timeOfJump = -1000.0f;
    private float       jumpTime = 0.18f;
    private bool        isJumpPressed;

    public Collider2D   groundCollider;
    public Collider2D   airCollider;

    protected override void FixedUpdate()
    {
        if (!canControl)
        {
            return;
        }

        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity.x = hAxis * moveSpeed * Time.fixedDeltaTime;

        if (isJumpPressed && isOnGround)
        {
            currentVelocity.y = jumpVelocity;
            timeOfJump = Time.time;
        }
        else if (isJumpPressed && (Time.time - timeOfJump) < jumpTime)
        {
            currentVelocity.y = jumpVelocity;
        }
        else
        {
            timeOfJump = -1000.0f;
        }

        rigidBody.velocity = currentVelocity;

        groundCollider.enabled = isOnGround;
        airCollider.enabled = !isOnGround;

    }

    protected override void Update()
    {
        base.Update();


        if (!canControl)
        {
            return;
        }

        hAxis = Input.GetAxis("Horizontal");

        isJumpPressed = Input.GetButton("Jump");

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        animator.SetFloat("AbsVelocityX", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("VelocityY", rigidBody.velocity.y);
        animator.SetBool("IsOnGround", isOnGround);
    }
}
