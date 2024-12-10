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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        //If there's a service request, patience goes down
        if (_serviceRequired)
        {
            if (_isVIP == true)
                _patienceTimer -= Time.deltaTime * 1.3f;
            else
            {
                _patienceTimer -= Time.deltaTime ;
            }
        }

        //If he has no patience, end game
        if (_patienceTimer <= 0)
        {
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
            }
            _serviceRequired = true;
            
        }


        


    }

    public void _resetService()
    {
        _serviceCooldown = Random.Range(5, 10);
        _serviceTimer = _serviceCooldown;
        _serviceRequired = false;
        _patienceTimer = _basePatience;
    }
}
