using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private GameObject m_selectObject = default;
    private Rigidbody m_rigidbody = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchPos = Input.mousePosition;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        if (!GameManager.Instance.GetStageFail())
        {
            // first touch
            if (Input.GetMouseButtonDown(0))
            {
                Physics.Raycast(ray, out hit);
                SetSelectObject(hit);
            }
            // if m_selectObject is not null and touch is going on
            else if (Input.GetMouseButton(0))
            {
                if (m_selectObject != null)
                {
                    //MoveSelectObject(touchPos);
                    CalcVector(touchPos);
                }
            }
            // if m_selectObject is not null and touch is over
            else if (Input.GetMouseButtonUp(0))
            {
                if (m_selectObject != null)
                {
                    StopMovingSelectObject();
                    // reset selected gameobject
                    ResetSelectObject();
                }
            }
        }
    }

    /// <summary>
    /// Calculate distance touch pos
    /// </summary>
    /// <param name="touchPos"></param>
    public void CalcVector(Vector3 touchPos)
    {
        if(m_selectObject != null)
        {
            Vector3 temprenderPos = Vector3.zero;

            //レイを飛ばしてオブジェクトをすべて取得
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit[] _raycastAll = Physics.RaycastAll(ray, Mathf.Infinity);
            //すべて探索
            foreach (var VARIABLE in _raycastAll)
            {
                Vector3 pos = VARIABLE.point;
                pos.z = VARIABLE.transform.position.z;
                temprenderPos = pos;
            }

            if (temprenderPos.z > 8 || temprenderPos == Vector3.zero) return;

            temprenderPos.y = m_selectObject.transform.position.y;
            // if in rigidbody contranints position has been freeze, move position except freeze position 
            if ((m_rigidbody.constraints & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX)
            {
                temprenderPos.x = m_selectObject.transform.position.x;
            }
            else if ((m_rigidbody.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ)
            {
                temprenderPos.z = m_selectObject.transform.position.z;
            }
            MoveSelectObject(temprenderPos);
        }
    }

    /// <summary>
    /// move function when Object is selected and drage touch
    /// </summary>
    public void MoveSelectObject(Vector3 touchPos)
    {
        if(m_selectObject != null)
        {
            m_selectObject.transform.position = touchPos;
        }
    }

    /// <summary>
    /// select object
    /// </summary>
    /// <param name="rayhit"></param>
    public void SetSelectObject(RaycastHit rayhit)
    {
        if (rayhit.transform.gameObject.tag == "Player" || rayhit.transform.gameObject.tag == "Car")
        {
            m_selectObject = rayhit.transform.gameObject;
            m_rigidbody = m_selectObject.GetComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// select object
    /// </summary>
    /// <param name="rayhit"></param>
    public void StopMovingSelectObject()
    {
        if(m_selectObject != null)
        {
            m_rigidbody.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// reset select object
    /// </summary>
    public void ResetSelectObject()
    {
        if (m_selectObject != null)
        {
            m_selectObject = null;
        }
    }
}
