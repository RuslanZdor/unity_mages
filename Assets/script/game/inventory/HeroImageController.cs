using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HeroImageController : MonoBehaviour, IPointerClickHandler {

	public Person person;

    public void OnPointerClick(PointerEventData eventData) {
        transform.root.Find("HeroList").transform.GetComponent<HeroListController>().person = person;
        transform.root.Find("HeroList").transform.GetComponent<HeroListController>().reload();

        transform.root.Find("HeroTab").transform.GetComponent<HeroTabController>().person = person;
        transform.root.Find("HeroTab").transform.GetComponent<HeroTabController>().reload();

        transform.root.Find("Inventory").transform.GetComponent<HeroItemsController>().person = person;
        transform.root.Find("Inventory").transform.GetComponent<HeroItemsController>().reload();
    }
}
