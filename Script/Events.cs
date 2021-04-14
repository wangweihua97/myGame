using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Events : NetworkBehaviour
{
    private void Start()
    {
        GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
    }

    void InitUpdate()
    {
        EventCenter instance = isLocalPlayer ? EventCenter.localPlayer : EventCenter.anotherPlayer;
        instance.AddEventListener("offTheGround",offTheGround);
        instance.AddEventListener("onTheGround",onTheGround);
        instance.AddEventListener<int>("turnAround",turnAround);
        GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
    }

    public void offTheGround()
    {
        
    }
    
    public void onTheGround()
    {
        
    }

    public void turnAround(int horizontal)
    {
        
    }
}
