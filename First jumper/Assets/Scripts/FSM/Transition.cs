using System;

public class Transition
{
    public Func<bool> condition;
    public State target;
    public string message;

    public Func<bool> CheckCondition => condition;
    public State Target => target;
    public string Message => message;

    public Transition(Func<bool> condition, State target, string message) {
        this.condition = condition;
        this.target = target;
        this.message = message;
    }
}
