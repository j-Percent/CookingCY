using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject table1;
    public GameObject table2;
    public GameObject table3;
    public GameObject table4;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(table1.GetComponent<Customer>()._serviceRequired == true  )
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

            }
        }
    }
}
