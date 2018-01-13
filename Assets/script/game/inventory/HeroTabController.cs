using UnityEngine;
using UnityEngine.UI;

public class HeroTabController : MonoBehaviour {

	public Person person;

    public GameObject heroSkills;
    public GameObject heroSkill;

    public GameObject heroItems;
    public GameObject heroItem;

    public bool isItem = false;
    public bool isSkills = false;

    public void reload() {
        if (isItem) {
            reloadItems();
        }
        if (isSkills) {
            reloadSkills();
        }
    }

    public void reloadItems() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        GameObject hitems = Instantiate(heroItems, transform, false);
        hitems.transform.localPosition = new Vector2(0.0f, 0.8f);
        hitems.GetComponent<HeroItemsTabController>().person = person;
        for (int s = 0; s < PartiesSingleton.inventory.Count; s++) {
            Item item = PartiesSingleton.inventory[s];
            GameObject hitem = Instantiate(heroItem, hitems.transform, false);
            hitem.transform.localPosition = new Vector2(-3.5f + (s * 2.2f), 2.2f);
            hitem.transform.GetComponent<Image>().sprite = item.image;
            hitem.GetComponent<HeroItemController>().item = item;
        }
    }

    public void reloadSkills() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        GameObject hiskills = Instantiate(heroSkills, transform, false);
        hiskills.transform.localPosition = new Vector2(0.0f, 3.0f);
        hiskills.GetComponent<SkillsTabController>().person = person;

        foreach (Ability ab in person.knownAbilities) {
            if (ab.position.x > 0
                && ab.position.y > 0) {
                GameObject hskill = Instantiate(heroSkill, hiskills.transform, false);
                hskill.GetComponent<SkillController>().ability = ab;
                hskill.GetComponent<SkillController>().person = person;
                hskill.transform.localPosition = new Vector2(-1.3f + (ab.position.x - 2) * 2.2f, 2.2f * (ab.position.y - 2));
                hskill.transform.GetComponent<Image>().sprite = ab.image;
                if (ab.requiredLevel > person.level) {
                    hskill.GetComponent<SkillController>().blocked();
                }else {
                    if (ab.isActive) {
                        hskill.GetComponent<SkillController>().enable();
                    } else {
                        hskill.GetComponent<SkillController>().disable();
                    }
                }
            }
        }
    }
}
