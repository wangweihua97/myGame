using Item;
using Player;
using PlayerState.Idle;
using RobustFSM.Base;
using UnityEngine.XR.WSA.Input;

namespace PlayerState
{
    public class IdleMainState:BHState
    {
        public override void AddStates()
        {
            AddState<IdleState>();
            SetInitialState<IdleState>();
        }

        public override void Enter()
        {
            base.Enter();
            OwnerFSM.gameObject.GetComponent<PlayerAmination>().UpdateAnimatin(true);
        }

        public override void ManualExecute()
        {
            base.ManualExecute();
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