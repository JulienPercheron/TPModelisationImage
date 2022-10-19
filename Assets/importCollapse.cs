using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class importCollapse : MonoBehaviour
{
    public TextAsset file;
    public Material mat;

    public int nbCubeArrete;

    private void Start()
    {

        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[int.Parse(infos[0])];
        Vector3[] verticesTemp = new Vector3[int.Parse(infos[0])];
        int[] triangles = new int[int.Parse(infos[1]) * 3];

        int compteur = 0;

        Vector3 pointMax = new Vector3(int.MinValue, int.MinValue, int.MinValue);
        Vector3 pointMin = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        for (int i = 2; i < int.Parse(infos[0]) + 2; ++i)
        {
            splitContent[i] = splitContent[i].Replace(".", ",");
            string[] coords = splitContent[i].Split(" ");
            float x = float.Parse(coords[0]);
            if(x > pointMax.x)
            {
                pointMax.x = x;
            }
            if (x < pointMin.x)
            {
                pointMin.x = x;
            }
            float y = float.Parse(coords[1]);
            if (y > pointMax.y)
            {
                pointMax.y = y;
            }
            if (y < pointMin.y)
            {
                pointMin.y = y;
            }
            float z = float.Parse(coords[2]);
            if (z > pointMax.z)
            {
                pointMax.z = z;
            }
            if (z < pointMin.z)
            {
                pointMin.z = z;
            }
            verticesTemp[compteur] = new Vector3(x, y, z);
            compteur++;
        }

        pointMax.x += 0.5f;
        pointMax.y += 0.5f;
        pointMax.z += 0.5f;

        pointMin.x -= 0.5f;
        pointMin.y -= 0.5f;
        pointMin.z -= 0.5f;

        float distanceX = pointMax.x - pointMin.x;
        float distanceY = pointMax.y - pointMin.y;
        float distanceZ = pointMax.z - pointMin.z;

        if (distanceX > distanceY && distanceX > distanceZ)
        {
            pointMax.y += distanceX - distanceY;
            pointMax.z += distanceX - distanceZ;
        }
        else if (distanceY > distanceX && distanceY > distanceZ)
        {
            pointMax.x += distanceY - distanceX;
            pointMax.z += distanceY - distanceZ;
        }
        else if (distanceZ > distanceY && distanceZ > distanceX)
        {
            pointMax.x += distanceZ - distanceX;
            pointMax.y += distanceZ - distanceY;
        }

        float longueurArrete = pointMax.x - pointMin.x;
        float longueurCube = longueurArrete / nbCubeArrete;

        for(int indexX=0; indexX < nbCubeArrete; indexX++)
        {
            for (int indexY = 0; indexY < nbCubeArrete; indexY++)
            {
                for (int indexZ = 0; indexZ < nbCubeArrete; indexZ++)
                {
                    Vector3 pointMinCube = new Vector3(pointMin.x+indexX*longueurCube, pointMin.y + indexY * longueurCube, pointMin.z + indexZ * longueurCube);
                    Vector3 pointMaxCube = new Vector3(pointMin.x + (indexX+1) * longueurCube, pointMin.y + (indexY+1) * longueurCube, pointMin.z + (indexZ+1) * longueurCube);
                    List<Vector3> listePoints = new List<Vector3>();
                    int nbPoints = 0;
                    Vector3 firstPoint = Vector3.zero;
                    for (int i = 0; i < verticesTemp.Length; i++)
                    {
                        if (pointDansCube(verticesTemp[i], pointMinCube, pointMaxCube))
                        {
                            if(nbPoints == 0)
                            {
                                vertices[i] = verticesTemp[i];
                                firstPoint = verticesTemp[i];
                                nbPoints++;
                            }
                            else
                            {
                                vertices[i] = firstPoint;
                                nbPoints++;
                            }
                        }
                    }
                }
            }
        }

        int initialI = int.Parse(infos[0]) + 2;
        compteur = 0;
        for (int i = initialI; i < initialI + int.Parse(infos[1]); i++)
        {
            string[] coords = splitContent[i].Split(" ");
            if (vertices[int.Parse(coords[1])] != vertices[int.Parse(coords[2])] && vertices[int.Parse(coords[1])] != vertices[int.Parse(coords[3])] && vertices[int.Parse(coords[2])] != vertices[int.Parse(coords[3])])
            {
                triangles[compteur] = int.Parse(coords[1]);
                triangles[compteur + 1] = int.Parse(coords[2]);
                triangles[compteur + 2] = int.Parse(coords[3]);
                compteur += 3;
            }
        }

        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }

    bool pointDansCube(Vector3 point, Vector3 pointMin, Vector3 pointMax)
    {
        if(point.x >= pointMin.x && point.x <= pointMax.x && point.y >= pointMin.y && point.y <= pointMax.y && point.z >= pointMin.z && point.z <= pointMax.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


