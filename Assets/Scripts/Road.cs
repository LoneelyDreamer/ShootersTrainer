using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    private bool isAvailable = true;
    private void OnCollisionStay(Collision collision)
    {
        isAvailable = false;     
    }

    private void OnCollisionExit(Collision collision)
    {
        isAvailable = true;
    }
    public bool CheckIsAvailable()
    {
        return isAvailable;
    }

}
