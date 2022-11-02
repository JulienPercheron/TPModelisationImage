using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    private List<Vector3> listePoints = new List<Vector3>();
    private List<Vector3> polynome = new List<Vector3>();

    enum PointChosen { first=0, second=1, third=2, fourth=3, none=4 };

    PointChosen pointChosen = PointChosen.none;
    float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        polynome.Add(new Vector3(-2, -2, 0));
        polynome.Add(new Vector3(-1, 1, 0));
        polynome.Add(new Vector3(1, 1, 0));
        polynome.Add(new Vector3(2, -2, 0));
        CalcBezier();
    }

    private void Update()
    {
        if(Input.GetKeyDown("1")){
            pointChosen = PointChosen.first;
        }
        if (Input.GetKeyDown("2"))
        {
            pointChosen = PointChosen.second;
        }
        if (Input.GetKeyDown("3"))
        {
            pointChosen = PointChosen.third;
        }
        if (Input.GetKeyDown("4"))
        {
            pointChosen = PointChosen.fourth;
        }
        if (Input.GetKeyDown("5"))
        {
            pointChosen = PointChosen.none;
        }

        switch (pointChosen)
        {
            case PointChosen.first:
                polynome[0] = new Vector3(polynome[0].x + Input.GetAxis("Horizontal")*speed*Time.deltaTime, polynome[0].y + Input.GetAxis("Vertical") * speed * Time.deltaTime, polynome[0].z);
                CalcBezier();
                break;
            case PointChosen.second:
                polynome[1] = new Vector3(polynome[1].x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, polynome[1].y + Input.GetAxis("Vertical") * speed * Time.deltaTime, polynome[1].z);
                CalcBezier();
                break;
            case PointChosen.third:
                polynome[2] = new Vector3(polynome[2].x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, polynome[2].y + Input.GetAxis("Vertical") * speed * Time.deltaTime, polynome[2].z);
                CalcBezier();
                break;
            case PointChosen.fourth:
                polynome[3] = new Vector3(polynome[3].x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, polynome[3].y + Input.GetAxis("Vertical") * speed * Time.deltaTime, polynome[3].z);
                CalcBezier();
                break;
            default:
                break;
        }
    }


    void CalcBezier()
    {
        listePoints.Clear();
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

                Gizmos.color = Color.gray;
                switch (pointChosen)
                {
                    case PointChosen.first:
                        Gizmos.DrawSphere(polynome[0], 0.1f);
                        break;
                    case PointChosen.second:
                        Gizmos.DrawSphere(polynome[1], 0.1f);
                        break;
                    case PointChosen.third:
                        Gizmos.DrawSphere(polynome[2], 0.1f);
                        break;
                    case PointChosen.fourth:
                        Gizmos.DrawSphere(polynome[3], 0.1f);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
