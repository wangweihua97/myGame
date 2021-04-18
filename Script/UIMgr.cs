
using System;
using Item;
using Net;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public static UIMgr instance;
    public GameObject healthyUI;
    public PlayerProperty PlayerPropertyInstance;
    public PlayerShoot playerShoot;
    public GameObject resultText;
    public Image pistolCD;
    public Image grenadeCD;
    public Image missileCD;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        resultText.SetActive(false);
        GameMgr.instance.AddLateUpdateEventListener(CDUpdate);
    }

    private void CDUpdate()
    {
        if(!PlayerPropertyInstance)
            return;
        if (!playerShoot)
            playerShoot = PlayerPropertyInstance.GetComponent<PlayerShoot>();
        pistolCD.fillAmount = playerShoot.CurCD[1] / playerShoot.MaxCD[1];
        grenadeCD.fillAmount = playerShoot.CurCD[2] / playerShoot.MaxCD[2];
        missileCD.fillAmount = playerShoot.CurCD[0] / playerShoot.MaxCD[0];
    }

    public GameObject CreatHealthyUI()
    {
        GameObject go = Instantiate(healthyUI);
        go.transform.SetParent(transform);
        return go;
    }

    public void UpClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.W);
    }
    public void UpClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.W);
    }
    public void DownClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.S);
    }
    public void DownClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.S);
    }
    public void LeftClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.A);
    }
    public void LeftClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.A);
    }
    public void RightClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.D);
    }
    public void RightClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.D);
    }
    public void ShootClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.J);
    }
    public void ShootClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.J);
    }
    public void AimClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.E);
    }
    public void AimClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.E);
    }
    public void JumpClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.Space);
    }
    public void JumpClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.Space);
    }
    public void SwitchClickDown()
    {
        PlayerPropertyInstance._inputMgr.ClickDown(KeyCode.Q);
    }
    public void SwitchClickUp()
    {
        PlayerPropertyInstance._inputMgr.ClickUp(KeyCode.Q);
    }
    
    public void ReturnToRoom()
    {
        RoomPlayer.local.RetrunToRoom();
    }
    
    public void ShowResult(string text)
    {
        resultText.SetActive(true);
        resultText.GetComponent<Text>().text = text;
    }


}