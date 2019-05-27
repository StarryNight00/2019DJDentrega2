using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ItemsPick
{
    [SerializeField] string itemName;

    protected override void NotifyPickUp(Player player)
    {
        player.AddToIventory(itemName);
    }
}
