using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ShotControl : MonoBehaviour
{
    //CONTROLS THE SHOT ABILITY. INSTATIATES THE ARROW

    Player              player;
    PlayerController    playerCtrl;

    private bool        arrowShot;

    public float        cooldownTime = 2.5f;
    public GameObject   projectile;

    float cooldown;

    void Start()
    {
        cooldown = 0.0f;
    }

    void FixedUpdate()
    { 
        cooldown -= Time.fixedDeltaTime;

        if (cooldown < 0.0f)
        {
            arrowShot = Input.GetButton("Fire1");

            if (arrowShot)
            {
                Shot();
            }
        }
    }

    void Shot()
    {
        Quaternion rotation = transform.rotation;
        Instantiate(projectile, transform.position, rotation);
        cooldown = cooldownTime;
    }

}
