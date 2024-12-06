using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Customer : MonoBehaviour
{
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
        if (_serviceRequired)
        {
            _patienceTimer -= Time.deltaTime;
        }
        else if(_serviceTimer > 0)
        {
            _serviceTimer -= Time.deltaTime;
        }

        if(_serviceTimer <= 0) {
            _serviceRequired = true;
        }

    }

    public void _resetService()
    {
        _serviceTimer = _serviceCooldown;
        _serviceRequired = false;
        _patienceTimer = _basePatience;
    }
}
