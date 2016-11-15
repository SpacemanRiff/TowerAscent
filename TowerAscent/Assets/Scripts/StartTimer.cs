using UnityEngine;
using System.Collections;

public class StartTimer : MonoBehaviour
{

    public Timer timer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Climbable")
        {
            timer.StartTime();
            

        }
    }
}
