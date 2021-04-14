using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class PlayerAim : NetworkBehaviour
    {
        public GameObject aimGameObject;
        public Transform aimUITransform;
        public float aimSpeed;
        private int vertical = 0;
        private float distance;
        private bool aimUIVisble = true;
        public PlayerProperty PlayerPropertyInstance;

        private void Awake()
        {
            aimSpeed = 20;
        }

        // Start is called before the first frame update
        void Start()
        {
            distance = aimUITransform.transform.localPosition.x;
            GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
        }
        
        void InitUpdate()
        {
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyWDown", KeyWDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keySDown", KeySDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyWUp", KeyWUp);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keySUp", KeySUp);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyEDown", KeyEDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener<int>("turnAround", TurnAround);
            GameMgr.instance.AddUpdateEventListener(AimMoveUpdate);
            GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
        }

        void KeyWDown()
        {
            vertical = 1;
        }

        void KeySDown()
        {
            vertical = -1;
        }

        void KeyWUp()
        {
            vertical = 0;
        }

        void KeySUp()
        {
            vertical = 0;
        }

        void KeyEDown()
        {
            aimUIVisble = !aimUIVisble;
            SetAimUIVisible(aimUIVisble);
        }

        void AimMoveUpdate()
        {
            if (vertical == 0)
                return;
            PlayerPropertyInstance.aimAngle +=
                vertical * PlayerPropertyInstance.faceHorizontal * aimSpeed * Time.deltaTime;
            if (PlayerPropertyInstance.aimAngle > 90)
                PlayerPropertyInstance.aimAngle = 90;
            if (PlayerPropertyInstance.aimAngle < -90)
                PlayerPropertyInstance.aimAngle = -90;
            aimGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, PlayerPropertyInstance.aimAngle));
        }

        void TurnAround(int horizontal)
        {
            aimUITransform.transform.localPosition = new Vector3(horizontal * distance,
                aimUITransform.transform.localPosition.y, aimUITransform.transform.localPosition.z);
            float rotation = horizontal == 1 ? 0 : 180;
            aimUITransform.localEulerAngles =new Vector3(aimUITransform.localEulerAngles.x ,rotation ,aimUITransform.localEulerAngles.z);
            PlayerPropertyInstance.aimAngle = -1 * PlayerPropertyInstance.aimAngle;
            aimGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, PlayerPropertyInstance.aimAngle));
        }

        public void SetAimUIVisible(bool visible)
        {
            int a = visible ? 255 : 0;
            aimUITransform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, a);
        }
    }
}
