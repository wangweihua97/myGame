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
    private void Awake()
    {
        instance = this;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        playerAmination = GetComponent<PlayerAmination>();
        arms = ItemEnum.ItemType.pistol;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
