using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    private GameObject m_selectObject = default;
    private Rigidbody m_rigidbody = default;
    //private Vector3 m_offset;
    private Vector3 m_screenSpace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(targetPos);

        // 最初にタッチした時
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit);
            if(hit.transform.gameObject.tag != "don't_touch")
            {
                Debug.Log(hit.transform.gameObject.tag);
                m_screenSpace = Camera.main.WorldToScreenPoint(hit.transform.position);
                //m_offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                SetSelectObject(hit);
                if (m_selectObject != null)
                {
                    m_rigidbody = m_selectObject.GetComponent<Rigidbody>();
                }
            }
        }
        // if m_selectObject is not null and touch is going on
        else if (m_selectObject != null && Input.GetMouseButton(0))
        {
            Debug.Log("start to move object process" + m_selectObject.transform.gameObject.tag);
            MoveSelectObject(targetPos);
        }
        // if m_selectObject is not null and touch is over
        else if (m_selectObject != null && Input.GetMouseButtonUp(0))
        {
            MoveSelectObject(targetPos);
            // reset selected gameobject
            ResetSelectObject();
        }
    }

    /// <summary>
    /// move function when Object is selected and drage touch
    /// </summary>
    public void MoveSelectObject(Vector3 targetPos)
    {
        if(m_selectObject != null)
        {
            Vector3 updatePos = Camera.main.ScreenToWorldPoint(targetPos);

            // if in rigidbody contranints position has been freeze, move position except freeze position 
            if ((m_rigidbody.constraints & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX)
            {
                updatePos.x = m_selectObject.transform.position.x;
            }
            else if ((m_rigidbody.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ)
            {
                updatePos.z = m_selectObject.transform.position.z;
            }
            updatePos.y = 0.6f;
            m_selectObject.transform.position = updatePos;
            Debug.Log(updatePos);
        }
    }

    public void SetSelectObject(RaycastHit rayhit)
    {
        m_selectObject = rayhit.transform.gameObject;
    }

    public void ResetSelectObject()
    {
        if (m_selectObject != null)
        {
            m_selectObject = null;
        }
    }
}
