using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HeroImageController : MonoBehaviour, IPointerClickHandler {

	public Person person;

    public void OnPointerClick(PointerEventData eventData) {
        transform.parent.parent.Find("HeroTab").transform.GetComponent<HeroTabController>().person = person;
        transform.parent.parent.Find("HeroTab").transform.GetComponent<HeroTabController>().reload();
    }
}
