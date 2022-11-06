using System;

public class Transition
{
    public Func<bool> condition;
    public MovementState movementTarget;
    public ItemState itemTarget;
    public string message;

    public Func<bool> CheckCondition => condition;
    public MovementState MovementTarget => movementTarget;
    public ItemState ItemTarget => itemTarget;
    public string Message => message;

    public Transition(Func<bool> condition, MovementState movementTarget, string message) {
        this.condition = condition;
        this.movementTarget = movementTarget;
        this.message = message;
    }

    public Transition(Func<bool> condition, ItemState itemTarget, string message) {
        this.condition = condition;
        this.itemTarget = itemTarget;
        this.message = message;
    }
}