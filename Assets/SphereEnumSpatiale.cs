using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class SphereEnumSpatiale : MonoBehaviour
{
    public Material mat;

    public Vector3 centreSphere = new Vector3(0, 0, 0);

    public float rayon;

    public int profondeurCube = 2;

    private Vector3[] vertices;

    private Vector3[] cubeVertices;

    // Start is called before the first frame update
    void Start()
    {
        int nbCubesArrete = (int)Pow(2, profondeurCube);

        int nbCubes = (int)Pow(nbCubesArrete, 3);

        

        int cpt = 0;
        for(int indexX = 0; indexX < nbCubesArrete; indexX++)
        {
            for (int indexY = 0; indexY < nbCubesArrete; indexY++)
            {
                for (int indexZ = 0; indexZ < nbCubesArrete; indexZ++)
                {
                    cpt++;
                    Vector3 centreCube = new Vector3(
                        (indexX * rayon * 2)  / ((float)Pow(profondeurCube, 2)),
                        (indexY * rayon * 2) / ((float)Pow(profondeurCube, 2)),
                        (indexZ * rayon * 2) / ((float)Pow(profondeurCube, 2)));
                    GameObject cube = CubeEnglo(centreCube, rayon/nbCubesArrete);
                    //if (Abs(Vector3.Distance(centreCube, centreSphere)) > rayon) 
                    //{
                    //    cube.SetActive(false);
                    //}
                }
            }
        }
    }

    GameObject CubeEnglo(Vector3 centre, float rayonCube)
    {
        GameObject cube = new GameObject("cube");
        cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>();

        cubeVertices = new Vector3[8];
        int[] trianglesCube = new int[36];

        cubeVertices[0] = new Vector3(centre.x - rayonCube, centre.y - rayonCube, centre.z - rayonCube);
        cubeVertices[1] = new Vector3(centre.x + rayonCube, centre.y - rayonCube, centre.z - rayonCube);
        cubeVertices[2] = new Vector3(centre.x - rayonCube, centre.y - rayonCube, centre.z + rayonCube);
        cubeVertices[3] = new Vector3(centre.x + rayonCube, centre.y - rayonCube, centre.z + rayonCube);


        cubeVertices[4] = new Vector3(centre.x + rayonCube, centre.y + rayonCube, centre.z + rayonCube);
        cubeVertices[5] = new Vector3(centre.x - rayonCube, centre.y + rayonCube, centre.z + rayonCube);
        cubeVertices[6] = new Vector3(centre.x - rayonCube, centre.y + rayonCube, centre.z - rayonCube);
        cubeVertices[7] = new Vector3(centre.x + rayonCube, centre.y + rayonCube, centre.z - rayonCube);

        trianglesCube[0] = 0;
        trianglesCube[1] = 2;
        trianglesCube[2] = 5;

        trianglesCube[3] = 5;
        trianglesCube[4] = 6;
        trianglesCube[5] = 0;
        

        trianglesCube[6] = 2;
        trianglesCube[7] = 3;
        trianglesCube[8] = 4;

        trianglesCube[9] = 4;
        trianglesCube[10] = 5;
        trianglesCube[11] = 2;


        trianglesCube[12] = 3;
        trianglesCube[13] = 1;
        trianglesCube[14] = 7;

        trianglesCube[15] = 7;
        trianglesCube[16] = 4;
        trianglesCube[17] = 3;


        trianglesCube[18] = 1;
        trianglesCube[19] = 0;
        trianglesCube[20] = 6;

        trianglesCube[21] = 6;
        trianglesCube[22] = 7;
        trianglesCube[23] = 1;

        trianglesCube[24] = 3;
        trianglesCube[25] = 2;
        trianglesCube[26] = 0;

        trianglesCube[27] = 0;
        trianglesCube[28] = 1;
        trianglesCube[29] = 3;

        trianglesCube[30] = 6;
        trianglesCube[31] = 5;
        trianglesCube[32] = 4;

        trianglesCube[33] = 4;
        trianglesCube[34] = 7;
        trianglesCube[35] = 6;


        Mesh msh = new Mesh();

        msh.vertices = cubeVertices;
        msh.triangles = trianglesCube;

        cube.GetComponent<MeshFilter>().mesh = msh;
        cube.GetComponent<MeshRenderer>().material = mat;
        return cube;
    }
}