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
    public float _patience;
    public float _basePatience;

    // true - is VIP; false - normal customer
    public bool _isVIP;

    // true - need service, patience starts reducing; false - don't need service, reduce service cooldown
    public bool _serviceRequired;
    public float _serviceCooldown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
