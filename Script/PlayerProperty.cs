using Item;
using Player;
using Spine.Unity;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public static PlayerProperty instance;
    public ItemEnum.ItemType arms;
    public SkeletonAnimation skeletonAnimation;
    public float aimAngle = 0;
    public int faceHorizontal = 1;
    public PlayerAmination playerAmination;
    public bool isJump;
    public bool canMove;
    public bool canShoot;
    public bool canSwitchWeapon;
    public int healthy;
    private HealthyUI healthyUI;
    private void Awake()
    {
        instance = this;
        canMove = true;
        canShoot = true;
        canSwitchWeapon = true;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        playerAmination = GetComponent<PlayerAmination>();
        arms = ItemEnum.ItemType.missile;
        healthy = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerAmination.instance.UpdateAnimatin(true);
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
