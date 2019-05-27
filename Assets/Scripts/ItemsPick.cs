using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            NotifyPickUp(player);
            Destroy(gameObject);
        }
    }

    protected virtual void NotifyPickUp(Player player)
    {

    }

}
