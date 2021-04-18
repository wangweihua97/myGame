using System.Collections;
using System.Reflection;
using ArmsState;
using Item;
using Mirror;
using Net;
using Player;
using PlayerState;
using Spine.Unity;
using UnityEngine;

public class PlayerProperty : NetworkBehaviour
{
    public static PlayerProperty localPlayer;
    public static PlayerProperty anotherPlayer;
    public PlayerFSM playerFsm;
    public ArmsFSM armsFsm;
    public ItemEnum.ItemType arms;
    public SkeletonAnimation skeletonAnimation;
    public float aimAngle = 0;
    public int faceHorizontal = 1;
    public PlayerAmination playerAmination;
    public PlayerDamage playerDamage;
    public bool isJump;
    public bool canMove;
    public bool canShoot;
    public bool canSwitchWeapon;
    public int healthy;
    public HealthyUI healthyUI;
    public EventCenter EventCenterInstance;
    public InputMgr _inputMgr;

    private void Awake()
    {
        canMove = true;
        canShoot = true;
        canSwitchWeapon = true;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        playerAmination = GetComponent<PlayerAmination>();
        playerFsm = GetComponent<PlayerFSM>();
        armsFsm = GetComponent<ArmsFSM>();
        EventCenterInstance = GetComponent<EventCenter>();
        playerDamage = GetComponent<PlayerDamage>();
        _inputMgr = GetComponent<InputMgr>();
        playerDamage.PlayerPropertyInstance = this;
        playerAmination.PlayerPropertyInstance = this;
        GetComponent<PlayerAim>().PlayerPropertyInstance = this;
        GetComponent<PlayerMove>().PlayerPropertyInstance = this;
        GetComponent<PlayerShoot>().PlayerPropertyInstance = this;
        GetComponent<PlayerSwitchWeapons>().PlayerPropertyInstance = this;
        GetComponent<SpineboyBodyTilt>().PlayerPropertyInstance = this;
        GetComponent<PlayerCamera>().PlayerPropertyInstance = this; 
        arms = ItemEnum.ItemType.missile;
        healthy = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
            UIMgr.instance.PlayerPropertyInstance = this;
        }
        else
        {
            anotherPlayer = this;
            
        }
        playerAmination.UpdateAnimatin(true);
        healthyUI = UIMgr.instance.CreatHealthyUI().GetComponent<HealthyUI>();
        healthyUI.Init(transform,100,100);
        GameMgr.instance.AddFristUpdateEventListener(UpdateHealthy);
    }

    void UpdateHealthy()
    {
        if (healthy <= 0)
        {
            if (isLocalPlayer)
            {
                UIMgr.instance.ShowResult("失败");
            }
            else
            {
                UIMgr.instance.ShowResult("成功");
            }
            StartCoroutine(EndGame());
        }
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        RoomPlayer.local.RetrunToRoom();
    }
}
