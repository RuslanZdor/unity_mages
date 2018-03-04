using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PersonIconController : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        GameObject.Find("Navigation").GetComponent<NavigationController>().openInventory();
    }
}
