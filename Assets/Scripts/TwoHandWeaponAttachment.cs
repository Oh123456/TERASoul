using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandWeaponAttachment : MonoBehaviour
{
    [SerializeField]
    Transform l_Hand;
    [SerializeField]
    Transform r_Hand;
    [SerializeField]
    Transform twoHandWeapon;
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
        twoHandWeapon.position = ownerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        ownerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand , 1.0f);
        ownerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand , 1.0f);

        ownerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, l_Hand.position);
        ownerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, l_Hand.rotation);

        ownerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand , 1.0f);
        ownerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand , 1.0f);

        ownerAnimator.SetIKPosition(AvatarIKGoal.RightHand, r_Hand.position);
        ownerAnimator.SetIKRotation(AvatarIKGoal.RightHand, r_Hand.rotation);
    }
}
