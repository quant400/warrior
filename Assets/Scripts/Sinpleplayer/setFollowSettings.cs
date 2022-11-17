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
    //CinemachineTransposer thirdperson;
    CinemachineFramingTransposer thirdperson;
    [Header("Player Grounded")]
    public GroundCheck groundCheck = null;
    // Start is called before the first frame update
    void Start()
    {
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").gameObject.GetComponent<GroundCheck>();

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


            
            thirdperson = followCameraSettings.GetCinemachineComponent<CinemachineFramingTransposer>();
            thirdperson.m_YDamping = 5;

            //thirdperson.m_DeadZoneHeight = 0.2f;
            thirdperson.m_SoftZoneHeight = 2;


            /*
            thirdperson = followCameraSettings.GetCinemachineComponent<CinemachineTransposer>();
                        thirdperson.m_XDamping = 0;
            thirdperson.m_YDamping = 10;
            */

            /*
            followCameraSettings.AddCinemachineComponent<CinemachineComposer>();


            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 0;
            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 20;
            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_DeadZoneHeight = 2;
            followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = 2;
            */
            

            //followCameraSettings.GetCinemachineComponent<CinemachineComposer>().m_ScreenY = 1.1f;



        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.transform.parent.transform.position.y > 15)
        {
            if(thirdperson.m_TrackedObjectOffset.y > -2)
            {
                thirdperson.m_TrackedObjectOffset.y -= Time.deltaTime;
            }
            
        }
        else if (player.transform.parent.transform.position.y > 5)
        {
            if (thirdperson.m_TrackedObjectOffset.y > 0)
            {
                thirdperson.m_TrackedObjectOffset.y -= Time.deltaTime;
            }
        }
        else
        {
            if (thirdperson.m_TrackedObjectOffset.y < 2)
            {
                thirdperson.m_TrackedObjectOffset.y += Time.deltaTime;
            }
        }

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

        /*
        if(grounded)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
        }
        */
    }

    public bool grounded
    {
        get
        {
            if (groundCheck != null)
            {
                return groundCheck.CheckGrounded();
            }
            else
            {
                return false;
            }
        }
    }
}
