using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractTargetTactic {
    public abstract List<Person> getTargets(Party party, int count);
}
