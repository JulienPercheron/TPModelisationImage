using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Import : MonoBehaviour
{

    public TextAsset file;
    public Material mat;

    private void Start()
    {
        
        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[int.Parse(infos[0])];
        int[] triangles = new int[int.Parse(infos[1])*3];

        int compteur = 0;
        for (int i = 2; i < int.Parse(infos[0])+2; ++i)
        {
            splitContent[i] = splitContent[i].Replace(".", ",");
            string[] coords = splitContent[i].Split(" ");
            float x = float.Parse(coords[0]);
            float y = float.Parse(coords[1]);
            float z = float.Parse(coords[2]);
            vertices[compteur] = new Vector3(x, y, z);
            compteur++;
        }

        int initialI = int.Parse(infos[0]) + 2;
        compteur = 0;
        for (int i = initialI; i < initialI + int.Parse(infos[1]);i++)
        {
            string[] coords = splitContent[i].Split(" ");
            triangles[compteur] = int.Parse(coords[1]);
            triangles[compteur+1] = int.Parse(coords[2]);
            triangles[compteur+2] = int.Parse(coords[3]);
            compteur += 3;
        }

        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        
    }


}
