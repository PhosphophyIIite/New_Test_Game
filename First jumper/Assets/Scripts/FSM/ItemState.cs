using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    public List<Transition> itemTransitions;
    protected PlayerController pc;

    public virtual void Awake()
    {
        pc = GetComponent<PlayerController>();
        
        itemTransitions = new List<Transition>();
    }

    public virtual void Start()
    {
        // Develop state start here
    }

    public virtual void OnEnable()
    {
        // Develop state initialization here
    }

    public virtual void Update()
    {
        // Develop behavior here
    }

    public virtual void FixedUpdate()
    {
        // Develop behavior here
    }

    public virtual void OnDisable()
    {
        // Develop state finalization here
    }

    public void LateUpdate()
    {
        foreach (Transition transition in itemTransitions)
        {
            if (transition.CheckCondition())
            {
                Debug.Log(transition.Message);
                transition.ItemTarget.enabled = true;
                enabled = false;
                return;
            }
        }
    }
}