using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroTabController : MonoBehaviour {

	public Person person;

    public GameObject heroSkills;
    public GameObject heroSkill;

    public GameObject heroItems;
    public GameObject heroItem;

    public void reload() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        GameObject hiskills = Instantiate(heroSkills, transform, false);
        hiskills.transform.localPosition = new Vector2(0.0f, 3.0f);
        hiskills.GetComponent<SkillsTabController>().person = person;

        for (int s = 0; s < person.knownAbilities.Count; s++) {
            Ability ab = person.knownAbilities[s];
            GameObject hskill = Instantiate(heroSkill, hiskills.transform, false);
            hskill.GetComponent<SkillController>().ability = ab;
            hskill.transform.localPosition = new Vector2(-3.5f + (s * 2.2f), 0.0f);
            hskill.transform.GetComponent<Image>().sprite = ab.image;

            if (ab.abilityTactic.defaultPriority == 3) {
                hskill.GetComponent<Image>().color = new Color(((float)245 / 256), ((float)48 / 256), ((float)48 / 256), ((float)157 / 256));
            }
        }

        GameObject hitems = Instantiate(heroItems, transform, false);
        hitems.transform.localPosition = new Vector2(0.0f, 0.8f);
        hitems.GetComponent<HeroItemsTabController>().person = person;
        for (int s = 0; s < PartiesSingleton.inventory.Count; s++) {
            Item item = PartiesSingleton.inventory[s];
            GameObject hitem = Instantiate(heroItem, hitems.transform, false);
            hitem.transform.localPosition = new Vector2(-3.5f + (s * 2.2f), 0.0f);
            hitem.transform.GetComponent<Image>().sprite = item.image;
            hitem.GetComponent<HeroItemController>().item = item;
        }
    }
}
