using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermite : MonoBehaviour
{
    private List<Vector3> listePoints = new List<Vector3>();
    private List<Vector3> polynome = new List<Vector3>();



    void Start()
    {
        //CalcHermite(Vector3.zero, new Vector3(2, 0, 0), new Vector3(8,8,0), new Vector3(8,-8,0));

        Bezier();
    }

    void Bezier()
    {
        polynome.Add(new Vector3(-2,-2,0));
        polynome.Add(new Vector3(-1, 1, 0));
        polynome.Add(new Vector3(1, 1, 0));
        polynome.Add(new Vector3(2, -2, 0));
    }

    void CalcHermite(Vector3 p0, Vector3 p1, Vector3 v0, Vector3 v1)
    {
        int pas = 20;
        int t = 0;
        for (t = 0; t <= pas; t+=1)
        {
            Vector3 p = new Vector3();

            float u = (float)t / pas;
            
            float f1 = 2 * Mathf.Pow(u, 3) - 3 * Mathf.Pow(u, 2) + 1;
            float f2 = -2 * Mathf.Pow(u, 3) + 3 * Mathf.Pow(u, 2);
            float f3 = Mathf.Pow(u, 3) - 2 * Mathf.Pow(u, 2) + u;
            float f4 = Mathf.Pow(u, 3) - Mathf.Pow(u, 2);   

            p = f1 * p0 + f2 * p1 + f3 * v0 + f4 * v1;
            

            //p = (2*p0-2*p1+v0+v1)*Mathf.Pow(u, 3)+(-3*p0+3*p1-2*v0-v1)*Mathf.Pow(u, 2) + v0 * u + p0;

            Debug.Log(p);
            listePoints.Add(p);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (listePoints != null)
        {
            if (listePoints.Count > 0)
            {

                for (int i = 0; i < listePoints.Count - 1; i++)
                {
                    //Gizmos.DrawSphere(listePoints[i], 0.1f);
                    Gizmos.DrawLine(listePoints[i], listePoints[i + 1]);
                }


                //Gizmos.DrawSphere(listePoints[listePoints.Count - 1], 0.1f);
                //Gizmos.DrawLine(listePoints[listePoints.Count - 1], listePoints[0]);
            }
        }
        if (polynome != null)
        {
            if (polynome.Count > 0)
            {
                Gizmos.color = Color.green;

                for (int i = 0; i < polynome.Count - 1; i++)
                {
                    Gizmos.DrawLine(polynome[i], polynome[i + 1]);
                }
            }
        }
    }
}
