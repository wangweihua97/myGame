using Item;
using RobustFSM.Base;

namespace PlayerState
{
    public class MoveMainState:BHState
    {
        public override void AddStates()
        {
            AddState<MoveState>();

            SetInitialState<MoveState>();
        }

        public override void Enter()
        {
            base.Enter();
            InterState();
        }

        public override void ManualExecute()
        {
            base.ManualExecute();
        }
        
        void InterState()
        {
            /*switch (PlayerProperty.instance.arms)
            {
                case ItemEnum.ItemType.emptyArms:
                    ChangeState<IdleEmptyArmsState>();
                    break;
                case ItemEnum.ItemType.grenade:
                    ChangeState<IdleGrenadeState>();
                    break;
                case ItemEnum.ItemType.missile:
                    ChangeState<IdleMissileState>();
                    break;
                case ItemEnum.ItemType.pistol:
                    ChangeState<IdlePistolState>();
                    break;
                case ItemEnum.ItemType.rifle:
                    ChangeState<IdleRifleState>();
                    break;
                case ItemEnum.ItemType.shotgun:
                    ChangeState<IdleShotgunState>();
                    break;
                default:
                    ChangeState<IdleEmptyArmsState>();
                    break;
            }*/
        }

        /// <summary>
        /// Retrives the super state machine
        /// </summary>
        public PlayerFSM OwnerFSM
        {
            get
            {
                return (PlayerFSM)SuperMachine;
            }
        }
    }
}