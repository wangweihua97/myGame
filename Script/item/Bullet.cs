using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Bullet:Iitem
    {
        public float speed;
        private void OnCreat()
        {
            base.Init();
            liftTime = 10;
            speed = 40;
            GravitySpped = 0;
            explosionRadius = 10;
            name = "bullet";
        }

        public override void Shoot(Transform transform,int faceHorizontal,float angle, float power)
        {
            base.Shoot(transform,faceHorizontal,angle,power);
            OnCreat();
            dead = false;
            gameObject.transform.position = transform.position + new Vector3(1.6f * faceHorizontal* Mathf.Cos(Mathf.Deg2Rad*angle)+0.5f* faceHorizontal,0.8f+faceHorizontal*3f* Mathf.Sin(Mathf.Deg2Rad*angle),0);
            xSpeed = faceHorizontal * speed * power * Mathf.Cos(Mathf.Deg2Rad*angle);
            ySpped = faceHorizontal * speed * power * Mathf.Sin(Mathf.Deg2Rad*angle);
        }
        
        public override bool Collision(Collision2D coll)
        {
            return true;
        }
    }
}