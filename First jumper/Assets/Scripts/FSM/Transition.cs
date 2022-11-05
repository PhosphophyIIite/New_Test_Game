using System;

public class Transition
{
    public Func<bool> condition;
    public State target;

    public Func<bool> CheckCondition => condition;
    public State Target => target;

    public Transition(Func<bool> condition, State target) {
        this.condition = condition;
        this.target = target;
    }
}
