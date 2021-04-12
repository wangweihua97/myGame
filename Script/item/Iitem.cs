using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Iitem: MonoBehaviour
    {
        public Sprite img;
        public float liftTime = 2;
        private float startTIme = 0;
        public float xSpeed;
        public float ySpped;
        public float GravitySpped;
        public bool dead = false;
        public string name;
        public float initZAngle = 0;
        public Transform playerTransform;

        public int explosionRadius;

        public void liftUpdate()
        {
            if(dead)
                return;
            startTIme += Time.deltaTime;
            if(startTIme >= liftTime)
            {
                LifeEnd();
                dead = true;
                startTIme = 0;
                GameObjectPool.instance.RemoveGameObject(name ,gameObject);
            }
        }
        
        public void MoveUpdate()
        {
            if(dead)
                return;
            if(xSpeed == 0 && ySpped == 0)
                return;
            ySpped -= GravitySpped * Time.deltaTime;
            transform.position += new Vector3(xSpeed ,ySpped ,0) * Time.deltaTime;
            if(xSpeed != 0)
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan(ySpped/xSpeed)*Mathf.Rad2Deg+initZAngle);
        }
        
        //初始化
        void Start()
        {
            GameMgr.instance.AddUpdateEventListener(MoveUpdate);
            GameMgr.instance.AddLateUpdateEventListener(liftUpdate);
            Init();
        }

        //射击
        public virtual void Shoot(Transform transform,int faceHorizontal,float angle, float power)
        {
            playerTransform = transform;
        }
        //初始化
        public virtual void Init()
        {
        }
        
        //碰撞时调用
        public virtual bool Collision(Collision2D coll)
        {
            return true;
        }
        
        //生命周期结束时调用
        public virtual void LifeEnd()
        {
        }
        
        void OnCollisionEnter2D (Collision2D coll)
        {
            if (dead) {
                return;
            }
            if(!Collision(coll))
                return;
            ContactPoint2D contact = coll.contacts[0];
            Vector2 pos = contact.point;
            if (coll.gameObject.CompareTag(tag: "ground"))
                OnRemove(pos);
        }
        
        public void OnRemove(Vector2 pos)
        {
            dead = true;
            startTIme = 0;
            GameObjectPool.instance.RemoveGameObject(name ,gameObject);
            if (explosionRadius <= 0)
                return;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, (float)explosionRadius/20);
            for (int i = 0; i < colliders.Length; i++)
            {
                // TODO: two calls for getcomponent is bad
                if(colliders[i].GetComponent<DestructibleSprite>())
                    colliders[i].GetComponent<DestructibleSprite>().ApplyDamage(pos, explosionRadius);
            }
        }
    }
}
