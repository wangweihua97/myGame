using System.Reflection;
using ArmsState;
using Item;
using Mirror;
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
            playerAmination.UpdateAnimatin(true);
        }
        else
        {
            anotherPlayer = this;
            playerAmination.UpdateAnimatin(true);
        }
        healthyUI = UIMgr.instance.CreatHealthyUI().GetComponent<HealthyUI>();
        healthyUI.Init(transform,100,100);
    }

    void UpdateHealthyUI()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
