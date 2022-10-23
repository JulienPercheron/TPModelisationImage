using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Import : MonoBehaviour
{

    public TextAsset file;
    public Material mat;
    public bool correctObject = true;
    public int brokenCount;

    private void Start()
    {
        if (correctObject)
        {
            BuildCorrectObject();
        }
        else
        {
            BuildBrokenObject();
            Save();
        }
        
    }

    private void Save()
    {
        string path = Application.persistentDataPath + "/objectBroken.txt";

        if (File.Exists(path))
        {
            File.WriteAllText(path, "");
        }

        Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        int[] triangles = gameObject.GetComponent<MeshFilter>().mesh.triangles;

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("OFF");

        string infos = vertices.Length + " " + triangles.Length / 3 + " " + 0;
        writer.WriteLine(infos);
        
        for(int i = 0; i < vertices.Length; i++)
        {
            string coords = "";
            coords += vertices[i].x + " ";
            coords += vertices[i].y + " ";
            coords += vertices[i].z;
            writer.WriteLine(coords);
        }

        for(int i = 0; i < triangles.Length; i+=3)
        {
            string coords = "";
            coords += "3 ";
            coords += triangles[i] + " ";
            coords += triangles[i+1] + " ";
            coords += triangles[i+2];
            writer.WriteLine(coords);
        }
        writer.Close();
    }

    private void BuildBrokenObject()
    {
        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[int.Parse(infos[0])];
        int[] triangles = new int[int.Parse(infos[1]) * 3];

        int compteur = 0;
        for (int i = 2; i < int.Parse(infos[0]) + 2; ++i)
        {
            if (!(i % brokenCount == 0))
            {
                splitContent[i] = splitContent[i].Replace(".", ",");
                string[] coords = splitContent[i].Split(" ");
                float x = float.Parse(coords[0]);
                float y = float.Parse(coords[1]);
                float z = float.Parse(coords[2]);
                vertices[compteur] = new Vector3(x, y, z);
            }
            compteur++;
        }

        int initialI = int.Parse(infos[0]) + 2;
        compteur = 0;
        for (int i = initialI; i < initialI + int.Parse(infos[1]); i++)
        {
            string[] coords = splitContent[i].Split(" ");
            if (vertices[int.Parse(coords[1])] != vertices[int.Parse(coords[2])] && vertices[int.Parse(coords[1])] != vertices[int.Parse(coords[3])] && vertices[int.Parse(coords[2])] != vertices[int.Parse(coords[3])] && vertices[int.Parse(coords[1])] != Vector3.zero && vertices[int.Parse(coords[2])] != Vector3.zero && vertices[int.Parse(coords[3])] != Vector3.zero)
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

    private void BuildCorrectObject()
    {
        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[int.Parse(infos[0])];
        int[] triangles = new int[int.Parse(infos[1]) * 3];

        int compteur = 0;
        for (int i = 2; i < int.Parse(infos[0]) + 2; ++i)
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
}
