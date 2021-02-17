using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttachment : MonoBehaviour
{
    [SerializeField]
    Transform L_Arm;
    [SerializeField]
    Transform shieldTransform;
    Animator ownerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        ownerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnAnimatorIK(int layerIndex)
    {
        //return ;
        //twoHandWeapon.position = ownerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        shieldTransform.position = ownerAnimator.GetIKHintPosition(AvatarIKHint.LeftElbow);
        ///shieldTransform.rotation = ownerAnimator.GetIKHint(AvatarIKGoal.RightHand);

        ownerAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1.0f);
        //ownerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        ownerAnimator.SetIKHintPosition(AvatarIKHint.LeftElbow, L_Arm.position);
        //ownerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, L_Arm.rotation);
    }
}
