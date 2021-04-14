using System.Collections;
using System.Collections.Generic;
using ArmsState;
using Item;
using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : NetworkBehaviour
    {
        public GameObject powerIcon;

        public float[] CurCD;
        public float[] MaxCD;
        private float MaxPower;
        private float startPower;
        [SyncVar]
        private bool Shooed;
        [SyncVar]
        private float power;
        private float rifleSpeed;
        private float rifleTime;
        public PlayerProperty PlayerPropertyInstance;
        
        // Start is called before the first frame update
        void Start()
        {
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
            GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
        }
        
        void InitUpdate()
        {
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyJDown", KeyJDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyJUp", KeyJUp);
            GameMgr.instance.AddFristUpdateEventListener(CDUpdate);
            GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
        }

        void KeyJDown()
        {
            PlayerPropertyInstance.canSwitchWeapon = false;
            if(!PlayerPropertyInstance.canShoot || Shooed)
                return;
            switch (PlayerPropertyInstance.arms)
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
                    PlayerPropertyInstance.playerAmination.PlayShootAnimation(PlayerPropertyInstance.armsFsm.CurrentState.StateName ,true);
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
            PlayerPropertyInstance.canSwitchWeapon = true;
            if(!PlayerPropertyInstance.canShoot || Shooed == false)
                return;
            switch (PlayerPropertyInstance.arms)
            {
                case ItemEnum.ItemType.missile:
                    GameMgr.instance.RemoveUpdateEventListener(AddPowerUpdate);
                    powerIcon.SetActive(false);
                    PlayerPropertyInstance.playerAmination.PlayShootAnimation(PlayerPropertyInstance.armsFsm.CurrentState.StateName ,false);
                    Shoot(power/MaxPower);
                    CurCD[0] = MaxCD[0];
                    break;
                case ItemEnum.ItemType.pistol:
                    PlayerPropertyInstance.playerAmination.PlayShootAnimation(PlayerPropertyInstance.armsFsm.CurrentState.StateName ,false);
                    Shoot(1);
                    CurCD[1] = MaxCD[1];
                    break;
                case ItemEnum.ItemType.rifle:
                    GameMgr.instance.RemoveUpdateEventListener(RifleShootUpdate);
                    PlayerPropertyInstance.playerAmination.ClearAnimation(2);
                    break;
                case ItemEnum.ItemType.grenade:
                    GameMgr.instance.RemoveUpdateEventListener(AddPowerUpdate);
                    powerIcon.SetActive(false);
                    Shoot(power/MaxPower);
                    CurCD[2] = MaxCD[2];
                    PlayerPropertyInstance.playerAmination.PlayShootAnimation(PlayerPropertyInstance.armsFsm.CurrentState.StateName ,false);
                    break;
            }
            PlayerPropertyInstance.playerAmination.PlayEmptyAnimation();
            Shooed = false;
            PlayerPropertyInstance.playerAmination.UpdateAnimatin(true);
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
            if(!PlayerPropertyInstance.canShoot)
                return;
            GameObject go = ItemMgr.instance.CreateItem(PlayerPropertyInstance.arms);
            go.GetComponent<Iitem>().Shoot(transform,PlayerPropertyInstance.faceHorizontal,PlayerPropertyInstance.aimAngle,powerValue);
        }
    }
}
