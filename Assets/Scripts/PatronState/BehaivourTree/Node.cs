using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaivourTree
{
    //public enum NodeState
    //{
    //    RUNNING,
    //    SUCCESS,
    //    FAILURE
    //}

    [CreateAssetMenu(fileName = "ScriptableNode", menuName = "ScriptableNodes/ScriptableNode", order = 3)]
    public class Node : ScriptableObject
    {
       // public NodeState state;
        public TypeOfCondition type;
        public Node parent;
        public List<Node> children;
        public ScriptableActionTree action;
        public ScriptableCondition cond;
     //   private Dictionary<string, object> _dataContext = new();

        //public Node() 
        //{
        //    parent = null;
        //}

        //public Node(List<Node> children)
        //{
        //    foreach (Node child in children)
        //    {
        //        Attach(child);
        //    }
        //}

        //private void Attach(Node node)
        //{
        //    node.parent = this;
        //    children.Add(node);
        //}

       // public virtual NodeState Evaluate() => NodeState.FAILURE;

        //public void SetData(string key, object value)
        //{
        //    _dataContext[key] = value;
        //}

        //public object GetData(string key)
        //{
        //    if (_dataContext.TryGetValue(key, out object value))
        //        return value;
        //    Node node = parent;
        //    while(node != null)
        //    {
        //        value = node.GetData(key);
        //        if(value!=null)
        //            return value;
        //        node = node.parent;
        //    }
        //    return null;
        //}

        //public bool ClearData(string key)
        //{
        //    if (_dataContext.ContainsKey(key))
        //        return true;
        //    Node node = parent;
        //    while (node != null)
        //    {
        //        bool clear = node.ClearData(key);
        //        if (clear)
        //            return true;
        //        node = node.parent;
        //    }
        //    return false;
        //}

        public bool Condition(StateTreeController sc)
        {
            return cond.Check(sc);
        }

        public bool AndCondition(ScriptableCondition cond, StateTreeController sc)
        {
            return cond.Check(sc) && Condition(sc);
        }

        public bool OrCondition(ScriptableCondition cond, StateTreeController sc)
        {
            return cond.Check(sc) || Condition(sc);
        }
    }
}
