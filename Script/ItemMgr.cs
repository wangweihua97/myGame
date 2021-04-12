using System;
using UnityEngine;

namespace Item
{
    public class ItemMgr : MonoBehaviour
    {
        public static ItemMgr instance;
        public GameObject rocket;
        public GameObject bullet;
        public GameObject rifleBullet;
        public GameObject grenade;

        private void Awake()
        {
            instance = this;
        }
        
        public GameObject CreateItem(ItemEnum.ItemType itemType)
        {
            GameObject go;
            switch (itemType)
            {
                case ItemEnum.ItemType.missile:
                    go = GameObjectPool.instance.GetGameObject("rocket");
                    if (!go)
                    {
                        go = GameObject.Instantiate(rocket);
                    }
                    break;
                case ItemEnum.ItemType.pistol:
                    go = GameObjectPool.instance.GetGameObject("bullet");
                    if (!go)
                    {
                        go = GameObject.Instantiate(bullet);
                    }
                    break;
                case ItemEnum.ItemType.rifle:
                    go = GameObjectPool.instance.GetGameObject("rifleBullet");
                    if (!go)
                    {
                        go = GameObject.Instantiate(rifleBullet);
                    }
                    break;
                case ItemEnum.ItemType.grenade:
                    go = GameObjectPool.instance.GetGameObject("grenade");
                    if (!go)
                    {
                        go = GameObject.Instantiate(grenade);
                    }
                    break;
                default:
                    go = GameObjectPool.instance.GetGameObject("rocket");
                    if (!go)
                    {
                        go = GameObject.Instantiate(rocket);
                    }
                    break;
            }
            return go;
        }
    }
}