using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfItem : MonoBehaviour
{
    [SerializeField] string itemName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            if (player.HasItem(itemName))
            {
                Destroy(gameObject);
            }
        }
    }
}
