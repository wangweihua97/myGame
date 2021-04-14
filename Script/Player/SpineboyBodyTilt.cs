
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Player {
	public class SpineboyBodyTilt : NetworkBehaviour {

		[SpineBone]
		public string head = "head", leftHand = "leftHand", rightHand = "rightHand";
		Bone headBone, leftHandBone, rightHandBone;
		public PlayerProperty PlayerPropertyInstance;

		void Start () {
			var skeletonAnimation = GetComponent<SkeletonAnimation>();
			var skeleton = skeletonAnimation.Skeleton;

			leftHandBone = skeleton.FindBone(leftHand);
			rightHandBone = skeleton.FindBone(rightHand);
			headBone = skeleton.FindBone(head);
			skeletonAnimation.UpdateLocal += UpdateLocal;
		}

		private void UpdateLocal (ISkeletonAnimation animated)
		{
			leftHandBone.Rotation += PlayerPropertyInstance.aimAngle * PlayerPropertyInstance.faceHorizontal;
			rightHandBone.Rotation += PlayerPropertyInstance.aimAngle * PlayerPropertyInstance.faceHorizontal;
			headBone.Rotation += PlayerPropertyInstance.aimAngle * PlayerPropertyInstance.faceHorizontal;
		}
	}

}
