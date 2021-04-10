using RobustFSM.Base;

namespace PlayerState
{
    public class JumpState:BState
    {
        public override void Initialize()
        {
            base.Initialize();

            //set a specific name for this state
            StateName = "JumpState";
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

        public JumpMainState OwnerFSM
        {
            get
            {
                return (JumpMainState)Machine;
            }
        }
    }
}