using System.Collections.Generic;
using UnityEngine;

public class LeverGroup : MonoBehaviour
{
    private List<GameObject> m_goLevers;
    /// <summary>
    /// child objects with a Lever component get automatically added on Awake
    /// </summary>
    [SerializeField] private List<Lever> m_manuallyAssignedLevers;
    [SerializeField] private bool m_hasDefaultSelection;
    private void Awake()
    {
        m_goLevers = InitialiseLevers();
    }

    public List<GameObject> InitialiseLevers()
    {
        List<GameObject> goLevers = new List<GameObject>();

        Lever[] childLevers = transform.GetComponentsInChildren<Lever>();

        foreach (Lever lever in childLevers)
        {
            if (!m_manuallyAssignedLevers.Contains(lever))
            {
                m_manuallyAssignedLevers.Add(lever);
                Debug.Log($"Child lever of SingleSelectLevers -{gameObject}- automatically assigned:  {lever.gameObject.name} ");
            }
        }
        foreach (Lever lever in m_manuallyAssignedLevers)
        {
            lever.AssignLeverGroup(this);
            goLevers.Add(lever.gameObject);
        }

        return goLevers;
    }
    public void ResetOtherLevers(GameObject leverObject)
    {
        foreach (GameObject go in m_goLevers)
        {
            if (go == leverObject)
            {
                Debug.Log("same object");
                continue;
            }
            else if (go.TryGetComponent(out Lever lever))
            {
                Debug.Log(go.name);

                //if lever is on, turn off
                if (lever.State) lever.ToggleState();
            }
        }
    }

    public void ResetOtherLeverInitialStates(GameObject leverObject)
    {
        List<GameObject> goLevers = InitialiseLevers();
        foreach (GameObject goLever in goLevers)
        {
            if(goLever == leverObject)
            {
                continue;
            }
            else if(goLever.TryGetComponent(out Lever lever) && lever.InitialState)
            {
                Debug.LogWarning($"InitialState of {goLever} set to false, after change to {leverObject} InitialState in the same LeverGroup.");
                lever.InitialState = false;
            }
        }
    }
}
