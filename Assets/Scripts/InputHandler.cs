using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{ 
    private Conductor conductor;
    private bool holdDrum1 = false;
    private bool holdDrum2 = false;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GetComponent<Conductor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Drum1") <= 0)
        {
            holdDrum1 = false;
        }
        else if(!holdDrum1)
        {
            conductor.HitDrumLeft();
            holdDrum1 = true;
        }

        if (Input.GetAxis("Drum2") <= 0)
        {
            holdDrum2 = false;
        }
        else if (!holdDrum2)
        {
            conductor.HitDrumRight();
            holdDrum2 = true;
        }
    }
}
