using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<Transition> transitions;
    public List<State> states;

    protected PlayerController pc;

    public virtual void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    public virtual void Awake()
    {
        transitions = new List<Transition>();
        //setup transition here
    }

    public virtual void OnEnable()
    {
        //develop state initialization here
    }

    public virtual void OnDisable()
    {
        //develop state finalization here
    }

    public virtual void Update()
    {
        //develop behavior here
    }

    public void LateUpdate()
    {
        foreach (Transition t in transitions)
        {
            if (t.condition.CheckCondition())
            {
                t.target.enabled = true;
                this.enabled = false;
                return;
            }
        }
    }
}