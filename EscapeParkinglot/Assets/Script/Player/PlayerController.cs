using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool m_contact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Initialize all variable
    /// </summary>
    void Initialize()
    {
        m_contact = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// function which can reference to check bool
    /// </summary>
    public bool GetContactChecker()
    {
        return m_contact;
    }

    /// <summary>
    /// contact function
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Floor")
        {
            GameManager.Instance.SetStageClear(true);
        }
        if(collider.tag == "Car")
        {
            m_contact = true;
        }
    }

    /// <summary>
    /// if player is not contact with other cars
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Car")
        {
            m_contact = false;
        }
    }
}
