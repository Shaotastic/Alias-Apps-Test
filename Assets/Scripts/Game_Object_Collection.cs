using System.Collections.Generic;
using SimpleJSON;
using UnityEditor;
using UnityEngine;

public class Game_Object_Collection : MonoBehaviour
{
    public string nodeName;

    public Object[] m_Scripts;

    private static List<GameObject> m_GameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        var text = Resources.Load("scene_contents");
        var N = JSONArray.Parse(text.ToString());

        for (int i = 0; i < N[nodeName].Count; i++)
        {
            CreateFromJsonNode(N[nodeName][i]);
        }
    }

    void CreateFromJsonNode(JSONNode node)
    {
        var tempNode = JSONArray.Parse(node);

        for (int i = 0; i < node.Count; i++)
        {
            Debug.Log(node[i].Value);

        }

        GameObject newObject = GameObject.CreatePrimitive(GetObject(node[0]));

        var script = GetScriptObject(node[1]) as MonoScript;

        newObject.AddComponent(script.GetClass());

        newObject.transform.position = node[2].ReadVector3();

        m_GameObjects.Add(newObject);
    }

    Object GetScriptObject(string name)
    {
        string filename = System.IO.Path.GetFileNameWithoutExtension(name);

        for (int i = 0; i < m_Scripts.Length; i++)
        {
            if (filename.Equals(m_Scripts[i].name))
                return m_Scripts[i];
        }

        Debug.LogWarning("Could not find script object " + name);
        return null;
    }

    PrimitiveType GetObject(string name)
    {
        switch (name.ToUpper())
        {
            case "CAPSULE":
                return PrimitiveType.Capsule;
            case "CUBE":
                return PrimitiveType.Cube;
            case "CYLINDER":
                return PrimitiveType.Cylinder;
            case "PLANE":
                return PrimitiveType.Plane;
            case "QUAD":
                return PrimitiveType.Quad;
            case "SPHERE":
                return PrimitiveType.Sphere;
            default:
                Debug.LogWarning("Couldn't find object, default to sphere");
                return 0;

        }
    }

    public static List<GameObject> GameObjects
    {
        get => m_GameObjects;
    }
}
