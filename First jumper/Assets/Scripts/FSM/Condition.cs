using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    protected PlayerController playerController;

    public Condition(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public virtual bool Test()
    {
        return false;
    }
}
