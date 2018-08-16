using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSideController : AbstractSideController {

    public override void reload() {
        refresh();

        for (int s = 0; s < getItemList().Count; s++) {
            var currentItem = getItemList()[s];
            var hitem = Instantiate(heroItem, transform.Find("Items").transform, false);
            hitem.transform.localPosition = new Vector2(-2.7f + s % 4 * 1.8f, 1.8f - s / 4 * 1.8f);
            hitem.GetComponent<ShopItemController>().setItem(currentItem);
            if (currentItem.Equals(item)) {
                hitem.GetComponent<ShopItemController>().makeActive();
                if (PartiesSingleton.currentGame.gold >= item.cost) {
                    transform.Find("SellButton").GetComponent<SellButtonController>().makeActive();
                }
            }
        }

        transform.Find("Gold/Text").GetComponent<Text>().text = "Gold : " + getGold();
    }

    public override List<Item> getItemList() {
        return PartiesSingleton.currentShop.items;
    }

    public override float getGold() {
        return PartiesSingleton.currentShop.gold;
    }

    public override void setGold(float gold) {
        PartiesSingleton.currentShop.gold = gold;
    }
}
