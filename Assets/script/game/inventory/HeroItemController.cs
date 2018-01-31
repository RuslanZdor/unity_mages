using UnityEngine;
using UnityEngine.EventSystems;

public class HeroItemController : PersonBehavior, IPointerClickHandler {

	public Item item;

    public void OnPointerClick(PointerEventData eventData) {
        Person currentPerson = transform.root.Find("Inventory").transform.GetComponent<HeroItemsController>().person;
        if (person != null) {
            putItemtoInventry(item, currentPerson);
        } else {
            foreach (Item pItem in currentPerson.itemList.FindAll((Item i )=> i.type == item.type)) {
                putItemtoInventry(pItem, currentPerson);
            }
            putItemToPerson(item, currentPerson);
        }

        transform.root.Find("HeroList").transform.GetComponent<HeroListController>().reload();
        transform.root.Find("HeroTab").transform.GetComponent<HeroTabController>().reload();
        transform.root.Find("Inventory").transform.GetComponent<HeroItemsController>().reload();
    }

    private void putItemToPerson(Item item, Person person) {
        PartiesSingleton.inventory.Remove(item);
        person.itemList.Add(item);
    }
    private void putItemtoInventry(Item item, Person person) {
        person.itemList.Remove(item);
        PartiesSingleton.inventory.Add(item);
    }
}
