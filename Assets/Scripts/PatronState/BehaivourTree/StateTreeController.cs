using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaivourTree
{
    public class StateTreeController : MonoBehaviour
    {
        public Node currentState;
        public GameObject target = null;
        protected Node statetoPlay = null;
        private Dictionary<string, object> _dataContext = new();
        public void StateTransition()
        {
            if (currentState.children.Count != 0)
            {
                bool cond = false;
                int count = 0;
                while(!cond&&count!=currentState.children.Count)
                {
                    cond = CheckCondition(currentState.children[count++]);
                }
                if(cond)
                {
                    if (statetoPlay != null && statetoPlay.action != null && statetoPlay != currentState.children[count - 1])
                    {
                        statetoPlay.action.OnFinishedState();
                    }
                    statetoPlay = currentState.children[count - 1];
                    currentState = statetoPlay;
                    if (statetoPlay.action != null) statetoPlay.action.OnSetState(this);
                }
            }
            if (!CheckCondition(currentState)&&currentState.parent!=null)
            {
                if (statetoPlay != null && statetoPlay.action != null && statetoPlay != currentState.parent)
                {
                    statetoPlay.action.OnFinishedState();
                }
                statetoPlay = currentState.parent;
                currentState = statetoPlay;
                if (statetoPlay.action != null) statetoPlay.action.OnSetState(this);
            }
        }

        public bool CheckCondition(Node node)
        {
            switch (node.type)
            {
                case TypeOfCondition.SIMPLE:
                    return node.Condition(this);
                case TypeOfCondition.AND:
                    return node.AndCondition(node.parent.cond,this);
                case TypeOfCondition.OR:
                    return node.OrCondition(node.parent.cond,this);
                default:
                    return false;
            }
        }

        // Update is called once per frame
       protected virtual void Update()
        {
            StateTransition();
            if(statetoPlay.action!=null)
            {
                statetoPlay.action.OnUpdate();
            }
        }
        public object GetData(string key)
        {
           if (_dataContext.TryGetValue(key, out object value))
                return value;
            return null;
        }
        public void SetData(string key, object value)
        {
            if (_dataContext.ContainsKey(key))
            _dataContext[key] = value;
            else
                _dataContext.Add(key, value);
        }
    }
    public enum TypeOfCondition
    {
        SIMPLE,
        AND,
        OR
    }
}
