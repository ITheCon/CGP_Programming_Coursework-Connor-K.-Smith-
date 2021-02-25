using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMassText : MonoBehaviour
{
    float playerObjectMass;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        playerObjectMass = transform.GetComponentInParent<Rigidbody>().mass;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shows the current mass of the player as a text element UI
        playerObjectMass = transform.GetComponentInParent<Rigidbody>().mass;
        text.text = "Mass = " + playerObjectMass;
    }
}
