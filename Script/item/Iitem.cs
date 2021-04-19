using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Player;
using UnityEngine;

namespace Item
{
    public class Iitem: NetworkBehaviour
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
        public int minHurt = 1;
        public int MaxHurt = 50;
            

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
                OnRemove(pos ,null);
            else if(coll.gameObject.CompareTag(tag: "Player")&&coll.transform != playerTransform)
                OnRemove(pos ,coll.gameObject.GetComponent<PlayerProperty>());
        }
        
        public void OnRemove(Vector2 pos, PlayerProperty playerProperty)
        {
            dead = true;
            startTIme = 0;
            GameObjectPool.instance.RemoveGameObject(name ,gameObject);
            if(!isServer)
                return;
            if (explosionRadius <= 0 && playerProperty!= null)
            {
                playerProperty.playerDamage.Hurt(MaxHurt);
                RpcHurt(playerProperty, MaxHurt);
                return;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, (float)explosionRadius/20);
            for (int i = 0; i < colliders.Length; i++)
            {
                // TODO: two calls for getcomponent is bad
                if (colliders[i].GetComponent<DestructibleSprite>())
                {
                    colliders[i].GetComponent<DestructibleSprite>().ApplyDamage(pos, explosionRadius);
                    RpcDemage(pos, explosionRadius);
                }
                if (colliders[i].GetComponent<PlayerDamage>())
                {
                    colliders[i].GetComponent<PlayerDamage>().Damage(pos, (float)explosionRadius/20);
                    float distance = Mathf.Sqrt(Vector2.SqrMagnitude((Vector2)colliders[i].transform.position - pos));
                    int hurt = minHurt + (int)(20 * (MaxHurt - minHurt) / distance / explosionRadius) ;
                    colliders[i].GetComponent<PlayerDamage>().Hurt(MaxHurt);
                    RpcHurt(colliders[i].GetComponent<PlayerProperty>(), MaxHurt);
                }
                    
            }
        }
        [ClientRpc]
        private void RpcDemage(Vector2 pos,int  explosionRadius)
        {
            dead = true;
            startTIme = 0;
            ItemMgr.instance.CreatBoom(pos, explosionRadius / 20);
            GameObjectPool.instance.RemoveGameObject(name ,gameObject);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, (float)explosionRadius/20);
            for (int i = 0; i < colliders.Length; i++)
            {
                // TODO: two calls for getcomponent is bad
                if(colliders[i].GetComponent<DestructibleSprite>())
                    colliders[i].GetComponent<DestructibleSprite>().ApplyDamage(pos, explosionRadius);
                if (colliders[i].GetComponent<PlayerDamage>())
                {
                    colliders[i].GetComponent<PlayerDamage>().Damage(pos, (float) explosionRadius / 20);
                }
            }
        }
        [ClientRpc]
        private void RpcHurt(PlayerProperty playerProperty, int hurt)
        {
            playerProperty.playerDamage.Hurt(hurt);
        }
    }
}
