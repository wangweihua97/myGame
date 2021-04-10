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
            speed = 20;
            GravitySpped = 1;
            explosionRadius = 40;
            name = "rocket";
        }

        public override void Shoot(Transform transform, Vector2 angle, float power)
        {
            OnCreat();
            dead = false;
            gameObject.transform.position = transform.position + new Vector3(2,2,0);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            xSpeed = speed * power * angle.x / Mathf.Sqrt(Mathf.Pow(angle.x,2) + Mathf.Pow(angle.y,2));
            ySpped = speed * power * angle.y / Mathf.Sqrt(Mathf.Pow(angle.x,2) + Mathf.Pow(angle.y,2));
        }

        void OnCollisionEnter2D (Collision2D coll)
        {
            if (dead) {
                return;
            }
            ContactPoint2D contact = coll.contacts[0];
            Vector2 pos = contact.point;
            if (coll.gameObject.CompareTag(tag: "ground"))
                OnRemove(pos);
        }
    }
}