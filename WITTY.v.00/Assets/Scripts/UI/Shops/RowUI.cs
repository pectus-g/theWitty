using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Shops;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI.Shops{
public class RowUI : MonoBehaviour
{
    [SerializeField]
    Image iconField;

    [SerializeField]
    TextMeshProUGUI nameField;

    [SerializeField]
    TextMeshProUGUI availabilityField;

    [SerializeField]
    TextMeshProUGUI priceField;

    [SerializeField]
    TextMeshProUGUI quantityField;

    Shop currentShop=null;
    ShopItem item= null;
    public void Setup(Shop currentShop, ShopItem item)
    {
        this.currentShop=currentShop;
        this.item=item;
        //points the shopitem
        iconField.sprite=item.GetIcon();
        nameField.text=item.GetName();
        availabilityField.text=$"{item.GetAvailability()}";//interpolated string
        priceField.text=$"${item.GetPrice():N2}";//N2 gives me 2 decimal places to this number
        quantityField.text=$"{item.GetQuantityInTransaction()}";
    }
    public void Add()
    {
        currentShop.AddToTransaction(item.GetInventoryItem(),1);
    }
    public void Remove()
    {
        currentShop.AddToTransaction(item.GetInventoryItem(),-1);
     }
}
}
