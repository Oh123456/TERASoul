using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject MainCamera;
    CinemachineVirtualCamera virtualCamera;
    CinemachineTransposer transposer;
    RaycastHit hit;
    RaycastHit hitback;
    float Zoffset;
    float newZoffset;
    // Start is called before the first frame update
    void Start()
    {
        //virtualCamera = GetComponent<CinemachineVirtualCamera>();
        //transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        //Zoffset = transposer.m_FollowOffset.z;
        //newZoffset = Zoffset;
        //MainCamera = GameObject.FindWithTag("MainCamera");
        MainCamera = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vector3 = target.position;
        vector3.y += 1.0f;
        MainCamera.transform.LookAt(vector3);


        //Vector3 doffset = transposer.m_FollowOffset;
        Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward * 5.8f, Color.blue);
        Debug.DrawRay(MainCamera.transform.position, -1 * MainCamera.transform.forward * 0.10f, Color.blue);


        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit))
        {
            if (!(hit.collider.gameObject.tag == "Player"))
                MainCamera.transform.position = hit.point;
            else
            {
                Vector3 newvector3 = MainCamera.transform.localPosition;
                newvector3.z -= 0.1f;
                if (newvector3.z <= -5.8f)
                    newvector3.z = -5.8f;
                MainCamera.transform.localPosition = newvector3;
                if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit))
                    if (!(hit.collider.gameObject.tag == "Player"))
                        MainCamera.transform.position = hit.point;

                //if ((Physics.Raycast(transform.position, -1 * transform.forward, out hitback, 0.2f)))
                //{
                //    MainCamera.transform.position = hitback.point;
                //}
                //else if ((Physics.Raycast(transform.position, -1 * transform.forward, out hitback, 5.8f)))
                //{
                //    MainCamera.transform.position = hitback.point;
                //}
                //else
                //{

                //}
            }
        }


        //if (Physics.Raycast(transform.position, transform.forward, out hit, doffset.z * -1.0f))
        //{
        //    Debug.Log(hit.collider.gameObject.tag);
        //    if (!(hit.collider.gameObject.tag == "Player"))
        //    {
        //        virtualCamera.transform.position = hit.point;


            //        Vector3 offset = transposer.m_FollowOffset;
            //        offset.z += 0.01f;
            //        transposer.m_FollowOffset = offset;
            //        newZoffset = offset.z;
            //        MainCamera.transform.position = hit.point;

            //    }
            //    else
            //    {
            //        if (!(Physics.Raycast(transform.position, -1 * transform.forward, out hitback, 0.2f)))
            //        {
            //            Vector3 offset = transposer.m_FollowOffset;
            //            offset.z -= 0.01f;
            //            if (offset.z <= Zoffset)
            //                offset.z = Zoffset;
            //            transposer.m_FollowOffset = offset;
            //        }

            //    }

            //}


    }

}
