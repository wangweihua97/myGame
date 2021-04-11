using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EventCenter.instance.AddEventListener("keyJDown", Shoot);
        }

        public void Shoot()
        {
            GameObject go = GameObjectPool.instance.GetGameObject("rocket");
            if (!go)
            {
                go = GameObject.Instantiate(ItemMgr.instance.rocket);
            }
            go.GetComponent<Iitem>().Shoot(transform,new Vector2(10,10),0.1f);
        }
    }
}
