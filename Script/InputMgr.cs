using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class InputMgr : NetworkBehaviour
{
    private bool isStart = true;
    void Start()
    {
        GameMgr.instance.AddFristUpdateEventListener(MyUpdate);
    }
    /// <summary>
    /// 是否开启或关闭 我的输入检测
    /// </summary>
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }
    private void CheckKeyCode(KeyCode key)
    {
        //事件中心模块 分发按下抬起事件
        if (Input.GetKeyDown(key))
        {
            CmdKeyDown(PlayerProperty.localPlayer, key);
        }
        //事件中心模块 分发按下抬起事件
        if (Input.GetKeyUp(key))
        {
            CmdKeyUp(PlayerProperty.localPlayer, key);
        }
    }
    private void MyUpdate()
    {
        if (!isStart)
            return;
        if(!isLocalPlayer)
            return;
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.Space);
        CheckKeyCode(KeyCode.J);
        CheckKeyCode(KeyCode.E);
        CheckKeyCode(KeyCode.Q);
    }

    [Command]
    void CmdKeyDown(PlayerProperty playerProperty,KeyCode key)
    {
        playerProperty.EventCenterInstance.EventTrigger("keyDown", key);
        playerProperty.EventCenterInstance.EventTrigger("key" + key + "Down");
        RpcKeyDown(key);
    }
    [ClientRpc]
    private void RpcKeyDown(KeyCode key)
    {
        Debug.Log("Down"+netIdentity.netId);
        GetComponent<EventCenter>().EventTrigger("keyDown", key);
        GetComponent<EventCenter>().EventTrigger("key" + key + "Down");
    }
    [Command]
    void CmdKeyUp(PlayerProperty playerProperty,KeyCode key)
    {
        playerProperty.EventCenterInstance.EventTrigger("keyUp", key);
        playerProperty.EventCenterInstance.EventTrigger("key" + key + "Up");
        RpcKeyUp(key);
    }
    [ClientRpc]
    private void RpcKeyUp(KeyCode key)
    {
        Debug.Log("Up"+netIdentity.netId);
        GetComponent<EventCenter>().EventTrigger("keyUp", key);
        GetComponent<EventCenter>().EventTrigger("key" + key + "Up");
    }
}
