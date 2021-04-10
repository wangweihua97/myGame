using System;
using Assets.RobustFSM.Mono;

namespace ArmsState
{
    public class ArmsFSM:MonoFSM
    {
        public static ArmsFSM instance;

        public void Awake()
        {
            instance = this;
        }
        
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
            SetInitialState<PistolState>();
        }
    }
}
