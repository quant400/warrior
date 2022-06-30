using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
public class setFollowSettings : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCameraSettings;
    [SerializeField] GameObject player;
    [SerializeField] StarterAssetsInputs _input;
    CinemachineTransposer thirdperson;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (_input == null)
        {
            _input = player.GetComponent<StarterAssetsInputs>();
        }
        if (followCameraSettings == null)
        {
            followCameraSettings = GetComponent<CinemachineVirtualCamera>();
        }
       
        if (followCameraSettings != null)
        {
            //followCameraSettings.LookAt = followCameraSettings.Follow;
            //thirdperson = followCameraSettings.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            thirdperson = followCameraSettings.GetCinemachineComponent<CinemachineTransposer>();
            followCameraSettings.AddCinemachineComponent<CinemachineComposer>();
            thirdperson.m_XDamping = 0;
            thirdperson.m_YDamping = 10;

            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 0;
            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 20;
            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = 2;
            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 2;



        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (followCameraSettings != null)
        {

            if (_input.sprint)
            {
                thirdperson.Damping.z = 1.2f;
            }
            else
            {
                thirdperson.Damping.z = 0.5f;
            }
        }
        */
    }
}
