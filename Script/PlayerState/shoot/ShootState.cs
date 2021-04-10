using Player;
using RobustFSM.Base;

namespace PlayerState
{
    public class ShootState:BState
    {
        public override void Initialize()
        {
            base.Initialize();

            //set a specific name for this state
            StateName = "ShootState";
        }

        public override void Enter()
        {
            base.Enter();
            OwnerFSM.OwnerFSM.gameObject.GetComponent<PlayerAmination>().UpdateAnimatin(false);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public ShootMainState OwnerFSM
        {
            get
            {
                return (ShootMainState)Machine;
            }
        }
    }
}