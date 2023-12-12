using System;
using System.Collections.Generic;
using UnityEngine;

public class MBManager : MonoBehaviour
{
    static ActionList actFixedUpdate = new ActionList();
    static ActionList actUpdate = new ActionList();
    static ActionList actLateUpdate = new ActionList();
    static ActionList actOnGUI = new ActionList();
    static ActionList actOnApplicationFocusIn = new ActionList();
    static ActionList actOnApplicationFocusOut = new ActionList();
    static ActionList actOnApplicationQuit = new ActionList();
    static ActionList actOnDisable = new ActionList();
    static ActionList actOnDestroy = new ActionList();

    public static ActionList ActionFixedUpdate { get { return actFixedUpdate; } }
    public static ActionList ActionUpdate { get { return actUpdate; } }
    public static ActionList ActionLateUpdate { get { return actLateUpdate; } }
    public static ActionList ActionOnGUI { get { return actOnGUI; } }
    public static ActionList ActionOnApplicationFocusIn { get { return actOnApplicationFocusIn; } }
    public static ActionList ActionOnApplicationFocusOut { get { return actOnApplicationFocusOut; } }
    public static ActionList ActionOnApplicationQuit { get { return actOnApplicationQuit; } }
    public static ActionList ActionOnDisable { get { return actOnDisable; } }
    public static ActionList ActionOnDestroy { get { return actOnDestroy; } }

    void FixedUpdate()
    {
        actFixedUpdate.ActTheQueue();
    }

    void Update()
    {
        actUpdate.ActTheQueue();
    }

    void LateUpdate()
    {
        actLateUpdate.ActTheQueue();
    }

    void OnGUI()
    {
        actOnGUI.ActTheQueue();
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
            actOnApplicationFocusIn.ActTheQueue();
        else
            actOnApplicationFocusOut.ActTheQueue();
    }

    void OnApplicationQuit()
    {
        actOnApplicationQuit.ActTheQueue();
    }

    void OnDisable()
    {
        actOnDisable.ActTheQueue();
    }

    void OnDestroy()
    {
        actOnDestroy.ActTheQueue();
    }

    public class ActionNu
    {
        Action action;

        public ActionNu()
        {
            action = null;
        }

        public ActionNu(Action a)
        {
            action = a;
        }

        public void ActionSet(Action a)
        {
            action = a;
        }

        public void ActionClear()
        {
            action = null;
        }

        public bool Invoke()
        {
            if (action != null)
            {
                action.Invoke();
                return true;
            }
            else
                return false;
        }

        public Delegate[] GetInvocationList()
        {
            return action.GetInvocationList();
        }

        //void PlaceholderStub() { }
    }

    public class ActionList
    {
        List<ActionNu> list;

        public ActionList()
        {
            list = new List<ActionNu>();
        }

        public bool Add(ActionNu a)
        {
            if (!list.Exists(x => x == a))
            {
                list.Add(a);
                return true;
            }
            else
                return false;
        }

        //public bool Remove(ActionNu a)
        //{
        //    return list.Remove(a);
        //}

        public bool Remove(ActionNu a)
        {
            return list.Exists(x => x == a) ? list.Remove(a) : false;
        }

        public void ActTheQueue()
        {
            list.ForEach(a => a.Invoke());
        }

        public IEnumerator<ActionNu> GetEnumerator()
        {
            foreach (ActionNu action in list)
                yield return action;
        }

        public string[] PrintInfo()
        {
            string[] infoLines = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
                infoLines[i] = list[i].GetInvocationList()[0].Method.DeclaringType + " : " + list[i].GetInvocationList()[0].Method;

            return infoLines;
        }
    }
}
