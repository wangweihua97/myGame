using System;
using UnityEngine;

namespace Item
{
    public class ItemMgr : MonoBehaviour
    {
        public static ItemMgr instance;
        public GameObject rocket;

        private void Awake()
        {
            instance = this;
        }
    }
}