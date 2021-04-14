using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Mirror;
using PlayerState;
using UnityEngine;

namespace Player
{
    public class PlayerMove : NetworkBehaviour
    {
        private int horizontal;
        private bool isFloating = false;
        public float moveSpeed = 5f;
        public PlayerProperty PlayerPropertyInstance;

        // Start is called before the first frame update
        void Start()
        {
            GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
        }

        void InitUpdate()
        {
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyADown", KeyADown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyDDown", KeyDDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keySpaceDown", KeySpaceDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyAUp", KeyAUp);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyDUp", KeyDUp);
            GameMgr.instance.AddUpdateEventListener(MoveUpdata);
            GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
        }
        void KeyADown()
        {
            horizontal = -1;
        }
        
        void KeyDDown()
        {
            horizontal = 1;
        }
        
        void KeySpaceDown()
        {
            DoJump();
        }

        void KeyAUp()
        {
            horizontal = 0;
            if (GetComponent<PlayerFSM>().CurrentState.StateName == "MoveMainState")
                GetComponent<PlayerFSM>().ToIdel();
        }
        
        void KeyDUp()
        {
            horizontal = 0;
            if (GetComponent<PlayerFSM>().CurrentState.StateName == "MoveMainState")
                GetComponent<PlayerFSM>().ToIdel();
        }

        private void MoveUpdata()
        {
            if (horizontal == 0 || !PlayerPropertyInstance.canMove)
                return;
            if (horizontal != PlayerPropertyInstance.faceHorizontal)
            {
                PlayerPropertyInstance.skeletonAnimation.skeleton.ScaleX = horizontal;
                PlayerPropertyInstance.faceHorizontal = horizontal;
                PlayerPropertyInstance.EventCenterInstance.EventTrigger("turnAround",horizontal);
            }

            transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
            if (!isFloating)
            {
                if (GetComponent<PlayerFSM>().CurrentState.StateName == "IdleMainState")
                    GetComponent<PlayerFSM>().ToMove();
            }
        }

        private void DoJump()
        {
            if (isFloating)
                return;
            isFloating = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 8f);
            GetComponent<PlayerFSM>().ToJump();

        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!coll.gameObject.CompareTag(tag: "ground"))
                return;
            PlayerPropertyInstance.EventCenterInstance.EventTrigger("onTheGround");
            ContactPoint2D contact = coll.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point; //这个就是碰撞点
            if (pos.y < transform.position.y)
                isFloating = false;
        }

        void OnCollisionExit2D(Collision2D coll)
        {
            if (!coll.gameObject.CompareTag("ground"))
                return;
            PlayerPropertyInstance.EventCenterInstance.EventTrigger("offTheGround");
            isFloating = true;
        }
    }
}
