using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCam : MonoBehaviour
{
    GameObject MainCamera;
    CinemachineVirtualCamera virtualCamera;
    CinemachineTransposer transposer;
    RaycastHit hit;
    float Zoffset;
    float newZoffset;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Zoffset = transposer.m_FollowOffset.z;
        newZoffset = Zoffset;
        MainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (!(hit.collider.gameObject.tag == "Player"))
            {
                virtualCamera.transform.position = hit.point;
                
                
                //Vector3 offset = transposer.m_FollowOffset;
                //offset.z += 0.1f;
                //transposer.m_FollowOffset = offset;
                //newZoffset = offset.z;
                //MainCamera.transform.position = hit.point;

            }
            else
            {

                //Vector3 offset = transposer.m_FollowOffset;
                //offset.z -= 0.1f;
                //if (offset.z <= Zoffset)
                //    offset.z = Zoffset;
                //transposer.m_FollowOffset = offset;
            }
        }

    }

}
