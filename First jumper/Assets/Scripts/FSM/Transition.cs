using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTransition
{
    public Condition condition;
    public State target;

    public NewTransition(Condition aCondition, State aTarget) {
        condition = aCondition;
        target = aTarget;
    }
}
