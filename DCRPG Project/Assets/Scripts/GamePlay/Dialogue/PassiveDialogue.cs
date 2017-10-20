using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PassiveDialogue : MonoBehaviour {

    [SerializeField] Button m_button;

    void Start()
    {
        m_button.enabled = false;

    }

    void onTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {

        }
    }
}
