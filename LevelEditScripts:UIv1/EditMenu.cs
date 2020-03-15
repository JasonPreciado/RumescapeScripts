using UnityEngine;

public class EditMenu : MonoBehaviour
{
    //public Transform yOnFloor;

    public Material newMaterial;
	public float dist;
    [SerializeField]
    private GameObject clickedUI;
    [SerializeField]
    private GameObject spawnObjUI;
    [SerializeField]
    private GameObject EditButton;
    Camera cam;
	Renderer m_Renderer;
	Material temp;
	private float radius = 3f;
	void Start()
	{
		m_Renderer = GetComponent<MeshRenderer>();
		temp = m_Renderer.material;
		cam = Camera.main;
	}
	void Update()
	{
	//	dist = Vector3.Distance(cam.transform.position, transform.position);
		//TriggerOutline();

    }
    public void EditItemMenu(GameObject item)
	{

            //return if any edit or spawn uis active to ensure correct activeObject
            GameObject[] Menus = GameObject.FindGameObjectsWithTag("EditUI");
            foreach (GameObject Menu in Menus)
            {
                if (Menu.activeSelf)
                    return;
            }
            GameObject[] MainMenus = GameObject.FindGameObjectsWithTag("EditUIMain");
            foreach (GameObject MainMenu in MainMenus)
            {
                if (MainMenu.activeSelf)
                return;
            }
            GameObject[] SpawnMenus = GameObject.FindGameObjectsWithTag("SpawnUI");
            foreach (GameObject SpawnMenu in SpawnMenus)
            {
                if (SpawnMenu.activeSelf)
                return;
            }
            //set whatevet ui this object has active and make the string active object to be edited this objects name
            clickedUI.SetActive(true);
            PlayerPrefs.SetString("activeObject", name);
            PlayerPrefs.SetInt("UIactive", 1);

    }

    void TriggerOutline()
	{
		if (dist <= radius && PlayerPrefs.GetInt("UIactive") != 1)
		{
			m_Renderer.material = newMaterial;
		}
		else
		{
			m_Renderer.material = temp;
		}
	}
}
