using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool m_state = false;
    public bool State => m_state;

    public bool InitialState = false;

    //Effects
    private Material m_gameobjectMaterial;
    [SerializeField] private Material m_onMaterial;
    [SerializeField] private Material m_offMaterial;
    [SerializeField] private Canvas m_interactionPrompt;

    private LeverGroup m_leverGroup = null;
    public LeverGroup LeverGroup => m_leverGroup;


    private void Awake()
    {
        //turns off on-targeted UI prompt in case it was left on in the editor
        ToggleUIPrompt(false);
    }
    private void Start()
    {
        if (InitialState)
        {
            if (gameObject.TryGetComponent(out Interactable interactable))
            {
                interactable.OnHit();
            }
            else
            {
                ToggleState(InitialState);
            }
        }
    }

    private void OnValidate()
    {
        if(transform.parent != null)
        {
            //resets other levers in the group to False
            if (transform.parent.gameObject.TryGetComponent(out LeverGroup lg) && InitialState)
            {
                lg.ResetOtherLeverInitialStates(gameObject);
            }
        } 
    }
    public void ToggleState()
    {
        m_state = !m_state;
        HandleAction();
    }

    public void ToggleState(bool state)
    {
        m_state = state;
        HandleAction();
    }

    private void HandleAction()
    {
        PlayAnimation(m_state);


        if (m_leverGroup != null && m_state)
        {
            m_leverGroup.ResetOtherLevers(gameObject);
        }
    }
    public void ToggleUIPrompt(bool state)
    {
        if (m_interactionPrompt != null)
        {
            m_interactionPrompt.enabled = state;
        }
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
    public void AssignLeverGroup(LeverGroup lg)
    {
        m_leverGroup = lg;
    }
}
