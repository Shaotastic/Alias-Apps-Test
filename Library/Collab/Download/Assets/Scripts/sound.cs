using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour, IAction
{
    public void Action()
    {
        Debug.Log("Play sound??");
    }

    public void OnMouseDown()
    {
        Action();
    }
}
