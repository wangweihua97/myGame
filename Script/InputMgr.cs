using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
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
            EventCenter.instance.EventTrigger("keyDown", key);
            EventCenter.instance.EventTrigger("key" + key + "Down");
        }
        //事件中心模块 分发按下抬起事件
        if (Input.GetKeyUp(key))
        {
            EventCenter.instance.EventTrigger("keyUp", key);
            EventCenter.instance.EventTrigger("key" + key + "Up");
        }
    }
    private void MyUpdate()
    {
        if (!isStart)
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
}
