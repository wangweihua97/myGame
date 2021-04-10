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

        public int explosionRadius;

        public void liftUpdate()
        {
            if(dead)
                return;
            startTIme += Time.deltaTime;
            if(startTIme >= liftTime)
            {
                dead = true;
                startTIme = 0;
                GameObjectPool.instance.RemoveGameObject(name ,gameObject);
            }
        }
        
        public void MoveUpdate()
        {
            if(dead)
                return;
            ySpped -= GravitySpped * Time.deltaTime;
            transform.position += new Vector3(xSpeed ,ySpped ,0) * Time.deltaTime;
        }
        
        //初始化
        void Start()
        {
            GameMgr.instance.AddUpdateEventListener(MoveUpdate);
            GameMgr.instance.AddLateUpdateEventListener(liftUpdate);
            Init();
        }

        //射击
        public virtual void Shoot(Transform transform, Vector2 angle, float power)
        {
        }
        //初始化
        public virtual void Init()
        {
        }
        
        public void OnRemove(Vector2 pos)
        {
            dead = true;
            startTIme = 0;
            if(explosionRadius <= 0)
                return;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, (float)explosionRadius/100);

            for (int i = 0; i < colliders.Length; i++)
            {
                // TODO: two calls for getcomponent is bad
                if(colliders[i].GetComponent<DestructibleSprite>())
                    colliders[i].GetComponent<DestructibleSprite>().ApplyDamage(pos, explosionRadius);
            }
            GameObjectPool.instance.RemoveGameObject(name ,gameObject);
        }
    }
}
