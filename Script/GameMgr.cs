using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    private class FristUpdateEvent:UnityEvent {}
    private class UpdateEvent:UnityEvent {}
    private class LateUpdateEvent:UnityEvent {}

    private FristUpdateEvent fristUpdateEvent = new FristUpdateEvent();
    private UpdateEvent updateEvent = new UpdateEvent();
    private LateUpdateEvent lateUpdateEvent = new LateUpdateEvent();

    public static GameMgr instance;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Screen.fullScreen = false;
    }

    public void AddFristUpdateEventListener(UnityAction unityAction)
    {
        fristUpdateEvent.AddListener(unityAction);
    }
    public void RemoveFristUpdateEventListener(UnityAction unityAction)
    {
        fristUpdateEvent.RemoveListener(unityAction);
    }
    public void AddUpdateEventListener(UnityAction unityAction)
    {
        updateEvent.AddListener(unityAction);
    }
    public void RemoveUpdateEventListener(UnityAction unityAction)
    {
        updateEvent.RemoveListener(unityAction);
    }
    public void AddLateUpdateEventListener(UnityAction unityAction)
    {
        lateUpdateEvent.AddListener(unityAction);
    }
    public void RemoveLateUpdateEventListener(UnityAction unityAction)
    {
        lateUpdateEvent.RemoveListener(unityAction);
    }


    // Update is called once per frame
    void Update()
    {
        fristUpdateEvent.Invoke();
        updateEvent.Invoke();
        lateUpdateEvent.Invoke();
    }
}
