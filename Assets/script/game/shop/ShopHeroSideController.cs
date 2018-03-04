using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHeroSideController : AbstractSideController {

    public override void reload() {
        refresh();

        for (int s = 0; s < getItemList().Count; s++) {
            Item currentItem = getItemList()[s];
            GameObject hitem = Instantiate(heroItem, transform.Find("Items").transform, false);
            hitem.transform.localPosition = new Vector2(-3.3f + ((s % 4) * 2.2f), 2.2f - ((s / 4) * 2.2f));
            hitem.GetComponent<ShopItemController>().setItem(currentItem);
            if (currentItem.Equals(item)) {
                hitem.GetComponent<ShopItemController>().makeActive();
                if (PartiesSingleton.currentShop.gold >= item.cost) {
                    transform.Find("SellButton").GetComponent<SellButtonController>().makeActive();
                }
            }
        }

        transform.Find("Gold").GetComponent<Text>().text = "Gold : " + getGold();
    }

    public override List<Item> getItemList() {
        return PartiesSingleton.inventory;
    }

    public override float getGold() {
        return PartiesSingleton.gold;
    }

    public override void setGold(float gold) {
        PartiesSingleton.gold = gold;
    }
}
