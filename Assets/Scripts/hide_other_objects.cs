using UnityEngine;

public class hide_other_objects : MonoBehaviour, IAction
{
    Renderer[] m_Objects;

    bool m_Toggle;
    public void Start()
    {
        var temp = Game_Object_Collection.GameObjects;

        m_Objects = new Renderer[temp.Count];

        for(int i = 0; i < m_Objects.Length; i++)
        {
            m_Objects[i] = temp[i].GetComponent<Renderer>();
        }

        m_Toggle = gameObject.activeInHierarchy;        
    }

    public void Action()
    {
        foreach(Renderer obj in m_Objects)
        {
            if(obj != null && !obj.GetComponent<hide_other_objects>())
            {
                obj.gameObject.SetActive(m_Toggle);
            }
        }
    }

    public void OnMouseDown()
    {
        m_Toggle = !m_Toggle;
        Action();
    }
}
