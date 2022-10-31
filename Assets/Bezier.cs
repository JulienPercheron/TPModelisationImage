using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    private List<Vector3> listePoints = new List<Vector3>();
    private List<Vector3> polynome = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        CalcBezier();
    }

    void CalcBezier()
    {

        polynome.Add(new Vector3(-2, -2, 0));
        polynome.Add(new Vector3(-1, 1, 0));
        polynome.Add(new Vector3(1, 1, 0));
        polynome.Add(new Vector3(2, -2, 0));

        int pas = 100;
        for (int u = 0; u <= pas; u += 1)
        {
            Vector3 p = Vector3.zero;

            float t = (float)u / pas;
            int n = polynome.Count-1;

            for (int i = 0; i < polynome.Count; i++)
            { 
                p += polynome[i] * (Facto(n) / (Facto(i) * Facto(n-i)) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i));
            }
            listePoints.Add(p);
        }
    }

    int Facto(int num)
    {
        if (num <= 1)
            return 1;
        return Facto(num - 1) * num;
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
