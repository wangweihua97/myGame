using System;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.instance.AddEventListener("offTheGround",offTheGround);
        EventCenter.instance.AddEventListener("onTheGround",onTheGround);
        EventCenter.instance.AddEventListener<int>("turnAround",turnAround);
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
