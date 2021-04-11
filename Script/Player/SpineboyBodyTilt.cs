
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Player {
	public class SpineboyBodyTilt : MonoBehaviour {

		[SpineBone]
		public string head = "head", leftHand = "leftHand", rightHand = "rightHand";
		Bone headBone, leftHandBone, rightHandBone;

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
			leftHandBone.Rotation += PlayerProperty.instance.aimAngle * PlayerProperty.instance.faceHorizontal;
			rightHandBone.Rotation += PlayerProperty.instance.aimAngle * PlayerProperty.instance.faceHorizontal;
			headBone.Rotation += PlayerProperty.instance.aimAngle * PlayerProperty.instance.faceHorizontal;
		}
	}

}
