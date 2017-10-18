using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOveride : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler

{
    [SerializeField] private RectTransform m_Target;
    [SerializeField] private float m_fTargetScale = 1.1f;
    [SerializeField] private float m_fTransitionSpeed = 1;

    private Vector3 m_StartScale;
    private bool m_bOverButton = false;  
    
    private void Start()
    {
        m_StartScale = m_Target.localScale;
    }  

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_bOverButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_bOverButton = false;
    }

    // Update is called once per frame
    void Update () {
		if(m_bOverButton)
        {
            m_Target.localScale = Vector3.Lerp(m_StartScale, m_StartScale* m_fTargetScale, m_fTransitionSpeed * Time.deltaTime);
        }
        else
        {
            m_Target.localScale = Vector3.Lerp(m_Target.localScale, m_StartScale, m_fTransitionSpeed * Time.deltaTime);
        }
	}
}
