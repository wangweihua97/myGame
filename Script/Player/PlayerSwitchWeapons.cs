using System;
using System.Collections;
using System.Collections.Generic;
using ArmsState;
using Item;
using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerSwitchWeapons : NetworkBehaviour
    {
        public PlayerProperty PlayerPropertyInstance;
        //切换武器的顺序是导弹，手枪，步枪，手榴弹
        private Dictionary<int, ItemEnum.ItemType> order;

        private void Awake()
        {
            order = new Dictionary<int, ItemEnum.ItemType>();
            order.Add(1,ItemEnum.ItemType.missile);
            order.Add(2,ItemEnum.ItemType.pistol);
            order.Add(3,ItemEnum.ItemType.rifle);
            order.Add(4,ItemEnum.ItemType.grenade);
        }

        // Start is called before the first frame update
        void Start()
        {
            GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
        }
        
        void InitUpdate()
        {
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyQDown", KeyQDown);
            GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
        }

        // Update is called once per frame
        void KeyQDown()
        {
            if(!PlayerPropertyInstance.canSwitchWeapon)
                return;
            int orderIndex = GetOrder(PlayerPropertyInstance.arms) + 1;
            orderIndex = orderIndex > 4 ? 1 : orderIndex;
            ItemEnum.ItemType itemType = order[orderIndex];
            PlayerPropertyInstance.arms = itemType;
            ChangeArmsState(itemType);
        }

        int GetOrder(ItemEnum.ItemType itemType)
        {
            foreach (var item in order)
            {
                if (item.Value == itemType)
                    return item.Key;
            }
            return 0;
        }

        void ChangeArmsState(ItemEnum.ItemType itemType)
        {
            switch (itemType)
            {
                case ItemEnum.ItemType.missile:
                    PlayerPropertyInstance.armsFsm.ToMissileState();
                    break;
                case ItemEnum.ItemType.pistol:
                    PlayerPropertyInstance.armsFsm.ToPistolState();
                    break;
                case ItemEnum.ItemType.rifle:
                    PlayerPropertyInstance.armsFsm.ToRifleState();
                    break;
                case ItemEnum.ItemType.grenade:
                    PlayerPropertyInstance.armsFsm.ToGrenadeState();
                    break;
                default:
                    break;
            }
            PlayerPropertyInstance.playerAmination.PlaySwitchAnimation(PlayerPropertyInstance.armsFsm.CurrentState.StateName);
        }
    }
}
