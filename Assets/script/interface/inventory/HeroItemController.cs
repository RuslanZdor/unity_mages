using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroItemController : MonoBehaviour, IPointerClickHandler {

	public Item item; 
    
    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
        transform.parent.GetComponent<HeroItemsTabController>().OnPointerClick(eventData);
        transform.GetComponent<Image>().color = new Color(((float)245 / 256), ((float)48 / 256), ((float)48 / 256), ((float)157 / 256));
        item.isActive = true;
    }
}
