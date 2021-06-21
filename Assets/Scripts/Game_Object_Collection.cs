using System.Collections.Generic;
using SimpleJSON;
using UnityEditor;
using UnityEngine;

public class Game_Object_Collection : MonoBehaviour
{    
    //Used to get the first node parameter in the JSON file
    public string nodeName;

    //Used to validate the node array parameters within the JSON file
    public string[] m_NodeNames;

    //For the "script" type in the JSON, have the list of scripts needed
    public Object[] m_Scripts;

    private static List<GameObject> m_GameObjects = new List<GameObject>();

    // Awake is called before the first frame update
    void Awake()
    {
        //Load the JSON file
        var text = Resources.Load("scene_contents");

        //Parse the Nodes in the JSON file
        var N = JSONArray.Parse(text.ToString());

        //Loop through each Node in the JSON array
        for (int i = 0; i < N[nodeName].Count; i++)
        {
            CreateComponentsFromNode(N[nodeName][i]);
        }
    }

    /// <summary>
    /// From the JSON Node, it will create a Gameobject with the values within the node. 
    /// "m_NodeNames" is used to reference each value within the node.
    /// </summary>
    /// <param name="node">The JSON Node containing each parameter to create the object</param>
    void CreateComponentsFromNode(JSONNode node)
    {
        GameObject newObject = null;
        
        for (int i = 0; i < m_NodeNames.Length; i++)
        {
            if (node[m_NodeNames[i]] != null)
            {
                Debug.Log(node[m_NodeNames[i]]);
                CreateObject(node, m_NodeNames[i], ref newObject);
            }
            else
                Debug.LogWarning("This node \"" + m_NodeNames[i] + "\" does not exist in JSON file");
        }

        if(newObject != null)
        {
            m_GameObjects.Add(newObject);
        }
    }

    /// <summary>
    /// Creates the GameObjects using each node parameter from the node array 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="nodeName">Used to check if this node type exist within the Node</param>
    /// <param name="newGameObject"></param>
    void CreateObject(JSONNode node, string nodeName, ref GameObject newGameObject)
    {
        switch (nodeName)
        {
            case "type":
                newGameObject = GameObject.CreatePrimitive(GetObject(node[nodeName]));
                return;

            case "script":
                var script = GetScriptObject(node[1]) as MonoScript;
                newGameObject.AddComponent(script.GetClass());
                return;

            case "position":
                newGameObject.transform.position = node[nodeName].ReadVector3();
                return;
        }
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
