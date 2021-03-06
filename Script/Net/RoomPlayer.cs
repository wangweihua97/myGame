using System;
using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Net
{
    [AddComponentMenu("")]
    public class RoomPlayer : NetworkRoomPlayer
    {
        public static RoomPlayer local;
        private static GameObject anotherPlayer;
        private static GameObject myReadyTip;
        private static GameObject anotherReadyTip;
        private Button readyBtn;
        private static int num = 0;
        public override void OnStartClient()
        {
            // Debug.LogFormat(LogType.Log, "OnStartClient {0}", SceneManager.GetActiveScene().path);
            base.OnStartClient();
        }

        public override void OnClientEnterRoom()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            local = this;
            readyBtn =  readyBtn == null ?GameObject.Find("Button").GetComponent<Button>() : readyBtn;
            readyBtn.onClick.AddListener(Ready);
            anotherPlayer = anotherPlayer == null ? GameObject.Find("playerReady2") : anotherPlayer;
            anotherPlayer.SetActive(false);
            myReadyTip = myReadyTip == null?GameObject.Find("ready1") : myReadyTip;
            myReadyTip.SetActive(false);
            anotherReadyTip = anotherReadyTip == null ?GameObject.Find("ready2") : anotherReadyTip;
            anotherReadyTip.SetActive(false);
        }

        public override void OnClientExitRoom()
        {
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            // Debug.LogFormat(LogType.Log, "ReadyStateChanged {0}", newReadyState);
        }

        public void Ready()
        {
            if(!isLocalPlayer)
                return;
            if(readyToBegin)
                CmdChangeReadyState(false);
            else
                CmdChangeReadyState(true);
        }

        private void Update()
        {
            if(!isClient)
                return;
            if(anotherReadyTip == null)
                return;
            if (isLocalPlayer)
            {
                if (num > 1)
                {
                    anotherPlayer.SetActive(true);
                }
                else
                {
                    anotherPlayer.SetActive(false);
                }
                if (!readyToBegin)
                {
                    myReadyTip.SetActive(false);
                }
                else
                {
                    myReadyTip.SetActive(true);
                }
                num = 1;
            }
            else
            {
                if (!readyToBegin)
                {
                    anotherReadyTip.SetActive(false);
                }
                else
                {
                    anotherReadyTip.SetActive(true);
                }
            }
        }

        private void LateUpdate()
        {
            if(!isLocalPlayer)
                num = 2;
        }

        [Command]
        public void RetrunToRoom()
        {
            NetMgr.instance.ChangeToRoom();
        }
    }
}
