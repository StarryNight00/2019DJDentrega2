using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public delegate void EventHandler(Player sender);
    //evento que controla evento para verificar Hollow vv
    public event EventHandler OnControlledChanged;

    [Header("Player")]

    [SerializeField] float  invulnerabilityDuration = 1.0f;

    protected List<string>  inventory;
    
    private SpriteRenderer  sprite;
    protected Animator      animator;
    public Transform        spawnPoint1;


    public bool             canControl;
    float                   invulnerabilityTimer;

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
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        inventory = new List<string>();
        CanControl = canControl;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (!canControl)
        {
            return;
        }

        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity.x = hAxis * moveSpeed * Time.fixedDeltaTime;

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
        vAxis = Input.GetAxis("Vertical");

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if (hAxis > 0.0f && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        animator.SetFloat("AbsVelocityX", Mathf.Abs(rigidBody.velocity.x));


        if(invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            sprite.enabled = Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2 == 0;

            if(invulnerabilityTimer <= 0.0f)
            {
                sprite.enabled = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 2.0f);
    }

    public void DealDamage()
    {
        if (isInvulnerable) return;

        isInvulnerable = true;
        transform.position = spawnPoint1.position;
    }

    public void AddToIventory(string itemName)
    {
        if (inventory.IndexOf(itemName) == -1)
        {
            inventory.Add(itemName);
        }
    }

    public bool HasItem(string itemName)
    {
        int index = inventory.IndexOf(itemName);

        return (index != -1);
    }

    public void RemoveFromIventory(string itemName)
    {
        int index = inventory.IndexOf(itemName);
        if (index != -1)
        {
            inventory.RemoveAt(index);
        }
    }


    public virtual bool CanControl
    {
        get => canControl;
        set
        {
            canControl = value;

            GetComponent<SpriteRenderer>().enabled = value;
            GetComponent<Collider2D>().enabled = value;
            rigidBody.gravityScale = value ? 1.0f : 0.0f;
            rigidBody.velocity = Vector2.zero;
            if (OnControlledChanged != null)
                OnControlledChanged(this);
        }
    }
}
