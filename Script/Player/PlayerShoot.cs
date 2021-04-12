using System.Collections;
using System.Collections.Generic;
using ArmsState;
using Item;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public GameObject powerIcon;

        public float[] CurCD;
        public float[] MaxCD;
        private float MaxPower;
        private float startPower;
        private bool Shooed;
        private float power;
        private float rifleSpeed;
        private float rifleTime;
        
        // Start is called before the first frame update
        void Start()
        {
            EventCenter.instance.AddEventListener("keyJDown", KeyJDown);
            EventCenter.instance.AddEventListener("keyJUp", KeyJUp);
            CurCD = new float[3];
            MaxCD = new float[3];
            MaxCD[0] = 4;
            MaxCD[1] = 1;
            MaxCD[2] = 4;
            CurCD[0] = 0;
            CurCD[1] = 0;
            CurCD[2] = 0;
            /*MaxCD.Add(ItemEnum.ItemType.missile,4);
            MaxCD.Add(ItemEnum.ItemType.pistol,1);
            MaxCD.Add(ItemEnum.ItemType.grenade,4);
            CurCD.Add(ItemEnum.ItemType.missile,0);
            CurCD.Add(ItemEnum.ItemType.pistol,0);
            CurCD.Add(ItemEnum.ItemType.grenade,0);*/
            startPower = 0.5f;
            MaxPower = 4.1f;
            rifleSpeed = 0.2f;
            rifleTime = 0f;
            Shooed = false;
            powerIcon.SetActive(false);
            power = startPower;
            GameMgr.instance.AddFristUpdateEventListener(CDUpdate);
        }

        void KeyJDown()
        {
            PlayerProperty.instance.canSwitchWeapon = false;
            if(!PlayerProperty.instance.canShoot || Shooed)
                return;
            switch (PlayerProperty.instance.arms)
            {
                case ItemEnum.ItemType.missile:
                    if(CurCD[0] > 0)
                        return;
                    else
                    {
                        power = startPower;
                        powerIcon.SetActive(true);
                        GameMgr.instance.AddUpdateEventListener(AddPowerUpdate);
                    }
                    break;
                case ItemEnum.ItemType.pistol:
                    if(CurCD[1] > 0)
                        return;
                    break;
                case ItemEnum.ItemType.rifle:
                    rifleTime = 0;
                    GameMgr.instance.AddUpdateEventListener(RifleShootUpdate);
                    PlayerAmination.instance.PlayShootAnimation(ArmsFSM.instance.CurrentState.StateName ,true);
                    break;
                case ItemEnum.ItemType.grenade:
                    if(CurCD[2] > 0)
                        return;
                    else
                    {
                        power = startPower;
                        powerIcon.SetActive(true);
                        GameMgr.instance.AddUpdateEventListener(AddPowerUpdate);
                    }
                    break;
            }
            Shooed = true;
        }

        void CDUpdate()
        {
            for (int i = 0; i < CurCD.Length; i++)
            {
                if(CurCD[i] > 0)
                    CurCD[i] = CurCD[i] - Time.deltaTime;
            }
        }
        
        void KeyJUp()
        {
            PlayerProperty.instance.canSwitchWeapon = true;
            if(!PlayerProperty.instance.canShoot || Shooed == false)
                return;
            switch (PlayerProperty.instance.arms)
            {
                case ItemEnum.ItemType.missile:
                    GameMgr.instance.RemoveUpdateEventListener(AddPowerUpdate);
                    powerIcon.SetActive(false);
                    PlayerAmination.instance.PlayShootAnimation(ArmsFSM.instance.CurrentState.StateName ,false);
                    Shoot(power/MaxPower);
                    CurCD[0] = MaxCD[0];
                    break;
                case ItemEnum.ItemType.pistol:
                    PlayerAmination.instance.PlayShootAnimation(ArmsFSM.instance.CurrentState.StateName ,false);
                    Shoot(1);
                    CurCD[1] = MaxCD[1];
                    break;
                case ItemEnum.ItemType.rifle:
                    GameMgr.instance.RemoveUpdateEventListener(RifleShootUpdate);
                    PlayerAmination.instance.ClearAnimation(2);
                    break;
                case ItemEnum.ItemType.grenade:
                    GameMgr.instance.RemoveUpdateEventListener(AddPowerUpdate);
                    powerIcon.SetActive(false);
                    Shoot(power/MaxPower);
                    CurCD[2] = MaxCD[2];
                    PlayerAmination.instance.PlayShootAnimation(ArmsFSM.instance.CurrentState.StateName ,false);
                    break;
            }
            PlayerAmination.instance.PlayEmptyAnimation();
            Shooed = false;
            PlayerAmination.instance.UpdateAnimatin(true);
            //StartCoroutine(ShootCD());
        }

        void AddPowerUpdate()
        {
            power += Time.deltaTime * 1f;
            if (power > MaxPower)
                power = MaxPower;
            powerIcon.GetComponent<SpriteRenderer>().size = new Vector2(powerIcon.GetComponent<SpriteRenderer>().size.x,power);
        }
        
        void RifleShootUpdate()
        {
            rifleTime += Time.deltaTime;
            if (rifleTime > 0.25)
            {
                Shoot(1);
                rifleTime -= 0.25f;
            }
                
        }

        public void Shoot(float powerValue)
        {
            if(!PlayerProperty.instance.canShoot)
                return;
            GameObject go = ItemMgr.instance.CreateItem(PlayerProperty.instance.arms);
            go.GetComponent<Iitem>().Shoot(transform,GetComponent<PlayerProperty>().faceHorizontal,GetComponent<PlayerProperty>().aimAngle,powerValue);
        }
    }
}
