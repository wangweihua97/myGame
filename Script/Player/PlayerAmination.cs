using System;
using System.Collections;
using System.Collections.Generic;
using ArmsState;
using Item;
using PlayerState;
using Spine.Unity;
using UnityEngine;

namespace Player
{
    public class PlayerAmination : MonoBehaviour
    {
        /*public AnimationReferenceAsset idleEmptyArms,idleGrenade,idleMissile,idlePistol,idleRifle,idleShotgun;
        public AnimationReferenceAsset shootEmptyArms,shootGrenade,shootMissile,shootPistol,shootRifle,shootShotgun;
        public AnimationReferenceAsset handEmptyArms,handGrenade,handMissile,handPistol,handRifle,handShotgun;
        public AnimationReferenceAsset jump,toGround,idle,move,hit;*/
        public void UpdateAnimatin(bool isLoop)
        {
            if (GetComponent<ArmsFSM>().CurrentState == null || GetComponent<PlayerFSM>().CurrentState == null)
                return;
            string armsState = GetComponent<ArmsFSM>().CurrentState.StateName;
            string playerState = GetComponent<PlayerFSM>().CurrentState.StateName;
            PlayAnimation(armsState, playerState, isLoop);
        }

        void PlayAnimation(string armsState, string playerState, bool isLoop)
        {
            string animationName = "";
            switch (playerState)
            {
                case "IdleMainState":
                    animationName = "Idle_Gun_";
                    break;
                case "HitMainState":
                    animationName = "Damage_";
                    break;
                case "JumpMainState":
                    animationName = "Jump_";
                    break;
                case "MoveMainState":
                    animationName = "Walk_Gun_";
                    break;
                case "ShootMainState":
                    animationName = "Shoot_";
                    break;
                default:
                    return;
            }

            if (GetAminationName(armsState) == "")
                return;
            animationName += GetAminationName(armsState);
            GetComponent<PlayerProperty>().skeletonAnimation.AnimationState.SetAnimation(1, animationName, isLoop);
        }

        string GetAminationName(string armsState)
        {
            switch (armsState)
            {
                case "EmptyArmsState":
                    return "ColdWeapon_1";
                case "GrenadeState":
                    return "Grenade_1";
                case "MissileState":
                    return "RPG_1";
                case "PistolState":
                    return "Pistol_1";
                case "RifleState":
                    return "Submachine_2";
                case "ShotgunState":
                    return "Shotgun_1";
                default:
                    return "";
            }
        }
    }
}
