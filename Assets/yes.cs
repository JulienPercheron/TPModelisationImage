using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class yes : MonoBehaviour
{
    public Material mat;

    public int numberX;

    public int numberY;

    public int nbMeridiens;

    public int nbParallele;

    public double rayon;

    public double height;

    public int pas;

    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        Sphere();
        
    }

    void Sphere()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        float pi = 3.1416f;

        vertices = new Vector3[nbMeridiens * (nbParallele -1)+2];
        int[] triangles = new int[nbMeridiens * nbParallele *12];

        


        for (int iPara = 1; iPara < nbParallele; ++iPara)
        {
            float fi = (pi * iPara) / nbParallele;

            for (int iMeri=0; iMeri < nbMeridiens; ++iMeri)
            {
                float teta = (2 * pi * iMeri) / nbMeridiens;
                vertices[ iMeri * (nbParallele - 1) + iPara - 1] = new Vector3(
                    (float)(rayon * Sin(fi) * Cos(teta)),
                    (float)(rayon * Sin(fi) * Sin(teta)),
                    (float)(rayon * Cos(fi)));
            }
        }

        vertices[nbMeridiens * (nbParallele - 1)] = new Vector3(0, 0, (float)rayon);
        vertices[nbMeridiens * (nbParallele - 1)+1] = new Vector3(0, 0, (float)-rayon);

        //tous les bords sauf le dernier
        int compteur = 0;
        for (int iPara = 1; iPara < nbParallele-1; iPara++)
        {
            for(int iMeri =0; iMeri < nbMeridiens-1; iMeri++)
            {
                triangles[compteur] = iMeri * (nbParallele-1) + iPara-1;
                triangles[compteur + 1] = iMeri * (nbParallele - 1) + iPara;
                triangles[compteur + 2] = (iMeri+1) * (nbParallele - 1) + iPara - 1;

                triangles[compteur + 4] = (iMeri+1) * (nbParallele - 1) + iPara;
                triangles[compteur + 3] = iMeri * (nbParallele - 1) + iPara;
                triangles[compteur + 5] = (iMeri+1) * (nbParallele - 1) + iPara - 1;
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
        for (int iMeri = 0; iMeri < nbMeridiens-1; iMeri++)
        {
            triangles[compteur] = nbMeridiens * (nbParallele - 1);
            triangles[compteur + 1] = iMeri * (nbParallele - 1) ;
            triangles[compteur + 2] = (iMeri + 1) * (nbParallele - 1);
            compteur += 3;
        }
        triangles[compteur] = nbMeridiens * (nbParallele - 1);
        triangles[compteur + 1] = (nbMeridiens - 1) * (nbParallele - 1);
        triangles[compteur + 2] = 0 * (nbParallele - 1);
        compteur += 3;

        //bouchon 2
        for (int iMeri = 0; iMeri < nbMeridiens-1; iMeri++)
        {
            triangles[compteur + 1] = nbMeridiens * (nbParallele - 1)+1;
            triangles[compteur ] = iMeri * (nbParallele - 1)+nbParallele-3;
            triangles[compteur + 2] = (iMeri + 1) * (nbParallele - 1) + nbParallele - 3;
            compteur += 3;
        }
        triangles[compteur +1] = nbMeridiens * (nbParallele - 1)+1;
        triangles[compteur ] = (nbMeridiens - 1) * (nbParallele - 1) + nbParallele - 3;
        triangles[compteur + 2] = 0 * (nbParallele - 1) + nbParallele - 3;



        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (vertices != null)
        {
            foreach (Vector3 cords in vertices)
            {
                Gizmos.DrawSphere(cords, 0.1f);
            }
        }
        
    }

    void Cylindre()
    {

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        double pi = 3.1416;

        Vector3[] vertices = new Vector3[nbMeridiens*2+2];
        int[] triangles = new int[nbMeridiens*12];

        for (int i = 0; i < nbMeridiens; ++i)
        {
            double teta = (2 * pi) * i / nbMeridiens;

            vertices[i] = new Vector3((float)(rayon * Cos(teta)), (float)(rayon * Sin(teta)), 0f);
        }

        for (int i = nbMeridiens; i < nbMeridiens*2; ++i)
        {
            double teta = (2 * pi) * i / nbMeridiens;

            vertices[i]  = new Vector3((float)(rayon * Cos(teta)), (float)(rayon * Sin(teta)), (float) height);
        }

        vertices[nbMeridiens*2]= new Vector3(0,0,0);
        vertices[nbMeridiens * 2+1] = new Vector3(0, 0, (float)height);

        //tous les bords sauf le dernier
        int compteur = 0;
        for (int i = 0; i < nbMeridiens-1; i++)
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
        triangles[compteur] = nbMeridiens-1;
        triangles[compteur + 1] = 0;
        triangles[compteur + 2] = nbMeridiens*2-1;

        triangles[compteur + 3] = nbMeridiens;
        triangles[compteur + 4] = nbMeridiens*2-1;
        triangles[compteur + 5] = 0;
        compteur += 6;

        //bouchons
        for (int i = 0; i < nbMeridiens-1; ++i)
        {
            triangles[compteur + 1] = i;
            triangles[compteur ] = i + 1;
            triangles[compteur + 2] = nbMeridiens * 2;

            triangles[compteur + 3] = i + nbMeridiens;
            triangles[compteur + 4] = i + nbMeridiens+1;
            triangles[compteur + 5] = nbMeridiens * 2 + 1;
            compteur += 6;
        }

        triangles[compteur + 1] = nbMeridiens - 1;
        triangles[compteur ] = 0;
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

    void Plan()
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
