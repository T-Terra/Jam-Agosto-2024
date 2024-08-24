using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        PlayerInputs_();
    }

    private void PlayerInputs_()
    {
        if (Input.GetMouseButton(0))
        {
            print("Atirou!");
        }
    }
}
