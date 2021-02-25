using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTriggerLevel2 : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // If enemy snowman falls into trigger field
        if (other.gameObject.layer == 12)
        {
            text.text = "YOU WIN!";
        }
        // If the player falls into trigger field
        if (other.gameObject.layer == 10)
        {
            text.text = "YOU LOSE!";
        }
        // If a falling rock falls into the trigger field
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<Rigidbody>().mass = 4;
        }
    }
}
