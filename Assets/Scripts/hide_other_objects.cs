using UnityEngine;

public class hide_other_objects : MonoBehaviour, IAction
{
    GameObject[] m_Objects;

    bool m_Toggle;

    /*First frame call, get all the objects that were generated from Game_Object_Collection
     * and store them as a array for Renderers
    */
    public void Start()
    {
        m_Objects = Game_Object_Collection.GameObjects.ToArray();

        m_Toggle = gameObject.activeInHierarchy;        
    }

    public void Action()
    {
        foreach(GameObject obj in m_Objects)
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
