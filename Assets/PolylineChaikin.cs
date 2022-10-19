using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolylineChaikin : MonoBehaviour
{

    private List<Vector3> listePoints;
    public int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        listePoints = new List<Vector3>();

        listePoints.Add(new Vector3(0, 0, 0));
        listePoints.Add(new Vector3(0, 1, 0));
        listePoints.Add(new Vector3(1, 2, 0));
        listePoints.Add(new Vector3(2, 2, 0));
        listePoints.Add(new Vector3(2, 3, 0));
        listePoints.Add(new Vector3(3, 3, 0));
        listePoints.Add(new Vector3(3, 0, 0));
        listePoints.Add(new Vector3(2, 0, 0));
        listePoints.Add(new Vector3(1, 0, 0));

    }

    private void Update()
    {
        counter++;

        if (counter >= 500)
        {
            Chaikin();
            counter = 0;
        }
    }

    private void Chaikin()
    {
        List<Vector3> output = new List<Vector3>(); 
        for (int i = 0; i < listePoints.Count; i++)
        {
            Vector3 p0 = listePoints[i];
            Vector3 p1;
            if (i == listePoints.Count - 1)
            {
                p1 = listePoints[0];
            }
            else
            {
                p1 = listePoints[i + 1];
            }


            Vector3 Q = new Vector3(0.75f * p0.x + 0.25f * p1.x, 0.75f * p0.y + 0.25f * p1.y, 0.75f * p0.z + 0.25f * p1.z);
            Vector3 R = new Vector3(0.25f * p0.x + 0.75f * p1.x, 0.25f * p0.y + 0.75f * p1.y, 0.25f * p0.z + 0.75f * p1.z);
            output.Add(Q);
            output.Add(R);
        }
        listePoints = output;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(listePoints != null)
        {
            if (listePoints.Count > 0)
            {

                for (int i = 0; i < listePoints.Count - 1; i++)
                {
                    //Gizmos.DrawSphere(listePoints[i], 0.1f);
                    Gizmos.DrawLine(listePoints[i], listePoints[i + 1]);
                }

                //Gizmos.DrawSphere(listePoints[listePoints.Count - 1], 0.1f);
                Gizmos.DrawLine(listePoints[listePoints.Count - 1], listePoints[0]);
            }
        }
    }
        
}
