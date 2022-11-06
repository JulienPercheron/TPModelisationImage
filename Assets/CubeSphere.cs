using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSphere : MonoBehaviour
{
    private bool isVisible = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gomme")
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else if(other.tag == "Ajout")
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
