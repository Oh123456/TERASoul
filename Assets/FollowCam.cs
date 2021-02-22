using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCam : MonoBehaviour
{

    Cinemachine.CinemachineVirtualCamera virtualCamera;

    bool hit = false;
    bool offestReset = true;
    CinemachineTransposer transposer;
    float distance;
    float newoffset;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        distance = transposer.m_FollowOffset.z;
        newoffset = distance;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 offset = transposer.m_FollowOffset;
        //offset.z += 0.1f;
        //if (offset.z >= newoffset)
        //    offset.z = newoffset;
        //transposer.m_FollowOffset = offset;
        if (!hit)
        {
            Vector3 offset = transposer.m_FollowOffset;
            offset.z -= 0.1f;
            if (offset.z <= distance)
                offset.z = distance;
            transposer.m_FollowOffset = offset;
        }

        if (transposer.m_FollowOffset.z >= -2.0f)
        {
            Vector3 offset = transposer.m_FollowOffset;
            offset.z = -2.0f;
            transposer.m_FollowOffset = offset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hit = true;
    }

    private void OnTriggerStay(Collider other)
    {
       
        Vector3 offset = transposer.m_FollowOffset;
        offset.z += 0.1f;
        if (offset.z >= -2.0f)
            offset.z = -2.0f;
        transposer.m_FollowOffset = offset;
      

    }

    private void OnTriggerExit(Collider other)
    {

        hit = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 offset = transposer.m_FollowOffset;
        offset.z += 0.1f;
        if (offset.z >= -2.0f)
            offset.z = -2.0f;
        transposer.m_FollowOffset = offset;
   
    }
}
