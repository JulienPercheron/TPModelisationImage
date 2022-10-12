using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Sphere : MonoBehaviour
{
    public Material mat;

    public int nbMeridiens;

    public int nbParallele;

    public double rayon;

    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        float pi = 3.1416f;

        vertices = new Vector3[nbMeridiens * (nbParallele - 1) + 2];
        int[] triangles = new int[nbMeridiens * nbParallele * 12];




        for (int iPara = 1; iPara < nbParallele; ++iPara)
        {
            float fi = (pi * iPara) / nbParallele;

            for (int iMeri = 0; iMeri < nbMeridiens; ++iMeri)
            {
                float teta = (2 * pi * iMeri) / nbMeridiens;
                vertices[iMeri * (nbParallele - 1) + iPara - 1] = new Vector3(
                    (float)(rayon * Sin(fi) * Cos(teta)),
                    (float)(rayon * Sin(fi) * Sin(teta)),
                    (float)(rayon * Cos(fi)));
            }
        }

        vertices[nbMeridiens * (nbParallele - 1)] = new Vector3(0, 0, (float)rayon);
        vertices[nbMeridiens * (nbParallele - 1) + 1] = new Vector3(0, 0, (float)-rayon);

        //tous les bords sauf le dernier
        int compteur = 0;
        for (int iPara = 1; iPara < nbParallele - 1; iPara++)
        {
            for (int iMeri = 0; iMeri < nbMeridiens - 1; iMeri++)
            {
                triangles[compteur] = iMeri * (nbParallele - 1) + iPara - 1;
                triangles[compteur + 1] = iMeri * (nbParallele - 1) + iPara;
                triangles[compteur + 2] = (iMeri + 1) * (nbParallele - 1) + iPara - 1;

                triangles[compteur + 4] = (iMeri + 1) * (nbParallele - 1) + iPara;
                triangles[compteur + 3] = iMeri * (nbParallele - 1) + iPara;
                triangles[compteur + 5] = (iMeri + 1) * (nbParallele - 1) + iPara - 1;
                compteur += 6;
                //compteur += 3;
            }
            triangles[compteur] = (nbMeridiens - 1) * (nbParallele - 1) + iPara - 1;
            triangles[compteur + 1] = (nbMeridiens - 1) * (nbParallele - 1) + iPara;
            triangles[compteur + 2] = 0 * (nbParallele - 1) + iPara - 1;

            triangles[compteur + 4] = 0 * (nbParallele - 1) + iPara;
            triangles[compteur + 3] = (nbMeridiens - 1) * (nbParallele - 1) + iPara;
            triangles[compteur + 5] = 0 * (nbParallele - 1) + iPara - 1;
            compteur += 6;
        }

        //bouchon 1
        for (int iMeri = 0; iMeri < nbMeridiens - 1; iMeri++)
        {
            triangles[compteur] = nbMeridiens * (nbParallele - 1);
            triangles[compteur + 1] = iMeri * (nbParallele - 1);
            triangles[compteur + 2] = (iMeri + 1) * (nbParallele - 1);
            compteur += 3;
        }
        triangles[compteur] = nbMeridiens * (nbParallele - 1);
        triangles[compteur + 1] = (nbMeridiens - 1) * (nbParallele - 1);
        triangles[compteur + 2] = 0 * (nbParallele - 1);
        compteur += 3;

        //bouchon 2
        for (int iMeri = 0; iMeri < nbMeridiens - 1; iMeri++)
        {
            triangles[compteur + 1] = nbMeridiens * (nbParallele - 1) + 1;
            triangles[compteur] = iMeri * (nbParallele - 1) + nbParallele - 3;
            triangles[compteur + 2] = (iMeri + 1) * (nbParallele - 1) + nbParallele - 3;
            compteur += 3;
        }
        triangles[compteur + 1] = nbMeridiens * (nbParallele - 1) + 1;
        triangles[compteur] = (nbMeridiens - 1) * (nbParallele - 1) + nbParallele - 3;
        triangles[compteur + 2] = 0 * (nbParallele - 1) + nbParallele - 3;



        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
