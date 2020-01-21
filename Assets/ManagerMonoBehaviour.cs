using System;
using UnityEngine;

public class ManagerMonoBehaviour : MonoBehaviour
{
    //static NuAction actAwake = new NuAction();
    //static NuAction actOnEnable = new NuAction();
    //static NuAction actStart = new NuAction();
    static NuAction actFixedUpdate = new NuAction();
    static NuAction actUpdate = new NuAction();
    static NuAction actLateUpdate = new NuAction();
    static NuAction actOnGUI = new NuAction();
    static NuAction actOnApplicationFocusIn = new NuAction();
    static NuAction actOnApplicationFocusOut = new NuAction();
    static NuAction actOnApplicationQuit = new NuAction();
    static NuAction actOnDisable = new NuAction();
    static NuAction actOnDestroy = new NuAction();

    //public static NuAction ActionAwake { get { return actAwake; } }
    //public static NuAction ActionOnEnable { get { return actOnEnable; } }
    //public static NuAction ActionStart { get { return actStart; } }
    public static NuAction ActionFixedUpdate { get { return actFixedUpdate; } }
    public static NuAction ActionUpdate { get { return actUpdate; } }
    public static NuAction ActionLateUpdate { get { return actLateUpdate; } }
    public static NuAction ActionOnGUI { get { return actOnGUI; } }
    public static NuAction ActionOnApplicationFocusIn { get { return actOnApplicationFocusIn; } }
    public static NuAction ActionOnApplicationFocusOut { get { return actOnApplicationFocusOut; } }
    public static NuAction ActionOnApplicationQuit { get { return actOnApplicationQuit; } }
    public static NuAction ActionOnDisable { get { return actOnDisable; } }
    public static NuAction ActionOnDestroy { get { return actOnDestroy; } }

    //void Awake() {
    //    actAwake.Invoke();
    //}
    //void OnEnable() {
    //    actOnEnable.Invoke();
    //}
    //void Start() {
    //    actStart.Invoke();
    //}
    void FixedUpdate() {
        actFixedUpdate.Invoke();
    }
    void Update() {
        actUpdate.Invoke();
    }
    void LateUpdate() {
        actLateUpdate.Invoke();
    }
    void OnGUI() {
        actOnGUI.Invoke();
    }
    void OnApplicationFocus(bool focus)//also fires upon game/scene/player launch //sometimes not, hm
    {
        if (focus)
            actOnApplicationFocusIn.Invoke();
        else
            actOnApplicationFocusOut.Invoke();
    }
    void OnApplicationQuit() {
        actOnApplicationQuit.Invoke();
    }
    void OnDisable() {
        actOnDisable.Invoke();
    }
    void OnDestroy() {
        actOnDestroy.Invoke();
    }

    /// <summary>
    /// Awake
    /// OnEnable
    /// Start
    /// FixedUpdate
    /// Update
    /// LateUpdate
    /// OnGUI
    /// OnApplicationFocus
    /// OnApplicationQuit
    /// OnDisable
    /// OnDestroy
    /// </summary>
    public static void Help() { }

    public class NuAction
    {
        Action action;

        public NuAction() {
            action = Stub;
        }
        public void ActionAdd(Action a) {
            action += a;
        }
        public void ActionSet(Action a) {
            action = a;
        }
        public void ActionClear() {
            action = null;
        }
        public void Invoke() {
            action.Invoke();
        }
        void Stub() { }
    }
}
