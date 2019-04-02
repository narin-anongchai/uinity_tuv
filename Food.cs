using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : InventoryItemBase
{
    //public GameObject panelQuest1;

    public int FoodPoints = 20;

    public override void OnUse()
    {
        GameManager.Instance.Player.Eat(FoodPoints);

        GameManager.Instance.Player.Inventory.RemoveItem(this);
        //panelQuest1.SetActive(true);
        Destroy(this.gameObject);
    }
}
