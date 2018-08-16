using script;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroItemController : PersonBehavior, IPointerClickHandler {

	public Item item;

    public void OnPointerClick(PointerEventData eventData) {
        var currentPerson = transform.root.Find(InventoryController.INVENTORY_OBJECT).transform.GetComponent<HeroItemsController>().person;
        if (person != null) {
            putItemtoInventry(item, currentPerson);
        } else {
/*            foreach (var pItem in currentPerson.itemList.FindAll(i=> i.type == item.type)) {
                putItemtoInventry(pItem, currentPerson);
            }
*/            putItemToPerson(item, currentPerson);
        }

        transform.root.Find(InventoryController.INVENTORY_HERO_LIST).transform.GetComponent<HeroListController>().reload();
        transform.root.Find(InventoryController.INVENTORY_HERO_TAB).transform.GetComponent<HeroTabController>().reload();
        transform.root.Find(InventoryController.INVENTORY_OBJECT).transform.GetComponent<HeroItemsController>().reload();
    }

    public void init(Person person, Item item) {
        transform.GetComponent<Image>().sprite = item.image;
        this.item = item;
        this.person = person;        
    }

    private void putItemToPerson(Item item, Person person) {
        PartiesSingleton.currentGame.inventory.Remove(item);
//        person.itemList.Add(item);
    }
    private void putItemtoInventry(Item item, Person person) {
//        person.itemList.Remove(item);
        PartiesSingleton.currentGame.inventory.Add(item);
    }
}
