using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan : MonoBehaviour
{
    public Material mat;

    public int numberX;

    public int numberY;

    public int pas;

    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int size = numberX * 3 * 3 * numberY;
        int numberOfTriangle = numberX * numberY * 2;


        Vector3[] vertices = new Vector3[size];
        int[] triangles = new int[size];


        int compteur = 0;
        for (int i = 0; i < numberY; i++)
        {
            for (int j = 0; j < numberX; ++j)
            {
                vertices[compteur] = new Vector3(j * pas, i * pas, 0);
                compteur++;
            }

        }

        compteur = 0;
        for (int i = 0; i < numberOfTriangle / 2; i++)
        {
            triangles[compteur] = i + 1;
            triangles[compteur + 1] = i;
            triangles[compteur + 2] = i + numberX;

            triangles[compteur + 3] = i + numberX;
            triangles[compteur + 4] = i + numberX + 1;
            triangles[compteur + 5] = i + 1;
            compteur += 6;
        }


        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
