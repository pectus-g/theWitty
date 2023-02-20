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

     [SerializeField]
     StockItemConfig[] stockConfig;

    [System.Serializable]//to see and configure in the inspector
    class StockItemConfig
    {
        public InventoryItem item;
        public int initialStock;

        [Range(0,100)]//discount percentage value min and max
        public float buyingDiscountPercentage;
    }
    Dictionary<InventoryItem,int> transaction = new Dictionary<InventoryItem, int>();
    public event Action onChange;//check the canges in the shop
    public IEnumerable<ShopItem> GetFilteredItems() 
    {
       foreach (StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage/100);
                int quantityInTransaction=0;
                transaction.TryGetValue(config.item,out quantityInTransaction);
                yield return new ShopItem(config.item, config.initialStock, price, quantityInTransaction);
            }
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
    public void AddToTransaction(InventoryItem item, int quantity) 
    {
        if(!transaction.ContainsKey(item))
        {
            transaction[item]=0;

        }

        transaction[item]+=quantity;

        if(transaction[item]<=0)
        {
            transaction.Remove(item);
        }

        if(onChange!=null)
        {
            onChange();
        }
    }

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
