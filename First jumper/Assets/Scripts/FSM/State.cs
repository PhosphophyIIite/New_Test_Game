using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<Transition> transitions;

    protected PlayerController pc;

    public virtual void Awake()
    {
        pc = GetComponent<PlayerController>();
        
        transitions = new List<Transition>();
    }

    public virtual void Start()
    {
        //developer state start here
    }

    public virtual void OnEnable()
    {
        //develop state initialization here
    }


    public virtual void Update()
    {
        //develop behavior here
    }

    public virtual void FixedUpdate()
    {
        //develop behavior here
    }

    public virtual void OnDisable()
    {
        //develop state finalization here
    }

    public void LateUpdate()
    {
        foreach (Transition transition in transitions)
        {
            if (transition.CheckCondition())
            {
                Debug.Log(transition.Message);
                transition.Target.enabled = true;
                enabled = false;
                return;
            }
        }
    }
}