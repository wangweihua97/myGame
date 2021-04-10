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
        private int faceHorizontal = 1;

        public float moveSpeed = 5f;

        public float mJumpSpeed = 5f;

        // Start is called before the first frame update
        void Start()
        {
            EventCenter.instance.AddEventListener<KeyCode>("keyDown", CheckInputDown);
            EventCenter.instance.AddEventListener<KeyCode>("keyUp", CheckInputUp);
            GameMgr.instance.AddUpdateEventListener(MoveUpdata);
        }

        private void CheckInputDown(KeyCode arg0)
        {
            switch (arg0)
            {
                case KeyCode.A:
                    horizontal = -1;
                    break;
                case KeyCode.D:
                    horizontal = 1;
                    break;
                case KeyCode.Space:
                    DoJump();
                    break;
                default:
                    break;
            }
        }

        private void CheckInputUp(KeyCode arg0)
        {
            switch (arg0)
            {
                case KeyCode.A:
                    horizontal = 0;
                    GetComponent<PlayerFSM>().ToIdel();
                    break;
                case KeyCode.D:
                    horizontal = 0;
                    GetComponent<PlayerFSM>().ToIdel();
                    break;
                default:
                    break;
            }
        }

        private void MoveUpdata()
        {
            if (horizontal == 0)
                return;
            if (horizontal != faceHorizontal)
            {
                GetComponent<PlayerProperty>().skeletonAnimation.skeleton.ScaleX = horizontal;
                faceHorizontal = horizontal;
            }

            transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
            if (!isFloating)
            {
                if (GetComponent<PlayerFSM>().CurrentState.StateName != "MoveMainState")
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
