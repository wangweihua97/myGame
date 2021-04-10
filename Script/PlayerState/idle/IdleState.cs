using RobustFSM.Base;

namespace PlayerState.Idle
{
    public class IdleState:BState
    {
        public override void Initialize()
        {
            base.Initialize();

            //set a specific name for this state
            StateName = "IdleState";
        }
        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public IdleMainState OwnerFSM
        {
            get
            {
                return (IdleMainState)Machine;
            }
        }
    }
}