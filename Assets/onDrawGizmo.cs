using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDrawGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
            if (vertices != null)
            {
                foreach (Vector3 cords in vertices)
                {
                    Gizmos.DrawSphere(cords, 0.1f);
                }
            }
        }   

    }
}
