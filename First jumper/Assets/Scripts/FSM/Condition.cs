using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    protected PlayerController pc;

    public Condition(PlayerController pc)
    {
        this.pc = pc;
    }

    public virtual bool CheckCondition()
    {
        return false;
    }
}
