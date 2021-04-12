using System;
using UnityEngine;

namespace Item
{
    public class Rocket:Iitem
    {
        public float speed;
        private void OnCreat()
        {
            base.Init();
            liftTime = 10;
            speed = 30;
            GravitySpped = 4;
            explosionRadius = 40;
            name = "rocket";
            initZAngle = -90f;
        }

        public override void Shoot(Transform transform,int faceHorizontal,float angle, float power)
        {
            base.Shoot(transform,faceHorizontal,angle,power);
            OnCreat();
            dead = false;
            GetComponent<SpriteRenderer>().flipY = faceHorizontal == -1;
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