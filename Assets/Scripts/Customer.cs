using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Customer : MonoBehaviour
{
    public GameObject GameManager;
    

    // 0 - outside; 1 - order; 2 - plate; 3 - check
    public int _state;

    // 0 - no player; 1 - p1; 2 - p2
    public int _playerState;

    // 1 - table 1; 2 - table 2; 3 - table 3; 4 - table 4
    public int _tableNum;

    // how long customers wait until leaving (base patience is for resetting patience)
    public float _patienceTimer;
    public float _basePatience;

    // true - is VIP; false - normal customer
    public bool _isVIP;

    // true - need service, patience starts reducing; false - don't need service, reduce service cooldown
    public bool _serviceRequired;
    public float _serviceTimer;
    public float _serviceCooldown;

    // counts player's click
    public int _clickingCount;


    public FMODUnity.EventReference VIP_angry;
    public FMODUnity.EventReference up_angry;
    public FMODUnity.EventReference left_angry;
    public FMODUnity.EventReference down_angry;
    public FMODUnity.EventReference right_angry;

    public FMODUnity.EventReference up_order_request;
    public FMODUnity.EventReference left_order_request;
    public FMODUnity.EventReference down_order_request;
    public FMODUnity.EventReference right_order_request;

    public FMODUnity.EventReference up_plate_request;
    public FMODUnity.EventReference left_plate_request;
    public FMODUnity.EventReference down_plate_request;
    public FMODUnity.EventReference right_plate_request;

    public FMODUnity.EventReference up_check_request;
    public FMODUnity.EventReference left_check_request;
    public FMODUnity.EventReference down_check_request;
    public FMODUnity.EventReference right_check_request;

    // Start is called before the first frame update
    void Start()
    {
        _patienceTimer = _basePatience;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        //If there's a service request, patience goes down
        if (_serviceRequired)
        {
            if (_isVIP == true)
                _patienceTimer -= Time.deltaTime * 3f;
            else
            {
                _patienceTimer -= Time.deltaTime * 2;
            }
        }

        //If he has no patience, end game
        if (_patienceTimer <= 0)
        {

            if (_isVIP)
            {
                //playsound_VIP_angry
                GameManager.GetComponent<GameManager>().getVIPAngry();
            }
            else
            {
                if (_tableNum == 1)
                {
                    //playsound_up_angry
                    GameManager.GetComponent<GameManager>().getAngry(1f);
                }
                if (_tableNum == 2)
                {
                    //playsound_left_angry
                    GameManager.GetComponent<GameManager>().getAngry(2f);
                }
                if (_tableNum == 3)
                {
                    //playsound_down_angry
                    GameManager.GetComponent<GameManager>().getAngry(4f);
                }
                if (_tableNum == 4)
                {
                    //playsound_right_angry
                    GameManager.GetComponent<GameManager>().getAngry(3f);
                }
            }

                GameManager.GetComponent<GameManager>().endgame = true;
            Debug.Log("end");
            //game end
        }

        //service cooldown
        if (_serviceTimer > 0)
        {
            _serviceTimer -= Time.deltaTime;
        }

        if(_serviceTimer <= 0) {
            if (!_serviceRequired)
            {
                _patienceTimer = _basePatience;
                if (_tableNum == 1)
                {
                    if (_state == 1)
                    {
                        //playsound_uporder_request
                        Debug.Log("Order 1");
                        GameManager.GetComponent<GameManager>().getOrder(1f);
                    }
                    if (_state == 2)
                    {
                        //playsound_upplate_request
                        GameManager.GetComponent<GameManager>().getPlate(1f);
                    }
                    if (_state == 3)
                    {
                        //playsound_upcheck_request
                        GameManager.GetComponent<GameManager>().getCheck(1f);
                    }
                }
                if (_tableNum == 2)
                {
                    if (_state == 1)
                    {
                        //playsound_leftorder_request
                        GameManager.GetComponent<GameManager>().getOrder(2f);
                    }
                    if (_state == 2)
                    {
                        //playsound_leftplate_request
                        GameManager.GetComponent<GameManager>().getPlate(2f);
                    }
                    if (_state == 3)
                    {
                        //playsound_leftcheck_request
                        GameManager.GetComponent<GameManager>().getCheck(2f);
                    }
                }
                if (_tableNum == 3)
                {
                    if (_state == 1)
                    {
                        //playsound_downorder_request
                        GameManager.GetComponent<GameManager>().getOrder(4f);
                    }
                    if (_state == 2)
                    {
                        //playsound_downplate_request
                        GameManager.GetComponent<GameManager>().getPlate(4f);
                    }
                    if (_state == 3)
                    {
                        //playsound_downcheck_request
                        GameManager.GetComponent<GameManager>().getCheck(4f);
                    }
                }
                if (_tableNum == 4)
                {
                    if (_state == 1)
                    {
                        //playsound_rightorder_request
                        GameManager.GetComponent<GameManager>().getOrder(3f);
                    }
                    if (_state == 2)
                    {
                        //playsound_rightplate_request
                        GameManager.GetComponent<GameManager>().getPlate(3f);
                    }
                    if (_state == 3)
                    {
                        //playsound_rightcheck_request
                        GameManager.GetComponent<GameManager>().getCheck(3f);
                    }
                }
            }
            _serviceRequired = true;
            
        }


        


    }

    public void _resetService()
    {
        _serviceCooldown = Random.Range(10, 15);
        _serviceTimer = _serviceCooldown;
        _serviceRequired = false;
        _patienceTimer = _basePatience;
        Debug.Log("Table " + _tableNum + ": " + _patienceTimer);
    }
}
