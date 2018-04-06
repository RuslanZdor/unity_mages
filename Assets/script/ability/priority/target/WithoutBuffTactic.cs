using System.Collections.Generic;
using script;

public class WithoutBuffTactic : AbstractTargetTactic {

    public Buff buff;

    public WithoutBuffTactic(Buff buff) {
        this.buff = buff;
    }

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        var list = party.getLivePersons();
		list.RemoveAll(person => person.hasEffect(buff));

		if (count >= list.Count) {
            return list;
        }
	    shuffle(list);
	    return list.GetRange(0, count);
	}
}
