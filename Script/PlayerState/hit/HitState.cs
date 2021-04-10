using Player;
using RobustFSM.Base;

namespace PlayerState
{
    public class HitState:BState
    {
        public override void Initialize()
        {
            base.Initialize();

            //set a specific name for this state
            StateName = "HitState";
        }

        public override void Enter()
        {
            OwnerFSM.OwnerFSM.gameObject.GetComponent<PlayerAmination>().UpdateAnimatin(false);
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

        public HitMainState OwnerFSM
        {
            get
            {
                return (HitMainState)Machine;
            }
        }
    }
}