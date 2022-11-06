using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using static System.Math;

public class SphereEnumSpatiale : MonoBehaviour
{
    public Material mat;

    public int nbCubesArrete;

    public bool intersection = true;

    public GameObject eraser;

    private Vector3[] vertices;

    private Vector3[] cubeVertices;

    private Vector3 pointMax;

    private Vector3 pointMin;

    float distanceCube;



    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointMax, 0.2f);
        Gizmos.DrawSphere(pointMin, 0.2f);
    }

    private struct Sphere
    {
        public float rayon;
        public Vector3 centre;

        public Sphere(float rayon, Vector3 centre)
        {
            this.rayon = rayon;
            this.centre = centre;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        int nbCubes = (int)Pow(nbCubesArrete, 3);

        
        List< Sphere > spheres = new List<Sphere> ();

        //spheres.Add(new Sphere(5, new Vector3(10, 0, 0)));
        spheres.Add(new Sphere(5, new Vector3(5, 2, 5)));
        //spheres.Add(new Sphere(5, new Vector3(10, 5, 0)));

        pointMax = new Vector3(int.MinValue, int.MinValue, int.MinValue);
        pointMin = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);

        foreach (Sphere sphere in spheres)
        {
            if (sphere.centre.x + sphere.rayon > pointMax.x)
                pointMax.x = sphere.centre.x + sphere.rayon;
            if (sphere.centre.y + sphere.rayon > pointMax.y)
                pointMax.y = sphere.centre.y + sphere.rayon;
            if (sphere.centre.z + sphere.rayon > pointMax.z)
                pointMax.z = sphere.centre.z + sphere.rayon;
            if (sphere.centre.x - sphere.rayon < pointMin.x)
                pointMin.x = sphere.centre.x - sphere.rayon;
            if (sphere.centre.y - sphere.rayon < pointMin.y)
                pointMin.y = sphere.centre.y - sphere.rayon;
            if (sphere.centre.z - sphere.rayon < pointMin.z)
                pointMin.z = sphere.centre.z - sphere.rayon;
        }

        float distanceX = pointMax.x - pointMin.x;
        float distanceY = pointMax.y - pointMin.y;
        float distanceZ = pointMax.z - pointMin.z;

        if (distanceX > distanceY && distanceX > distanceZ)
        {
            pointMax.y += distanceX - distanceY;
            pointMax.z += distanceX - distanceZ;
        }
        else if(distanceY > distanceX && distanceY > distanceZ)
        {
            pointMax.x += distanceY - distanceX;
            pointMax.z += distanceY - distanceZ;
        } 
        else if(distanceZ > distanceY && distanceZ > distanceX)
        {
            pointMax.x += distanceZ - distanceX;
            pointMax.y += distanceZ - distanceY;
        }

        distanceCube = pointMax.x - pointMin.x;

        for (int indexX = -nbCubesArrete/2; indexX <= nbCubesArrete/2; indexX++)
        {
            for (int indexY = -nbCubesArrete/2; indexY <= nbCubesArrete/2; indexY++)
            {
                for (int indexZ = -nbCubesArrete/2; indexZ <= nbCubesArrete/2; indexZ++)
                {
                    Vector3 centreCube = new Vector3(
                    (indexX * distanceCube / nbCubesArrete),
                    (indexY * distanceCube / nbCubesArrete),
                    (indexZ * distanceCube / nbCubesArrete));

                    if (intersection)
                    {
                        bool inAllSpheres = true;
                        foreach (Sphere sphere in spheres)
                        {
                            if ((Abs(Vector3.Distance(centreCube, sphere.centre -centreCube)) > sphere.rayon))
                            {
                                inAllSpheres = false;
                            }
                        }

                        if (inAllSpheres)
                        {
                            GameObject cube = CubeEnglo(centreCube, distanceCube / (nbCubesArrete * 2));
                        }
                    }
                    else
                    {
                        foreach (Sphere sphere in spheres)
                        {
                            if (!(Abs(Vector3.Distance(centreCube, sphere.centre - centreCube)) > sphere.rayon))
                            {
                                GameObject cube = CubeEnglo(centreCube, distanceCube / (nbCubesArrete * 2));
                                break;
                            }
                        }
                    }   
                }
            }
        }
    }


    Vector3 AbsVector(Vector3 input)
    {
        return new Vector3(Abs(input.x), Abs(input.y), Abs(input.z));
    }

    GameObject CubeEnglo(Vector3 centre, float rayonCube)
    {
        GameObject cube = new GameObject("cube");
        cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>();
        cube.AddComponent<BoxCollider>();
        cube.AddComponent<CubeSphere>();

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

        
        Vector3 longeur = AbsVector(cubeVertices[0] - cubeVertices[1]);
        Vector3 profondeur = AbsVector(cubeVertices[3] - cubeVertices[1]);
        Vector3 hauteur = AbsVector(cubeVertices[3] - cubeVertices[4]);

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

        //cube.AddComponent<onDrawGizmo>();


        cube.GetComponent<BoxCollider>().center = centre;
        cube.GetComponent<BoxCollider>().size = new Vector3(longeur.magnitude, hauteur.magnitude, profondeur.magnitude);
        cube.GetComponent<BoxCollider>().isTrigger = true;


        cube.GetComponent<MeshFilter>().mesh = msh;
        cube.GetComponent<MeshRenderer>().material = mat;
        return cube;
    }
}
