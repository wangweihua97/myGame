using Player;
using RobustFSM.Base;

namespace ArmsState
{
    public class ShotgunState:BState
    {
        public override void Initialize()
        {
            base.Initialize();

            //set a specific name for this state
            StateName = "ShotgunState";
        }

        public override void Enter()
        {
            base.Enter();
            OwnerFSM.gameObject.GetComponent<PlayerAmination>().UpdateAnimatin(true);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public ArmsFSM OwnerFSM
        {
            get
            {
                return (ArmsFSM)Machine;
            }
        }
    }
}