using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<NewTransition> transitions;

    protected PlayerController playerController;

    public virtual void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public virtual void Awake()
    {
        transitions = new List<NewTransition>();
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
        foreach (NewTransition t in transitions)
        {
            if (t.condition.Test())
            {
                t.target.enabled = true;
                this.enabled = false;
                return;
            }
        }
    }
}