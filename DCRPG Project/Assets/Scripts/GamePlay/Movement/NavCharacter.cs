using UnityEngine;
public class NavCharacter : NavMoveable {

    [SerializeField] private Camera m_CharacterCamera;
    private Ray m_Ray;

    private void Start()
    {
        m_Ray = new Ray();
        if(!m_CharacterCamera)
        {
            Debug.LogWarning("NavCharacter: Start: No camera selected, using main!");
            m_CharacterCamera = Camera.main;
        }
    }

    private void Update()
    {
        ClickToMove();
    }

	private void ClickToMove()
    {
        if(Input.GetButtonDown("BasicAttack"))
        {
            m_Ray = m_CharacterCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(m_Ray, out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "Ground")
                {
                    SetDestination(hit.point);
                }
            }
        }
    }
}
