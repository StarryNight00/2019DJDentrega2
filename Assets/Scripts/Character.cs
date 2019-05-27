using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character")]

    protected Rigidbody2D rigidBody;
    protected float hAxis;
    protected float vAxis;
    public float moveSpeed = 5000.0f;
    public bool isOnGround { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));
        isOnGround = collider != null;

    }
}
