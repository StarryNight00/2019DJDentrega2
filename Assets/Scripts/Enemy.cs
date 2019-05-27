using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Hollow")]

    public float                saveSpeed;
    private float               moveDirection = 1.0f;
    [SerializeField] float      invulnerabilityDuration = 1.0f;

    [SerializeField] Transform  groundSensor;
    [SerializeField] Transform  wallSensor;
    [SerializeField] Collider2D damageSensor;

    private SpriteRenderer sprite;
    private float invulnerabilityTimer;
    private float prevGravity;

    bool isInvulnerable
    {
        get
        {
            if (invulnerabilityTimer > 0.0f) return true;

            return false;
        }
        set
        {
            if (value)
            {
                invulnerabilityTimer = invulnerabilityDuration;
            }
            else
            {
                invulnerabilityTimer = 0.0f;
            }
        }
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        saveSpeed = moveSpeed;

        Player[] ps = FindObjectsOfType<Player>();
        foreach (Player p in ps)
            p.OnControlledChanged += OnJacobChangedController;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity.x = moveDirection * moveSpeed;

        rigidBody.velocity = currentVelocity;

        prevGravity = rigidBody.gravityScale;

        if (moveDirection < 0) transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        if (moveDirection > 0) transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        Collider2D collider = Physics2D.OverlapCircle(wallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
        if (collider != null)
        {
            moveDirection = -moveDirection;
        }

        collider = Physics2D.OverlapCircle(groundSensor.position, 2.0f, LayerMask.GetMask("Ground"));
        if (collider == null)
        {
            moveDirection = -moveDirection;
        }

       

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.ClearLayerMask();
        contactFilter.SetLayerMask(LayerMask.GetMask("Player"));

        Collider2D[] results = new Collider2D[6];
        
        int nCollisions = Physics2D.OverlapCollider(damageSensor, contactFilter, results);

        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                if (isInvulnerable) return;
                collider = results[i];

                Player player = collider.GetComponent<Player>();
                if (player)
                {
                    player.DealDamage();
                }
            }
        }

        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            sprite.enabled = Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2 == 0;

            Collider2D ownCollider = GetComponent<Collider2D>();

            if (ownCollider)
            {
                ownCollider.enabled = false;
                rigidBody.freezeRotation = true;
                rigidBody.gravityScale = 0.0f;
            }

            if (invulnerabilityTimer <= 0.0f)
            {
                moveSpeed = saveSpeed;
                sprite.enabled = true;
                ownCollider.enabled = true;
                rigidBody.freezeRotation = false;
                rigidBody.gravityScale = prevGravity;
            }
        }
    }

    public void GetNumb()
    {
        
        if (isInvulnerable) return;

        moveSpeed = 0;

        isInvulnerable = true;
    }

    private void OnJacobChangedController(Player sender)
    {
        if (sender is Jacob)
            Enable(sender.canControl);
    }

    private void Enable(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundSensor)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(groundSensor.position, 2.0f);
        }

        if (wallSensor)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(wallSensor.position, 2.0f);
        }
    }
}
