using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //OBJECT ARROW. STATES MOVEMENT AND CONTROLS COLIDER
    [Header ("Arrow")]

    [SerializeField] Collider2D collider;

    private float       speed = 400;
    private float       horizontalAmplitude = 0;
    private float       horizontalFrequency = 0;
    private float       timer;
    private float       angle;

    public Vector3      moveVector;
    private Vector3     startPos;
    private Vector3     hVector;
    public Transform    hitPos;

    void Start()
    {
        collider = GetComponent<Collider2D>();

        hVector.Normalize();

        timer = 0.0f;
        angle = 0.0f;

        hVector = new Vector3(moveVector.y, moveVector.x, moveVector.z);

        startPos = transform.position;
        
    }

    private void FixedUpdate()
    {
        hVector.y = startPos.y;
        timer += Time.fixedDeltaTime;
        angle += horizontalFrequency * Time.fixedDeltaTime;

        float s = horizontalAmplitude * Mathf.Sin(angle * Mathf.Deg2Rad);

        transform.position = startPos + moveVector * speed * timer + hVector * s;
    }

    private void Update()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.ClearLayerMask();
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        Collider2D[] results = new Collider2D[6];

        int nCollisions = Physics2D.OverlapCollider(collider, contactFilter, results);

        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                collider = results[i];

                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.GetNumb();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(hitPos.position, 2.0f);
    }

}
