using UnityEngine;

public class hide_show_object : MonoBehaviour, IAction
{
    Renderer m_MeshRenderer;

    public void Action()
    {
        m_MeshRenderer.enabled = !m_MeshRenderer.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MeshRenderer = GetComponent<Renderer>();
    }

    public void OnMouseDown()
    {
        Action();
    }
}
