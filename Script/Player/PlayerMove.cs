using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using PlayerState;
using UnityEditor.UIElements;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        private int horizontal;
        private bool isFloating = false;
        public float moveSpeed = 5f;
        public float mJumpSpeed = 5f;

        // Start is called before the first frame update
        void Start()
        {
            EventCenter.instance.AddEventListener("keyADown", KeyADown);
            EventCenter.instance.AddEventListener("keyDDown", KeyDDown);
            EventCenter.instance.AddEventListener("keySpaceDown", KeySpaceDown);
            EventCenter.instance.AddEventListener("keyAUp", KeyAUp);
            EventCenter.instance.AddEventListener("keyDUp", KeyDUp);
            GameMgr.instance.AddUpdateEventListener(MoveUpdata);
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
            if (horizontal == 0 || !PlayerProperty.instance.canMove)
                return;
            if (horizontal != GetComponent<PlayerProperty>().faceHorizontal)
            {
                GetComponent<PlayerProperty>().skeletonAnimation.skeleton.ScaleX = horizontal;
                GetComponent<PlayerProperty>().faceHorizontal = horizontal;
                EventCenter.instance.EventTrigger("turnAround",horizontal);
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
            EventCenter.instance.EventTrigger("onTheGround");
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
            EventCenter.instance.EventTrigger("offTheGround");
            isFloating = true;
        }
    }
}
