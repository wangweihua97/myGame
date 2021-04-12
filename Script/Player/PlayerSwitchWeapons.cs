using System;
using System.Collections;
using System.Collections.Generic;
using ArmsState;
using Item;
using UnityEngine;

namespace Player
{
    public class PlayerSwitchWeapons : MonoBehaviour
    {
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
            EventCenter.instance.AddEventListener("keyQDown", KeyQDown);
        }

        // Update is called once per frame
        void KeyQDown()
        {
            if(!PlayerProperty.instance.canSwitchWeapon)
                return;
            int orderIndex = GetOrder(GetComponent<PlayerProperty>().arms) + 1;
            orderIndex = orderIndex > 4 ? 1 : orderIndex;
            ItemEnum.ItemType itemType = order[orderIndex];
            GetComponent<PlayerProperty>().arms = itemType;
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
                    ArmsFSM.instance.ToMissileState();
                    break;
                case ItemEnum.ItemType.pistol:
                    ArmsFSM.instance.ToPistolState();
                    break;
                case ItemEnum.ItemType.rifle:
                    ArmsFSM.instance.ToRifleState();
                    break;
                case ItemEnum.ItemType.grenade:
                    ArmsFSM.instance.ToGrenadeState();
                    break;
                default:
                    break;
            }
            PlayerAmination.instance.PlaySwitchAnimation(ArmsFSM.instance.CurrentState.StateName);
        }
    }
}
