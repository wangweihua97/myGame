using Unity.Mathematics;
using UnityEngine;

namespace Item
{
    public class Grenade:Iitem
    {
        public float speed;
        private float _rotationZ;
        private float rotationSpeed;
        private void OnCreat()
        {
            base.Init();
            liftTime = 5;
            speed = 30;
            GravitySpped = 0;
            explosionRadius = 80;
            name = "grenade";
            GetComponent<Rigidbody2D>().gravityScale = 1;
            _rotationZ = 0;
            minHurt = 5;
            MaxHurt = 40;
        }

        public override void Shoot(Transform transform,int faceHorizontal,float angle, float power)
        {
            base.Shoot(transform,faceHorizontal,angle,power);
            OnCreat();
            dead = false;
            rotationSpeed = faceHorizontal == 1 ? -20 : 20;
            gameObject.transform.position = transform.position + new Vector3(1.6f * faceHorizontal* Mathf.Cos(Mathf.Deg2Rad*angle)+0.5f* faceHorizontal,0.8f+faceHorizontal*3f* Mathf.Sin(Mathf.Deg2Rad*angle),0);
            xSpeed = 0;
            ySpped = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(faceHorizontal * speed * power * Mathf.Cos(Mathf.Deg2Rad*angle), faceHorizontal * speed * power * Mathf.Sin(Mathf.Deg2Rad*angle));
            GameMgr.instance.AddUpdateEventListener(RotationUpdate);
        }
        
        public override bool Collision(Collision2D coll)
        {
            if (!coll.gameObject.CompareTag(tag: "ground"))
                return true;
            GameMgr.instance.RemoveUpdateEventListener(RotationUpdate);
            return false;
        }
        
        public override void LifeEnd()
        {
            GameMgr.instance.RemoveUpdateEventListener(RotationUpdate);
            OnRemove(new Vector2(transform.position.x, transform.position.y) ,null);
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        void RotationUpdate()
        {
            _rotationZ += rotationSpeed * Time.deltaTime;
            transform.rotation = quaternion.Euler(0,0,_rotationZ);
        }
    }
}