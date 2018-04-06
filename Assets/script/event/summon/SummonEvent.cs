using script;
using UnityEngine;

public class SummonEvent : BasicTargetEvent {

	public Person person;

	public override float eventStart() {
        var pf = GameObject.Find("GameFactory").GetComponent<PersonFactory>();
        var go = pf.create(person);

        PartiesSingleton.getParty(owner.ally).addPerson(go);
        go.GetComponent<PersonController>().person.summoner = owner;

        go.GetComponent<PersonController>().person.setLevel(owner.level);
        go.GetComponent<PersonController>().person.initAbilities();
        go.GetComponent<PersonController>().person.generateEvents(eventTime);

        logEvent("summon  " + person.name);
        return 0.0f; ;
    }
}
