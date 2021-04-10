using System;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.instance.AddEventListener("offTheGround",offTheGround);
        EventCenter.instance.AddEventListener("onTheGround",onTheGround);
    }

    public void offTheGround()
    {
        
    }
    
    public void onTheGround()
    {
        
    }
}
