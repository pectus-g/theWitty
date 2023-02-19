using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using System;
using RPG.Control;

namespace RPG.Shops
{
public class Shop : MonoBehaviour, IRaycastable
{
    [SerializeField] string shopName;
    public class ShopItem
    {
        InventoryItem item;
        int availability;
        float price;
        int quantityInTransaction;
    }
    public event Action onChange;//check the canges in the shop
    public IEnumerable<ShopItem> GetFilteredItems() {return null;}
    public void SelectFilter(ItemCategory category) {}
    public ItemCategory GetFilter(){return ItemCategory.None;}
    public void SelectMode(bool isBuying) {}
    public bool isBuyingMode() {return true;}
    public bool CanTransact() {return true;}
    public void ConfirmTransaction() {}
    public float TransactionTotal() {return 0;}
    public string GetShopName()
    {
        return shopName;
    }
    public void AddToTransaction(InventoryItem item, int quantity) {}

    public CursorType GetCursorType(){return CursorType.Shop;}

    public bool HandleRaycast(PlayerController callingController)
    {
        if(Input.GetMouseButtonDown(0))
        {
            callingController.GetComponent<Shopper>().SetActiveShop(this);
        }
        return true;
    }
}
}
