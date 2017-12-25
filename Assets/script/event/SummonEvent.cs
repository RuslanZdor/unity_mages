using UnityEngine;
using System.Collections;

public class SummonEvent : BasicTargetEvent {

	public Person person;

	public override float eventStart() {
        PersonFactory pf = owner.personController.transform.parent.parent.transform.Find("GameFactory").GetComponent<PersonFactory>();
        GameObject go = pf.create(person);

        PartiesSingleton.getParty(owner.ally).addPerson(go);
        go.GetComponent<PersonController>().person.summoner = owner;
        go.GetComponent<PersonController>().person.generateEvents(eventTime);

        owner.statistics.summonCount++;
        logEvent("summon  " + person.name);
        return 0.0f; ;
    }
}
