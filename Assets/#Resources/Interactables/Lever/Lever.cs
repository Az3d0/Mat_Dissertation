using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool m_state = false;
    public bool State => m_state;
    [SerializeField] private Material m_onMaterial;
    [SerializeField] private Material m_offMaterial;

    public void ToggleState()
    {
        m_state = !m_state; 
        PlayAnimation(m_state);
    }

    public void PlayAnimation(bool state)
    {
        if (state)
        {
            gameObject.GetComponent<Renderer>().material = m_onMaterial;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m_offMaterial;
        }
    }
}
