using System;
using System.Collections;
using Mirror;
using Net;
using UnityEngine;
using UnityEngine.Events;

namespace Item
{
    public class ItemMgr : MonoBehaviour
    {
        public static ItemMgr instance;
        public GameObject rocket;
        public GameObject bullet;
        public GameObject rifleBullet;
        public GameObject grenade;
        public GameObject boom;

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
            if(NetMgr.instance)
                NetMgr.instance.SpawnGameObject(go);
            return go;
        }

        public void CreatBoom(Vector2 pos,float size)
        {
            GameObject go = GameObjectPool.instance.GetGameObject("boom");
            if (!go)
            {
                go = GameObject.Instantiate(boom);
            }

            go.transform.position = new Vector3(pos.x, pos.y, -1);
            go.transform.localScale = new Vector3(size*5, size*5, size*5);
            go.GetComponent<Animator>().Play("Boom");
            YieldAniFinish(go.GetComponent<Animator>(), "Boom", () =>
            {
                GameObjectPool.instance.RemoveGameObject("Boom",go);
            });
        }
        
        public static IEnumerator YieldAniFinish(Animator ani,string aniName, UnityAction action)
        {
            yield return null;
            AnimatorStateInfo stateinfo = ani.GetCurrentAnimatorStateInfo(0);

            if (stateinfo.IsName(aniName) && (stateinfo.normalizedTime > 1.0f))
            {
                action();
            }
            else
            {
                instance.StartCoroutine(YieldAniFinish(ani,aniName, action));
            }
        }
    }
}