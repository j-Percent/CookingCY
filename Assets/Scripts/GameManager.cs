using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class NewBehaviourScript : MonoBehaviour
{

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

    public bool stun;
    public float stunTImer;
    public float stunCooldown;

    // Start is called before the first frame update
    void Start()
    {
        _customerSpawnTimer = UnityEngine.Random.Range(_customerSpawnMin, _customerSpawnMax);
    }

    // Update is called once per frame
    void Update()
    {


        

        if(stunTImer >= 0)
        {
            stunTImer -= Time.deltaTime;
        }

        if (stunTImer <= 0)
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
                    if (customer1._playerState == 0 || customer1._playerState == 2)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (customer1._playerState == 0 || customer1._playerState == 1)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                }

                if (customer1._clickingCount >= 30)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table1 = null;
                }

            }

            else if (table1.GetComponent<Customer>()._isVIP == false) {

            if (Input.GetKeyDown(KeyCode.W))
                {
                    var customer1 = table1.GetComponent<Customer>();
                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 1;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTImer = 2f;
                        Debug.Log("stun");
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
                    }

                    if (customer1._state == 4)
                    {
                        Destroy(customer1);
                        table1 = null;
                    }
                    
                }


                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    var customer1 = table1.GetComponent<Customer>();
                    //change player state based on player 1's interaction
                    if (customer1._playerState == 0)
                    {
                        customer1._playerState = 2;
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 2)
                    {
                        customer1._clickingCount += 1;
                    }
                    else if (customer1._playerState == 1)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        stun = true;
                        stunTImer = 2f;
                        Debug.Log("stun");
                    }

                    if (customer1._clickingCount >= 15)
                    {
                        customer1._playerState = 0;
                        customer1._clickingCount = 0;
                        customer1._state += 1;
                        customer1._resetService();
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

            if (Input.GetKeyDown(KeyCode.A))
            {
                var customer1 = table2.GetComponent<Customer>();
                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 1;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 2)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTImer = 2f;
                    Debug.Log("stun");
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table2 = null;
                }

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                var customer1 = table2.GetComponent<Customer>();
                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 2;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 2)
                {
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTImer = 2f;
                    Debug.Log("stun");
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }
                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table2 = null;
                }

            }
        }

        if (table3 != null && table3.GetComponent<Customer>()._serviceRequired == true && stun == false)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                var customer1 = table3.GetComponent<Customer>();
                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 1;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 2)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTImer = 2f;
                    Debug.Log("stun");
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table3 = null;
                }

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                var customer1 = table3.GetComponent<Customer>();
                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 2;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 2)
                {
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTImer = 2f;
                    Debug.Log("stun");
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }
                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table3 = null;
                }

            }
        }
        if (table4 != null && table4.GetComponent<Customer>()._serviceRequired == true && stun == false)
        {

            if (Input.GetKeyDown(KeyCode.D))
            {
                var customer1 = table4.GetComponent<Customer>();
                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 1;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 2)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTImer = 2f;
                    Debug.Log("stun");
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }

                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table4 = null;
                }

            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                var customer1 = table4.GetComponent<Customer>();
                //change player state based on player 1's interaction
                if (customer1._playerState == 0)
                {
                    customer1._playerState = 2;
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 2)
                {
                    customer1._clickingCount += 1;
                }
                else if (customer1._playerState == 1)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    stun = true;
                    stunTImer = 2f;
                    Debug.Log("stun");
                }

                if (customer1._clickingCount >= 15)
                {
                    customer1._playerState = 0;
                    customer1._clickingCount = 0;
                    customer1._state += 1;
                    customer1._resetService();
                }
                if (customer1._state == 4)
                {
                    Destroy(customer1);
                    table4 = null;
                }

            }
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
            table1 = customer;
            table1 =  Instantiate(customer, transform.position, Quaternion.identity);
        }
        else if (table2 == null)
        {
            //Debug.Log("Table2");
            table2 = customer;
            table2 = Instantiate(customer, transform.position, Quaternion.identity);
        }
        else if (table3 == null)
        {
           //Debug.Log("Table3");
            table3 = customer;
            table3 = Instantiate(customer, transform.position, Quaternion.identity);
        }
        else if (table4 == null)
        {
            //Debug.Log("Table4");
            table4 = customer;
            table4 = Instantiate(customer, transform.position, Quaternion.identity);
        }
        else
        {
            //Debug.Log("Queue");
            customer.GetComponent<Customer>()._state = 0;
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
                table1.GetComponent<Customer>()._state = 1;
                table1.GetComponent<Customer>()._serviceRequired = false;
            }
        }
        else if (table2 == null)
        {
            if (customers.Count > 0)
            {
                Debug.Log("Dequeue Table2");
                table2 = customers.Dequeue();
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
                table4.GetComponent<Customer>()._state = 1;
                table4.GetComponent<Customer>()._serviceRequired = false;
            }
        }
    }

}
