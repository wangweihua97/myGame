using Item;
using Player;
using RobustFSM.Base;
using UnityEngine.XR.WSA.Input;

namespace PlayerState
{
    public class JumpMainState:BHState
    {
        public override void AddStates()
        {
            AddState<JumpState>();

            SetInitialState<JumpState>();
        }

        public override void Enter()
        {
            base.Enter();
            OwnerFSM.gameObject.GetComponent<PlayerAmination>().UpdateAnimatin(false);
            EventCenter.instance.AddEventListener("onTheGround",ToIdleState);
        }
        
        void ToIdleState()
        {
            SuperMachine.ChangeState<IdleMainState>();
        }

        public override void Exit()
        {
            base.Exit();
            EventCenter.instance.RemoveEventListener("onTheGround",ToIdleState);
        }

        public override void ManualExecute()
        {
            base.ManualExecute();
        }
        
        void InterState()
        {
            
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