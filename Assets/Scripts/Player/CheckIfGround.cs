using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGround : MonoBehaviour
{
    public bool IsGround = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.position.y<transform.parent.transform.position.y)
        IsGround= true;
    }
    private void OnTriggerExit(Collider other)
    {
        IsGround = false;
    }
}
