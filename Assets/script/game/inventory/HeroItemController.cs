using UnityEngine;
using UnityEngine.EventSystems;

public class HeroItemController : MonoBehaviour, IPointerClickHandler {

	public Item item;
    public Person person;

    public void OnPointerClick(PointerEventData eventData) {
        if (person != null) {
            person.itemList.Remove(item);
            PartiesSingleton.inventory.Add(item);
        }else {
            PartiesSingleton.inventory.Remove(item);
            transform.root.Find("Inventory").transform.GetComponent<HeroItemsController>().person.itemList.Add(item);
        }


        transform.root.Find("HeroList").transform.GetComponent<HeroListController>().reload();
        transform.root.Find("HeroTab").transform.GetComponent<HeroTabController>().reload();
        transform.root.Find("Inventory").transform.GetComponent<HeroItemsController>().reload();

    }
}
