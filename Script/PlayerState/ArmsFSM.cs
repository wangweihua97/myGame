using System;
using Assets.RobustFSM.Mono;

namespace ArmsState
{
    public class ArmsFSM:MonoFSM
    {
        public override void AddStates()
        {
            //set the custom update frequenct
            SetUpdateFrequency(0.1f);

            //add the states
            AddState<EmptyArmsState>();
            AddState<GrenadeState>();
            AddState<MissileState>();
            AddState<PistolState>();
            AddState<RifleState>();
            AddState<ShotgunState>();

            //set the initial state
            SetInitialState<MissileState>();
        }
        public void ToEmptyArmsState()
        {
            ChangeState<EmptyArmsState>();
        }
        
        public void ToGrenadeState()
        {
            ChangeState<GrenadeState>();
        }
        
        public void ToMissileState()
        {
            ChangeState<MissileState>();
        }

        public void ToPistolState()
        {
            ChangeState<PistolState>();
        }
        
        public void ToRifleState()
        {
            ChangeState<RifleState>();
        }
        
        public void ToShotgunState()
        {
            ChangeState<ShotgunState>();
        }
    }
}
