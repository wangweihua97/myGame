using Player;
using RobustFSM.Base;

namespace PlayerState
{
    public class MoveState:BState
    {
        public override void Initialize()
        {
            base.Initialize();

            //set a specific name for this state
            StateName = "MoveState";
        }

        public override void Enter()
        {
            base.Enter();
            OwnerFSM.OwnerFSM.gameObject.GetComponent<PlayerAmination>().UpdateAnimatin(true);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public MoveMainState OwnerFSM
        {
            get
            {
                return (MoveMainState)Machine;
            }
        }
    }
}