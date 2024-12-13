using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GameManager : MonoBehaviour
{

    public float timer=0;
    public bool ended = false;

    public Queue<GameObject> customers = new Queue<GameObject>();

    public GameObject customer;
    public float _vipChance;
    public float _customerSpawnTimer;
    public float _customerSpawnMin;
    public float _customerSpawnMax;

    public GameObject table1;
    public GameObject table2;
    public GameObject table3;
    public GameObject table4;

    #region Jerry AudioReferences
    public FMODUnity.EventReference jerryOrder;
    FMOD.Studio.EventInstance jOrderInstance;

    public FMODUnity.EventReference jerryPlate;
    FMOD.Studio.EventInstance jPlateInstance;

    public FMODUnity.EventReference jerryCheck;
    FMOD.Studio.EventInstance jCheckInstance;

    public FMODUnity.EventReference jerryDone;
    FMOD.Studio.EventInstance jDoneInstance;
    FMOD.Studio.PARAMETER_ID jDoneTableNumber;
    #endregion

    #region Leo AudioReferences
    public FMODUnity.EventReference leoOrder;
    FMOD.Studio.EventInstance lOrderInstance;

    public FMODUnity.EventReference leoPlate;
    FMOD.Studio.EventInstance lPlateInstance;

    public FMODUnity.EventReference leoCheck;
    FMOD.Studio.EventInstance lCheckInstance;

    public FMODUnity.EventReference leoDone;
    FMOD.Studio.EventInstance lDoneInstance;
    FMOD.Studio.PARAMETER_ID lDoneTableNumber;
    #endregion

    #region Table AudioReferences
    public FMODUnity.EventReference stunReference;
    FMOD.Studio.EventInstance stunInstance;

    public FMODUnity.EventReference tableAngry;
    FMOD.Studio.EventInstance angryInstance;
    FMOD.Studio.PARAMETER_ID angryTableNumber;

    public FMODUnity.EventReference tableCheck;
    FMOD.Studio.EventInstance checkInstance;
    FMOD.Studio.PARAMETER_ID checkTableNumber;

    public FMODUnity.EventReference tableLeave;
    FMOD.Studio.EventInstance leaveInstance;
    FMOD.Studio.PARAMETER_ID leaveTableNumber;

    public FMODUnity.EventReference tableOrder;
    FMOD.Studio.EventInstance orderInstance;
    FMOD.Studio.PARAMETER_ID orderTableNumber;

    public FMODUnity.EventReference tablePlate;
    FMOD.Studio.EventInstance plateInstance;
    FMOD.Studio.PARAMETER_ID plateTableNumber;

    public FMODUnity.EventReference tableWait;
    FMOD.Studio.EventInstance waitInstance;
    FMOD.Studio.PARAMETER_ID waitTableNumber;

    public FMODUnity.EventReference vipAngry;
    FMOD.Studio.EventInstance vipAngryInstance;

    public FMODUnity.EventReference vipLeave;
    FMOD.Studio.EventInstance vipLeaveInstance;

    public FMODUnity.EventReference vipTable;
    FMOD.Studio.EventInstance vipTableInstance;
    FMOD.Studio.PARAMETER_ID vipAtTableNumber;

    public FMODUnity.EventReference finalRank;
    FMOD.Studio.EventInstance finalRankInstance;
    FMOD.Studio.PARAMETER_ID finalRankScore;

    public FMODUnity.EventReference BGM;
    FMOD.Studio.EventInstance BGMInstance;

    #endregion


    public bool endgame = false;

    public KeyCode player1input;
    public KeyCode player2input;

    public bool stun;
    public float stunTimer;
    public float stunCooldown;

    // Start is called before the first frame update
    void Start()
    {
        

        #region FMOD Instantiation
        jOrderInstance = FMODUnity.RuntimeManager.CreateInstance(jerryOrder);
        jPlateInstance = FMODUnity.RuntimeManager.CreateInstance(jerryPlate);
        jCheckInstance = FMODUnity.RuntimeManager.CreateInstance(jerryCheck);
        jDoneInstance = FMODUnity.RuntimeManager.CreateInstance(jerryDone);
        FMOD.Studio.EventDescription jDoneEventDescription;
        jDoneInstance.getDescription(out jDoneEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION jDoneParameterDescription;
        jDoneEventDescription.getParameterDescriptionByName("TableNumber", out jDoneParameterDescription);
        jDoneTableNumber = jDoneParameterDescription.id;

        lOrderInstance = FMODUnity.RuntimeManager.CreateInstance(leoOrder);
        lPlateInstance = FMODUnity.RuntimeManager.CreateInstance(leoPlate);
        lCheckInstance = FMODUnity.RuntimeManager.CreateInstance(leoCheck);
        lDoneInstance = FMODUnity.RuntimeManager.CreateInstance(leoDone);
        FMOD.Studio.EventDescription lDoneEventDescription;
        lDoneInstance.getDescription(out lDoneEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION lDoneParameterDescription;
        lDoneEventDescription.getParameterDescriptionByName("TableNumber", out lDoneParameterDescription);
        lDoneTableNumber = lDoneParameterDescription.id;

        stunInstance = FMODUnity.RuntimeManager.CreateInstance(stunReference);

        BGMInstance = FMODUnity.RuntimeManager.CreateInstance(BGM);

        angryInstance = FMODUnity.RuntimeManager.CreateInstance(tableAngry);
        FMOD.Studio.EventDescription angryEventDescription;
        angryInstance.getDescription(out angryEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION angryParameterDescription;
        angryEventDescription.getParameterDescriptionByName("TableNumber", out angryParameterDescription);
        angryTableNumber = angryParameterDescription.id;

        checkInstance = FMODUnity.RuntimeManager.CreateInstance(tableCheck);
        FMOD.Studio.EventDescription checkEventDescription;
        checkInstance.getDescription(out checkEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION checkParameterDescription;
        checkEventDescription.getParameterDescriptionByName("TableNumber", out checkParameterDescription);
        checkTableNumber = checkParameterDescription.id;

        leaveInstance = FMODUnity.RuntimeManager.CreateInstance(tableLeave);
        FMOD.Studio.EventDescription leaveEventDescription;
        leaveInstance.getDescription(out leaveEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION leaveParameterDescription;
        leaveEventDescription.getParameterDescriptionByName("TableNumber", out leaveParameterDescription);
        leaveTableNumber = leaveParameterDescription.id;

        orderInstance = FMODUnity.RuntimeManager.CreateInstance(tableOrder);
        FMOD.Studio.EventDescription orderEventDescription;
        orderInstance.getDescription(out orderEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION orderParameterDescription;
        orderEventDescription.getParameterDescriptionByName("TableNumber", out orderParameterDescription);
        orderTableNumber = orderParameterDescription.id;

        plateInstance = FMODUnity.RuntimeManager.CreateInstance(tablePlate);
        FMOD.Studio.EventDescription plateEventDescription;
        plateInstance.getDescription(out plateEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION plateParameterDescription;
        plateEventDescription.getParameterDescriptionByName("TableNumber", out plateParameterDescription);
        plateTableNumber = plateParameterDescription.id;

        waitInstance = FMODUnity.RuntimeManager.CreateInstance(tableWait);
        FMOD.Studio.EventDescription waitEventDescription;
        waitInstance.getDescription(out waitEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION waitParameterDescription;
        waitEventDescription.getParameterDescriptionByName("TableNumber", out waitParameterDescription);
        waitTableNumber = waitParameterDescription.id;

        vipAngryInstance = FMODUnity.RuntimeManager.CreateInstance(vipAngry);

        vipLeaveInstance = FMODUnity.RuntimeManager.CreateInstance(vipLeave);

        vipTableInstance = FMODUnity.RuntimeManager.CreateInstance(vipTable);
        FMOD.Studio.EventDescription vipAtEventDescription;
        vipTableInstance.getDescription(out vipAtEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION vipAtParameterDescription;
        vipAtEventDescription.getParameterDescriptionByName("TableNumber", out vipAtParameterDescription);
        vipAtTableNumber = vipAtParameterDescription.id;

        finalRankInstance = FMODUnity.RuntimeManager.CreateInstance(finalRank);
        FMOD.Studio.EventDescription rankEventDescription;
        finalRankInstance.getDescription(out rankEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION rankParameterDescription;
        rankEventDescription.getParameterDescriptionByName("Rank", out rankParameterDescription);
        finalRankScore = rankParameterDescription.id;
        #endregion

        _customerSpawnTimer = UnityEngine.Random.Range(_customerSpawnMin, _customerSpawnMax);
        if (BGMInstance.isValid())
        {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            BGMInstance.getPlaybackState(out playbackState);
            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                BGMInstance.start();
            }
        }
    }

    #region AudioGetterMethods
    public void getAngry(float tableNum)
    {
        angryInstance.setParameterByID(angryTableNumber, tableNum);
        if (angryInstance.isValid())
        {
            angryInstance.start();
        }
    }
    public void getOrder(float tableNum)
    {
        orderInstance.setParameterByID(orderTableNumber, tableNum);
        if (orderInstance.isValid())
        {
            orderInstance.start();
        }
    }
    public void getPlate(float tableNum)
    {
        plateInstance.setParameterByID(plateTableNumber, tableNum);
        if (plateInstance.isValid())
        {
            plateInstance.start();
        }
    }
    public void getCheck(float tableNum)
    {
        checkInstance.setParameterByID(orderTableNumber, tableNum);
        if (checkInstance.isValid())
        {
            checkInstance.start();
        }
    }
    public void getVIPAngry()
    {
        if (vipAngryInstance.isValid())
        {
            vipAngryInstance.start();
        }
    }
    public void getJDone(int tableNum)
    {
        jDoneInstance.setParameterByID(jDoneTableNumber, tableNum);
        if (jDoneInstance.isValid())
        {
            jDoneInstance.start();
        }
    }
    public void getLDone(int tableNum)
    {
        lDoneInstance.setParameterByID(lDoneTableNumber, tableNum);
        if (lDoneInstance.isValid())
        {
            lDoneInstance.start();
        }
    }

    public void getEnd (float rank)
    {
        finalRankInstance.setParameterByID(finalRankScore, rank);
        if (finalRankInstance.isValid())
        {
            finalRankInstance.start();
        }
    }
    #endregion

    void keycheck(KeyCode playerinput)
    {

        if(playerinput == KeyCode.W || playerinput == KeyCode.UpArrow)
        {
            table1.GetComponent<Customer>()._playerState = 0;
            table1.GetComponent<Customer>()._clickingCount = 0;
        }
        if (playerinput == KeyCode.A || playerinput == KeyCode.LeftArrow)
        {
            table2.GetComponent<Customer>()._playerState = 0;
            table2.GetComponent<Customer>()._clickingCount = 0;
        }
        if (playerinput == KeyCode.S || playerinput == KeyCode.DownArrow)
        {
            table3.GetComponent<Customer>()._playerState = 0;
            table3.GetComponent<Customer>()._clickingCount = 0;
        }
        if (playerinput == KeyCode.D || playerinput == KeyCode.RightArrow)
        {
            table4.GetComponent<Customer>()._playerState = 0;
            table4.GetComponent<Customer>()._clickingCount = 0;
        }

    }



    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (endgame == true && ended == false)
        {
            ended = true;
            if(timer <= 120)
            {
                //f
                getEnd(6);
            }
            else if(timer <= 150)
            {
                //d
                getEnd(5);
            }
            else if (timer <= 180)
            {
                //c
                getEnd(4);
            }
            else if (timer <= 210)
            {
                //b
                getEnd(3);
            }
            else if (timer <= 240)
            {
                //a
                getEnd(2);
            }
            else if(timer <= 270)
            {
                //s
                getEnd(1);
            }
        }

        if (table1 != null && table1.GetComponent<Customer>()._patienceTimer <= 30)
        {
            //playsound_uplate
            waitInstance.setParameterByID(waitTableNumber, 1f);
            if (waitInstance.isValid())
            {
                FMOD.Studio.PLAYBACK_STATE playbackState;
                waitInstance.getPlaybackState(out playbackState);
                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    waitInstance.start();
                }
            }
        }
        if (table2 != null && table2.GetComponent<Customer>()._patienceTimer <= 30)
        {
            //playsound_leftlate
            waitInstance.setParameterByID(waitTableNumber, 2f);
            if (waitInstance.isValid())
            {
                FMOD.Studio.PLAYBACK_STATE playbackState;
                waitInstance.getPlaybackState(out playbackState);
                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    waitInstance.start();
                }
            }
        }
        if (table3 != null && table3.GetComponent<Customer>()._patienceTimer <= 30)
        {
            //playsound_downlate
            waitInstance.setParameterByID(waitTableNumber, 4f);
            if (waitInstance.isValid())
            {
                FMOD.Studio.PLAYBACK_STATE playbackState;
                waitInstance.getPlaybackState(out playbackState);
                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    waitInstance.start();
                }
            }
        }
        if (table4 != null && table4.GetComponent<Customer>()._patienceTimer <= 30)
        {
            //playsound_rightlate
            waitInstance.setParameterByID(waitTableNumber, 3f);
            if (waitInstance.isValid())
            {
                FMOD.Studio.PLAYBACK_STATE playbackState;
                waitInstance.getPlaybackState(out playbackState);
                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    waitInstance.start();
                }
            }
        }



        if (stunTimer >= 0)
        {
            stunTimer -= Time.deltaTime;
        }

        if (stunTimer <= 0)
        {
            stun = false;
        }

        if (table1 != null && table1.GetComponent<Customer>()._serviceRequired == true && stun == false)
        {

            if(table1.GetComponent<Customer>()._isVIP == true)
            {
                var customer1 = table1.GetComponent<Customer>();

                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (player1input != KeyCode.W)
                    {
                        keycheck(player1input);
                        player1input = KeyCode.W;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 2)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    if(customer1._state == 1)
                    {
                        //playsound_up1order
                        if (lOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up1plate
                        if (lPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up1check
                        if (lCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lCheckInstance.start();
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (player2input != KeyCode.UpArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.UpArrow;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 1)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        if (jOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up2plate
                        if (jPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up2check
                        if (jCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jCheckInstance.start();
                            }
                        }
                    }
                }

                if (customer1._clickingCount >= 30)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                   
                    customer1._state += 1;
                    customer1._resetService();
                    getJDone(1);
                    getLDone(1);
                }

                if (customer1._state == 4)
                {
                    //playsound_upVIP_leave
                    if (vipLeaveInstance.isValid())
                    {
                        vipLeaveInstance.start();
                    }
                    Destroy(customer1);
                    table1 = null;
                }

            }

            else if (table1.GetComponent<Customer>()._isVIP == false) {

                if (Input.GetKeyDown(KeyCode.W))
                    {
                        var customer1 = table1.GetComponent<Customer>();

                        if(player1input != KeyCode.W)
                        {
                            keycheck(player1input);
                            player1input = KeyCode.W;
                        }

                        //change player state based on player 1's interaction
                        if (customer1._playerState == 0)
                        {
                            customer1._playerState = 1;
                            customer1._clickingCount += 1;
                        }
                        else if (customer1._playerState == 1)
                        {
                            customer1._clickingCount += 1;
                            if (customer1._state == 1)
                            {
                                if (lOrderInstance.isValid())
                                {
                                    FMOD.Studio.PLAYBACK_STATE playbackState;
                                    lOrderInstance.getPlaybackState(out playbackState);
                                    if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                    {
                                        lOrderInstance.start();
                                    }
                                }
                            }
                            if (customer1._state == 2)
                            {
                                //playsound_up1plate
                                if (lPlateInstance.isValid())
                                {
                                    FMOD.Studio.PLAYBACK_STATE playbackState;
                                    lPlateInstance.getPlaybackState(out playbackState);
                                    if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                    {
                                        lPlateInstance.start();
                                    }
                                }
                            }
                            if (customer1._state == 3)
                            {
                                //playsound_up1check
                                if (lCheckInstance.isValid())
                                {
                                    FMOD.Studio.PLAYBACK_STATE playbackState;
                                    lCheckInstance.getPlaybackState(out playbackState);
                                    if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                    {
                                        lCheckInstance.start();
                                    }
                                }
                            }
                        }
                        else if (customer1._playerState == 2)
                        {
                            customer1._playerState = 0;
                            customer1._clickingCount = 0;
                            stun = true;
                            stunTimer = 2f;
                            Debug.Log("stun");
                            if (stunInstance.isValid())
                            {
                                stunInstance.start();
                            }
                        }

                        if (customer1._clickingCount >= 15)
                        {
                            customer1._playerState = 0;
                            customer1._clickingCount = 0;
                            customer1._state += 1;
                            customer1._resetService();
                        getLDone(1);
                        }

                        if (customer1._state == 4)
                        {
                            //playsound_upleave
                            leaveInstance.setParameterByID(leaveTableNumber, 1f);
                            if (leaveInstance.isValid())
                            {
                                leaveInstance.start();
                            }
                            Destroy(customer1);
                            table1 = null;
                        }
                    
                    }


                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    var customer1 = table1.GetComponent<Customer>();

                    if (player2input != KeyCode.UpArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.UpArrow;
                    }
                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._clickingCount += 1;
                        if (customer1._state == 1)
                        {
                            if (jOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up2plate
                            if (jPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up2check
                            if (jCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jCheckInstance.start();
                                }
                            }
                        }
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTimer = 2f;
                        Debug.Log("stun");
                        if (stunInstance.isValid())
                        {
                            stunInstance.start();
                        }
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                        getJDone(1);
                    }
                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table1 = null;
                    }
                }
            }
        }

        if (table2 != null && table2.GetComponent<Customer>()._serviceRequired == true && stun == false)
        {

            if (table2.GetComponent<Customer>()._isVIP == true)
            {
                var customer1 = table2.GetComponent<Customer>();

                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (player1input != KeyCode.A)
                    {
                        keycheck(player1input);
                        player1input = KeyCode.A;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 2)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        //playsound_up1order
                        if (lOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up1plate
                        if (lPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up1check
                        if (lCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lCheckInstance.start();
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (player2input != KeyCode.LeftArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.LeftArrow;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 1)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        //playsound_up2order
                        if (jOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up2plate
                        if (jPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up2check
                        if (jCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jCheckInstance.start();
                            }
                        }
                    }
                }

                if (customer1._clickingCount >= 30)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                    getLDone(2);
                    getJDone(2);
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table2 = null;
                    //playsound_upVIP_leave
                    if (vipLeaveInstance.isValid())
                    {
                        vipLeaveInstance.start();
                    }
                }

            }

            else if (table2.GetComponent<Customer>()._isVIP == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    var customer1 = table2.GetComponent<Customer>();
                    if (player1input != KeyCode.A)
                    {
                        keycheck(player1input);
                        player1input = KeyCode.A;
                    }

                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._clickingCount += 1; if (customer1._state == 1)
                        {
                            //playsound_up1order
                            if (lOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up1plate
                            if (lPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up1check
                            if (lCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lCheckInstance.start();
                                }
                            }
                        }
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTimer = 2f;
                        Debug.Log("stun");
                        if (stunInstance.isValid())
                        {
                            stunInstance.start();
                        }
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                        getLDone(2);
                    }

                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table2 = null;
                        leaveInstance.setParameterByID(leaveTableNumber, 2f);
                        if (leaveInstance.isValid())
                        {
                            leaveInstance.start();
                        }
                    }

                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    var customer1 = table2.GetComponent<Customer>();
                    if (player2input != KeyCode.LeftArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.LeftArrow;
                    }
                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._clickingCount += 1; 
                        if (customer1._state == 1)
                        {
                            //playsound_up2order
                            if (jOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up2plate
                            if (jPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up2check
                            if (jCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jCheckInstance.start();
                                }
                            }
                        }
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTimer = 2f;
                        Debug.Log("stun");
                        if (stunInstance.isValid())
                        {
                            stunInstance.start();
                        }
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                        getJDone(2);
                    }
                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table2 = null;
                        leaveInstance.setParameterByID(leaveTableNumber, 2f);
                        if (leaveInstance.isValid())
                        {
                            leaveInstance.start();
                        }
                    }

                }
            }
        }

        if (table3 != null && table3.GetComponent<Customer>()._serviceRequired == true && stun == false)
        {

            if (table3.GetComponent<Customer>()._isVIP == true)
            {
                var customer1 = table3.GetComponent<Customer>();

                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (player1input != KeyCode.S)
                    {
                        keycheck(player1input);
                        player1input = KeyCode.S;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 2)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        //playsound_up1order
                        if (lOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up1plate
                        if (lPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up1check
                        if (lCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lCheckInstance.start();
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (player2input != KeyCode.DownArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.DownArrow;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 1)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        //playsound_up2order
                        if (jOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up2plate
                        if (jPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up2check
                        if (jCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jCheckInstance.start();
                            }
                        }
                    }
                }

                if (customer1._clickingCount >= 30)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                    getLDone(4);
                    getJDone(4);
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table3 = null;
                    //playsound_upVIP_leave
                    if (vipLeaveInstance.isValid())
                    {
                        vipLeaveInstance.start();
                    }
                }

            }

            else if (table3.GetComponent<Customer>()._isVIP == false)
            {

                if (Input.GetKeyDown(KeyCode.S))
            {
                var customer1 = table3.GetComponent<Customer>();

                if (player1input != KeyCode.S)
                {
                    keycheck(player1input);
                    player1input = KeyCode.S;
                }
                

                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 1;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._clickingCount += 1; 
                        if (customer1._state == 1)
                        {
                            //playsound_up1order
                            if (lOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up1plate
                            if (lPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up1check
                            if (lCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lCheckInstance.start();
                                }
                            }
                        }
                    }
                else if (customer1._playerState == 2)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTimer = 2f;
                    Debug.Log("stun");
                    if (stunInstance.isValid())
                    {
                        stunInstance.start();
                    }
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                        getLDone(4);
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table3 = null;
                    leaveInstance.setParameterByID(leaveTableNumber, 4f);
                    if (leaveInstance.isValid())
                    {
                        leaveInstance.start();
                    }
                }
            }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {

                    var customer1 = table3.GetComponent<Customer>();
                    if (player2input != KeyCode.DownArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.DownArrow;
                    }
                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._clickingCount += 1;
                        if (customer1._state == 1)
                        {
                            //playsound_up2order
                            if (jOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up2plate
                            if (jPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up2check
                            if (jCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jCheckInstance.start();
                                }
                            }
                        }
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTimer = 2f;
                        Debug.Log("stun");
                        if (stunInstance.isValid())
                        {
                            stunInstance.start();
                        }
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                        getJDone(4);
                    }
                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table3 = null;
                        leaveInstance.setParameterByID(leaveTableNumber, 4f);
                        if (leaveInstance.isValid())
                        {
                            leaveInstance.start();
                        }
                    }
                }
            }
        }

        if (table4 != null && table4.GetComponent<Customer>()._serviceRequired == true && stun == false)
        {
            if (table4.GetComponent<Customer>()._isVIP == true)
            {
                var customer1 = table4.GetComponent<Customer>();

                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (player1input != KeyCode.D)
                    {
                        keycheck(player1input);
                        player1input = KeyCode.D;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 2)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        //playsound_up1order
                        if (lOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up1plate
                        if (lPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up1check
                        if (lCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            lCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                lCheckInstance.start();
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (player2input != KeyCode.RightArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.RightArrow;
                    }
                    if (customer1._playerState == 0 || customer1._playerState == 1)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    if (customer1._state == 1)
                    {
                        if (jOrderInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jOrderInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jOrderInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 2)
                    {
                        //playsound_up2plate
                        if (jPlateInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jPlateInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jPlateInstance.start();
                            }
                        }
                    }
                    if (customer1._state == 3)
                    {
                        //playsound_up2check
                        if (jCheckInstance.isValid())
                        {
                            FMOD.Studio.PLAYBACK_STATE playbackState;
                            jCheckInstance.getPlaybackState(out playbackState);
                            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                            {
                                jCheckInstance.start();
                            }
                        }
                    }
                }

                if (customer1._clickingCount >= 30)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                    getJDone(3);
                    getLDone(3);
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table4 = null;
                    if (vipLeaveInstance.isValid())
                    {
                        vipLeaveInstance.start();
                    }
                }

            }


            else if (table4.GetComponent<Customer>()._isVIP == false)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    var customer1 = table4.GetComponent<Customer>();
                    if (player1input != KeyCode.D)
                    {
                        keycheck(player1input);
                        player1input = KeyCode.D;
                    }

                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._clickingCount += 1; 
                        if (customer1._state == 1)
                        {
                            if (lOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up1plate
                            if (lPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up1check
                            if (lCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                lCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    lCheckInstance.start();
                                }
                            }
                        }
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTimer = 2f;
                        Debug.Log("stun");
                        if (stunInstance.isValid())
                        {
                            stunInstance.start();
                        }
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                        getLDone(3);
                    }

                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table4 = null;
                        leaveInstance.setParameterByID(leaveTableNumber, 3f);
                        if (leaveInstance.isValid())
                        {
                            leaveInstance.start();
                        }
                    }

                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                    var customer1 = table4.GetComponent<Customer>();
                    if (player2input != KeyCode.RightArrow)
                    {
                        keycheck(player2input);
                        player2input = KeyCode.RightArrow;
                    }

                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._clickingCount += 1; 
                        if (customer1._state == 1)
                        {
                            if (jOrderInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jOrderInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jOrderInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 2)
                        {
                            //playsound_up2plate
                            if (jPlateInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jPlateInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jPlateInstance.start();
                                }
                            }
                        }
                        if (customer1._state == 3)
                        {
                            //playsound_up2check
                            if (jCheckInstance.isValid())
                            {
                                FMOD.Studio.PLAYBACK_STATE playbackState;
                                jCheckInstance.getPlaybackState(out playbackState);
                                if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                                {
                                    jCheckInstance.start();
                                }
                            }
                        }
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTimer = 2f;
                        Debug.Log("stun");
                        if (stunInstance.isValid())
                        {
                            stunInstance.start();
                        }
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                        getJDone(3);
                    }
                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table4 = null;
                        leaveInstance.setParameterByID(leaveTableNumber, 3f);
                        if (leaveInstance.isValid())
                        {
                            leaveInstance.start();
                        }
                    }

                } }
        }

        //Cleanup
        _customerSpawnTimer -= Time.deltaTime;
        if (_customerSpawnTimer <= 0)
        {
            _addNewCustomer();
            _customerSpawnTimer = UnityEngine.Random.Range(_customerSpawnMin, _customerSpawnMax);
        }
        _seatWaitingCustomer();
    }

    public void _addNewCustomer()
    {
        float isCustomerVIP = UnityEngine.Random.Range(0.0f, 1.0f);
        if (isCustomerVIP <= _vipChance)
        {
            customer.GetComponent<Customer>()._isVIP = true;
        }
        else
        {
            customer.GetComponent<Customer>()._isVIP = false;
        }

        if (table1 == null)
        {
            //Debug.Log("Table1");
            customer.GetComponent<Customer>()._tableNum = 1;
            table1 = customer;
            table1 =  Instantiate(customer, transform.position, Quaternion.identity);
            if (customer.GetComponent<Customer>()._isVIP == true)
            {
                vipTableInstance.setParameterByID(vipAtTableNumber, 1f);
                if (vipTableInstance.isValid())
                {
                    vipTableInstance.start();
                }
            }
        }
        else if (table2 == null)
        {
            //Debug.Log("Table2");
            customer.GetComponent<Customer>()._tableNum = 2;
            table2 = customer;
            table2 = Instantiate(customer, transform.position, Quaternion.identity);
            if (customer.GetComponent<Customer>()._isVIP == true)
            {
                vipTableInstance.setParameterByID(vipAtTableNumber, 2f);
                if (vipTableInstance.isValid())
                {
                    vipTableInstance.start();
                }
            }
        }
        else if (table3 == null)
        {
            //Debug.Log("Table3");
            customer.GetComponent<Customer>()._tableNum = 3;
            table3 = customer;
            table3 = Instantiate(customer, transform.position, Quaternion.identity);
            if (customer.GetComponent<Customer>()._isVIP == true)
            {
                vipTableInstance.setParameterByID(vipAtTableNumber, 4f);
                if (vipTableInstance.isValid())
                {
                    vipTableInstance.start();
                }
            }
        }
        else if (table4 == null)
        {
            //Debug.Log("Table4");
            customer.GetComponent<Customer>()._tableNum = 4;
            table4 = customer;
            table4 = Instantiate(customer, transform.position, Quaternion.identity);
            if (customer.GetComponent<Customer>()._isVIP == true)
            {
                vipTableInstance.setParameterByID(vipAtTableNumber, 3f);
                if (vipTableInstance.isValid())
                {
                    vipTableInstance.start();
                }
            }
        }
        else
        {
            //Debug.Log("Queue");
            customer.GetComponent<Customer>()._state = 0;
            customer.GetComponent<Customer>()._tableNum = 0;
            customer.GetComponent<Customer>()._serviceRequired = true;
            GameObject queueCustomer = Instantiate(customer, transform.position, Quaternion.identity);
            customers.Enqueue(queueCustomer);
        }
        customer.GetComponent<Customer>()._state = 1;
        customer.GetComponent<Customer>()._serviceRequired = false;
    }

    public void _seatWaitingCustomer()
    {
        if (table1 == null)
        {
            if (customers.Count > 0)
            {
                Debug.Log("Dequeue Table1");
                table1 = customers.Dequeue();
                table1.GetComponent<Customer>()._tableNum = 1;
                table1.GetComponent<Customer>()._state = 1;
                table1.GetComponent<Customer>()._serviceRequired = false;
                if (table1.GetComponent<Customer>()._isVIP == true)
                {
                    //playsound_VIPupenter
                }
                else if (table1.GetComponent<Customer>()._isVIP == false)
                {
                    //playsound_VIPupenter
                }
            }
        }
        else if (table2 == null)
        {
            if (customers.Count > 0)
            {
                Debug.Log("Dequeue Table2");
                table2 = customers.Dequeue();
                table2.GetComponent<Customer>()._tableNum = 2;
                table2.GetComponent<Customer>()._state = 1;
                table2.GetComponent<Customer>()._serviceRequired = false;
            }
        }
        else if (table3 == null)
        {
            if (customers.Count > 0)
            {
                Debug.Log("Dequeue Table3");
                table3 = customers.Dequeue();
                table3.GetComponent<Customer>()._tableNum = 3;
                table3.GetComponent<Customer>()._state = 1;
                table3.GetComponent<Customer>()._serviceRequired = false;
            }
        }
        else if (table4 == null)
        {
            if (customers.Count > 0)
            {
                Debug.Log("Dequeue Table4");
                table4 = customers.Dequeue();
                table4.GetComponent<Customer>()._tableNum = 4;
                table4.GetComponent<Customer>()._state = 1;
                table4.GetComponent<Customer>()._serviceRequired = false;
            }
        }
    }

    public void PlaySound(FMODUnity.EventReference eventReference)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventReference);
    }

        bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

}
