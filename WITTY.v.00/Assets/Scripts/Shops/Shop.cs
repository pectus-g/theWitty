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
    
    public event Action onChange;//check the canges in the shop
    public IEnumerable<ShopItem> GetFilteredItems() 
    {
        yield return new ShopItem(InventoryItem.GetFromID("a24ed9a5-b18f-42ba-bfa1-16cf54dedea3"),10, 10.0f,0);//Unique ID's from resources
        yield return new ShopItem(InventoryItem.GetFromID("f15c2c4e-29fd-4ae7-be92-a2ea19338233"),10, 10.0f,0);
        yield return new ShopItem(InventoryItem.GetFromID("5255cf4c-58a8-48af-99bc-1517296ced41"),10, 10.0f,0);
        yield return new ShopItem(InventoryItem.GetFromID("5255cf4c-58a8-48af-99bc-1517296ced41"),10, 10.0f,0);
    }
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
