using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Cylindre : MonoBehaviour
{
    public Material mat;

    public int nbMeridiens;

    public double rayon;

    public double height;

    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        double pi = 3.1416;

        Vector3[] vertices = new Vector3[nbMeridiens * 2 + 2];
        int[] triangles = new int[nbMeridiens * 12];

        for (int i = 0; i < nbMeridiens; ++i)
        {
            double teta = (2 * pi) * i / nbMeridiens;

            vertices[i] = new Vector3((float)(rayon * Cos(teta)), (float)(rayon * Sin(teta)), 0f);
        }

        for (int i = nbMeridiens; i < nbMeridiens * 2; ++i)
        {
            double teta = (2 * pi) * i / nbMeridiens;

            vertices[i] = new Vector3((float)(rayon * Cos(teta)), (float)(rayon * Sin(teta)), (float)height);
        }

        vertices[nbMeridiens * 2] = new Vector3(0, 0, 0);
        vertices[nbMeridiens * 2 + 1] = new Vector3(0, 0, (float)height);

        //tous les bords sauf le dernier
        int compteur = 0;
        for (int i = 0; i < nbMeridiens - 1; i++)
        {
            triangles[compteur] = i;
            triangles[compteur + 1] = i + 1;
            triangles[compteur + 2] = i + nbMeridiens;

            triangles[compteur + 3] = i + nbMeridiens + 1;
            triangles[compteur + 4] = i + nbMeridiens;
            triangles[compteur + 5] = i + 1;
            compteur += 6;
        }
        //dernier bord
        triangles[compteur] = nbMeridiens - 1;
        triangles[compteur + 1] = 0;
        triangles[compteur + 2] = nbMeridiens * 2 - 1;

        triangles[compteur + 3] = nbMeridiens;
        triangles[compteur + 4] = nbMeridiens * 2 - 1;
        triangles[compteur + 5] = 0;
        compteur += 6;

        //bouchons
        for (int i = 0; i < nbMeridiens - 1; ++i)
        {
            triangles[compteur + 1] = i;
            triangles[compteur] = i + 1;
            triangles[compteur + 2] = nbMeridiens * 2;

            triangles[compteur + 3] = i + nbMeridiens;
            triangles[compteur + 4] = i + nbMeridiens + 1;
            triangles[compteur + 5] = nbMeridiens * 2 + 1;
            compteur += 6;
        }

        triangles[compteur + 1] = nbMeridiens - 1;
        triangles[compteur] = 0;
        triangles[compteur + 2] = nbMeridiens * 2;

        triangles[compteur + 4] = nbMeridiens;
        triangles[compteur + 3] = nbMeridiens * 2 - 1;
        triangles[compteur + 5] = nbMeridiens * 2 + 1;


        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }
}
