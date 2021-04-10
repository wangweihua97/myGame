using System;
using Assets.RobustFSM.Mono;

namespace PlayerState
{
    public class PlayerFSM:MonoFSM
    {
        public static PlayerFSM instance;

        public void Awake()
        {
            instance = this;
        }
        
        public override void AddStates()
        {
            //set the custom update frequenct
            SetUpdateFrequency(0.1f);

            //add the states
            AddState<IdleMainState>();
            AddState<HitMainState>();
            AddState<MoveMainState>();
            AddState<JumpMainState>();
            AddState<ShootMainState>();

            //set the initial state
            SetInitialState<IdleMainState>();
        }

        public void ToMove()
        {
            ChangeState<MoveMainState>();
        }
        
        public void ToIdel()
        {
            ChangeState<IdleMainState>();
        }
        
        public void ToJump()
        {
            ChangeState<JumpMainState>();
        }

        public void ToHit()
        {
            ChangeState<HitMainState>();
        }
        
        public void ToShoot()
        {
            ChangeState<ShootMainState>();
        }

    }
}