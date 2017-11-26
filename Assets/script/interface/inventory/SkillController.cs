using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillController : MonoBehaviour, IPointerClickHandler {

	public Ability ability; 
    
    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
        transform.parent.GetComponent<SkillsTabController>().OnPointerClick(eventData);
        transform.GetComponent<Image>().color = new Color(((float)245 / 256), ((float)48 / 256), ((float)48 / 256), ((float)157 / 256));
        ability.abilityTactic.defaultPriority = 3;
    }
}
