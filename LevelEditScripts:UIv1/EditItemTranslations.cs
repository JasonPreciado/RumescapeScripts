using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditItemTranslations : MonoBehaviour
{
    [SerializeField]
    private GameObject CoinPrefab;
    [SerializeField]
    private GameObject PigPrefab;
    [SerializeField]
    private GameObject BuildingPrefab;
    [SerializeField]
    private GameObject PedestalPrefab;

    [SerializeField]
    private GameObject spawnObjUI;
    [SerializeField]
    private GameObject EditButton;
    public Transform originalParent;
    public Text SafeComboXtext;
    public Text SafeComboYtext;
    public Text SafeComboZtext;
    public GameObject targetSpawnOBJ;
       [SerializeField]
    private Button SpawnButton;
    [SerializeField]
    private GameObject SpawnlvlBox;
    [SerializeField]
    private GameObject ScaleMenu;
    [SerializeField]
    private GameObject MoveUI;
    [SerializeField]
    private GameObject ComboMenu;
    public GameObject activeOBJ;
    private bool ObjHeld;
    private bool scaling;
    private bool beingHeld;
    public int SpawnOBjCount;
    Rigidbody rb;
    [SerializeField]
    public Transform placementTransform;
    public float speed = 1.0f;
    public float movementDist = 0.3f;
    public float scaleUp = 1.2f;
    public float scaleDown = .8f;
    private float scaleChange;
   
    void Start()
    {
        SpawnOBjCount = 0;
        beingHeld = false;
        ObjHeld = false;
        scaling = false;
        PlayerPrefs.SetInt("UIactive", 0);
        PlayerPrefs.SetInt("SafeComboX", 0);
        PlayerPrefs.SetInt("SafeComboY", 0);
        PlayerPrefs.SetInt("SafeComboZ", 0);
        PlayerPrefs.SetString("activeObject", "NULL");
        SafeComboXtext.text = (PlayerPrefs.GetInt("SafeComboX")).ToString();
        SafeComboYtext.text = (PlayerPrefs.GetInt("SafeComboY")).ToString();
        SafeComboZtext.text = (PlayerPrefs.GetInt("SafeComboZ")).ToString();
    }
    public void DragObj()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (!ObjHeld)
            ObjHeld = true;
        if (ObjHeld)
            ObjHeld = false;
    }
    public void Scaling()
    {
        //set all editUIs false for easy switching between edit types
        GameObject[] Menus = GameObject.FindGameObjectsWithTag("EditUI");
        foreach (GameObject Menu in Menus)
        {
            if (Menu.activeSelf)
                Menu.SetActive(false);
        }
        ScaleMenu.SetActive(true);
    }
    public void SpawnItemMenu()
    {
        //set all editUIs and main menu to false for spawning
        GameObject[] Menus = GameObject.FindGameObjectsWithTag("EditUI");
        foreach (GameObject Menu in Menus)
        {
            if (Menu.activeSelf)
                Menu.SetActive(false);
        }
        GameObject[] MainMenus = GameObject.FindGameObjectsWithTag("EditUIMain");
        foreach (GameObject MainMenu in MainMenus)
        {
            if (MainMenu.activeSelf)
                MainMenu.SetActive(false);
        }
        //set spawn ui active and zero out active object
        spawnObjUI.SetActive(true);
        PlayerPrefs.SetString("activeObject", "NULL");
        PlayerPrefs.SetInt("UIactive", 1);
    }
    public void SpawnToEdit()
    {
        //set all editUIs false for easy switching between edit types
        GameObject[] SpawnMenus = GameObject.FindGameObjectsWithTag("SpawnUI");
        foreach (GameObject SpawnMenu in SpawnMenus)
        {
            if (SpawnMenu.activeSelf)
                SpawnMenu.SetActive(false);
        }
        EditButton.SetActive(true);
    }
    public void MoveMenu()
    {
        //set all editUIs false for easy switching between edit types
        GameObject[] Menus = GameObject.FindGameObjectsWithTag("EditUI");
        foreach (GameObject Menu in Menus)
        {
            if (Menu.activeSelf)
                Menu.SetActive(false);
        }
        MoveUI.SetActive(true);
    }

    public void SettingCombo()
    {
        //set all editUIs false for easy switching between edit types
        GameObject[] Menus = GameObject.FindGameObjectsWithTag("EditUI");
        foreach (GameObject Menu in Menus)
        {
            if (Menu.activeSelf)
                Menu.SetActive(false);
        }
        ComboMenu.SetActive(true);
    }
    public void ScaleXUP()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change scale
        scaleChange = (float)(activeOBJ.transform.localScale.x * scaleUp);
        activeOBJ.transform.localScale = new Vector3(scaleChange, activeOBJ.transform.localScale.y, activeOBJ.transform.localScale.z);
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.scale.x = scaleChange;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if(activeOBJ.name == obj.data.OBJname)
            {
                obj.data.scale.x = scaleChange;
            }
        }
        */
    }
    public void ScaleXDown()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change scale
        scaleChange = (float)(activeOBJ.transform.localScale.x * scaleDown);
        activeOBJ.transform.localScale = new Vector3(scaleChange, activeOBJ.transform.localScale.y, activeOBJ.transform.localScale.z);
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.scale.x = scaleChange;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.scale.x = scaleChange;
            }
        }
        */
    }
    public void ScaleYUP()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change scale
        scaleChange = (float)(activeOBJ.transform.localScale.y * scaleUp);
        activeOBJ.transform.localScale = new Vector3(activeOBJ.transform.localScale.x, scaleChange, activeOBJ.transform.localScale.z);
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.scale.y = scaleChange;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.scale.y = scaleChange;
            }
        }
        */
    }
    public void ScaleYDown()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change scale
        scaleChange = (float)(activeOBJ.transform.localScale.y * scaleDown);
        activeOBJ.transform.localScale = new Vector3(activeOBJ.transform.localScale.x, scaleChange, activeOBJ.transform.localScale.z);
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.scale.y = scaleChange;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.scale.y = scaleChange;
            }
        }
        */
    }
    public void ScaleZUP()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change scale
        scaleChange = (float)(activeOBJ.transform.localScale.z * scaleUp);
        activeOBJ.transform.localScale = new Vector3(activeOBJ.transform.localScale.x, activeOBJ.transform.localScale.y, scaleChange);
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.scale.z = scaleChange;

        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.scale.z = scaleChange;
            }
        }
        */
    }
    public void ScaleZDown()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change scale
        scaleChange = (float)(activeOBJ.transform.localScale.z * scaleDown);
        activeOBJ.transform.localScale = new Vector3(activeOBJ.transform.localScale.x, activeOBJ.transform.localScale.y, scaleChange);
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.scale.z = scaleChange;

        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.scale.z = scaleChange;
            }
        }
        */
    }
    public void xPosUp()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change pos
        Vector3 newPos = activeOBJ.transform.position;
        newPos.x += movementDist;
        activeOBJ.transform.position = newPos;
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.pos = newPos;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.pos = newPos;
            }
        }
        */
    }
    public void yPosUp()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change pos
        Vector3 newPos = activeOBJ.transform.position;
        newPos.y += movementDist;
        activeOBJ.transform.position = newPos;
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.pos = newPos;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.pos = newPos;
            }
        }
        */

    }
    public void zPosUp()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change pos
        Vector3 newPos = activeOBJ.transform.position;
        newPos.z += movementDist;
        activeOBJ.transform.position = newPos;
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.pos = newPos;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.pos = newPos;
            }
        }
        */

    }
    public void xPosDown()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change pos
        Vector3 newPos = activeOBJ.transform.position;
        newPos.x -= movementDist;
        activeOBJ.transform.position = newPos;
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.pos = newPos;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.pos = newPos;
            }
        }
        */
    }
    public void yPosDown()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change pos
        Vector3 newPos = activeOBJ.transform.position;
        newPos.y -= movementDist;
        activeOBJ.transform.position = newPos;
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.pos = newPos;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.pos = newPos;
            }
        }
        */
    }
    public void zPosDown()
    {
        //confirm active object
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        //change pos
        Vector3 newPos = activeOBJ.transform.position;
        newPos.z -= movementDist;
        activeOBJ.transform.position = newPos;
        //save change to data
        activeOBJ.GetComponent<EditModeOBJ>().data.pos = newPos;
        /*
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            if (activeOBJ.name == obj.data.OBJname)
            {
                obj.data.pos = newPos;
            }
        }
        */
    }
    public void SafeIncX()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (PlayerPrefs.GetInt("SafeComboX") == 9)
            PlayerPrefs.SetInt("SafeComboX", 0);
        else
            PlayerPrefs.SetInt("SafeComboX", PlayerPrefs.GetInt("SafeComboX") + 1);

        SafeComboXtext.text = (PlayerPrefs.GetInt("SafeComboX")).ToString();
    }
    public void SafeIncY()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (PlayerPrefs.GetInt("SafeComboY") == 9)
            PlayerPrefs.SetInt("SafeComboY", 0);
        else
            PlayerPrefs.SetInt("SafeComboY", PlayerPrefs.GetInt("SafeComboX") + 1);

        SafeComboYtext.text = (PlayerPrefs.GetInt("SafeComboY")).ToString();
    }
    public void SafeIncZ()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (PlayerPrefs.GetInt("SafeComboZ") == 9)
            PlayerPrefs.SetInt("SafeComboZ", 0);
        else
            PlayerPrefs.SetInt("SafeComboZ", PlayerPrefs.GetInt("SafeComboX") + 1);

        SafeComboZtext.text = (PlayerPrefs.GetInt("SafeComboZ")).ToString();
    }
    public void SafeDecX()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (PlayerPrefs.GetInt("SafeComboX") == 0)
            PlayerPrefs.SetInt("SafeComboX", 9);
        else
            PlayerPrefs.SetInt("SafeComboX", PlayerPrefs.GetInt("SafeComboX") - 1);

        SafeComboXtext.text = (PlayerPrefs.GetInt("SafeComboX")).ToString();
    }
    public void SafeDecY()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (PlayerPrefs.GetInt("SafeComboY") == 0)
            PlayerPrefs.SetInt("SafeComboY", 9);
        else
            PlayerPrefs.SetInt("SafeComboX", PlayerPrefs.GetInt("SafeComboY") - 1);

        SafeComboYtext.text = (PlayerPrefs.GetInt("SafeComboY")).ToString();
    }
    public void SafeDecZ()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
        if (PlayerPrefs.GetInt("SafeComboZ") == 0)
            PlayerPrefs.SetInt("SafeComboZ", 9);
        else
            PlayerPrefs.SetInt("SafeComboZ", PlayerPrefs.GetInt("SafeComboX") - 1);

        SafeComboZtext.text = (PlayerPrefs.GetInt("SafeComboZ")).ToString();
    }
    public void HideMenu()
    {
        //set all edit uis and the main false
        GameObject[] Menus = GameObject.FindGameObjectsWithTag("EditUI");
        foreach (GameObject Menu in Menus)
        {
            if (Menu.activeSelf)
                Menu.SetActive(false);
        }
        GameObject[] MainMenus = GameObject.FindGameObjectsWithTag("EditUIMain");
        foreach (GameObject MainMenu in MainMenus)
        {
            if (MainMenu.activeSelf)
                MainMenu.SetActive(false);
        }
        //zero out active object
        PlayerPrefs.SetString("activeObject", "NULL");
        PlayerPrefs.SetInt("UIactive", 0);
    }

    public void FreeDrag()
    {
        activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));

        if (!beingHeld)
        {
            rb = activeOBJ.GetComponent<Rigidbody>();
            activeOBJ.transform.position = placementTransform.position;
            activeOBJ.transform.parent = placementTransform;
            rb.isKinematic = true;
            beingHeld = true;
        }
        else if (beingHeld)
        {
            rb.isKinematic = false;
            activeOBJ.transform.parent = GameObject.Find("EditLevel").transform;
            beingHeld = false;
        }
    }
    public void PlayLevel()
    {
        //change parent of respawn object to null to have it active when edit level isnt
        originalParent = SpawnlvlBox.transform.parent;
        SpawnlvlBox.transform.parent = null;
        GameObject.Find("EditLevel").SetActive(false);
        SpawnlvlBox.SetActive(true);

    }
    public void SpawnOBJ()
    {
        //check sprite to prefab
        string spriteName = SpawnButton.image.sprite.name;
        if (SpawnButton.image.sprite.name == spriteName)
        {
            //set target to corresponding prefab and give it needed variables
            targetSpawnOBJ = CoinPrefab;
            targetSpawnOBJ.GetComponent<HoldItem>().destinationTransform = placementTransform;
            // set strings for names
            string EditObjName = string.Concat("SpawnOBJ", SpawnOBjCount);
            string RealObjName = string.Concat("Real", SpawnOBjCount);
            //create edit objects and remove components
            GameObject SpawnedEditObj = Instantiate(targetSpawnOBJ, placementTransform.position, placementTransform.rotation);
            Rigidbody EditObjRB = SpawnedEditObj.GetComponent<Rigidbody>();
            EditObjRB.isKinematic = true;
            EditObjRB.useGravity = false;
            SpawnedEditObj.RemoveComponent<HoldItem>();
            SpawnedEditObj.RemoveComponent<ItemRespawn>();
            //give parent and edit attributes
            SpawnedEditObj.transform.parent = GameObject.Find("EditLevel").transform;
            SpawnedEditObj.name = EditObjName;
            SpawnedEditObj.transform.tag = "EditOBJ";
            SpawnedEditObj.SetActive(true);
            //spawn real equivalent
            GameObject SpawnedRealObj = Instantiate(targetSpawnOBJ, placementTransform.position, placementTransform.rotation);
            SpawnedRealObj.name = RealObjName;
            //parent to inactive real level
            SpawnedRealObj.transform.parent = GameObject.Find("RealLevel").transform;
            
            SpawnOBjCount++;
            //save edit object data
            EditModeOBJ eo = SpawnedEditObj.AddComponent<EditModeOBJ>();
            eo.data.pos = SpawnedEditObj.transform.position;
            eo.data.rot = SpawnedEditObj.transform.rotation;
            eo.data.scale = SpawnedEditObj.transform.localScale;
            eo.data.objectType = EditModeOBJ.ObjectType.RealCoin1;
            eo.data.OBJname = name;
        }
        else if (SpawnButton.image.sprite.name == "PigIcon")
        {
            targetSpawnOBJ = PigPrefab;
            string EditObjName = string.Concat("SpawnOBJ", SpawnOBjCount);
            string RealObjName = string.Concat("Real", SpawnOBjCount);  

            GameObject SpawnedEditObj = Instantiate(targetSpawnOBJ, placementTransform.position, placementTransform.rotation);
            Rigidbody EditObjRB = SpawnedEditObj.GetComponent<Rigidbody>();
            EditObjRB.isKinematic = true;
            EditObjRB.useGravity = false;

            SpawnedEditObj.transform.parent = GameObject.Find("EditLevel").transform;
            SpawnedEditObj.name = EditObjName;
            SpawnedEditObj.transform.tag = "EditOBJ";
            SpawnedEditObj.SetActive(true);

            GameObject SpawnedRealObj = Instantiate(targetSpawnOBJ, placementTransform.position, placementTransform.rotation);
            SpawnedRealObj.name = RealObjName;
            SpawnedRealObj.transform.parent = GameObject.Find("RealLevel").transform;

            //SpawnedRealObj.SetActive(false);
            SpawnOBjCount++;

            EditModeOBJ eo = SpawnedEditObj.AddComponent<EditModeOBJ>();
            eo.data.pos = SpawnedEditObj.transform.position;
            eo.data.rot = SpawnedEditObj.transform.rotation;
            eo.data.scale = SpawnedEditObj.transform.localScale;
            eo.data.objectType = EditModeOBJ.ObjectType.RealPig;
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (ObjHeld)
        {
            activeOBJ = GameObject.Find(PlayerPrefs.GetString("activeObject"));
            Vector3 targetDirection = Vector3.Normalize(placementTransform.position - activeOBJ.transform.position);
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(activeOBJ.transform.forward, targetDirection, singleStep, 0.0f);
            Debug.DrawRay(placementTransform.transform.position, targetDirection, Color.red);
            activeOBJ.transform.rotation = Quaternion.LookRotation(targetDirection) * Quaternion.Euler(0, 90, 0);
            Vector3 arrowAngles = activeOBJ.transform.rotation.eulerAngles;
            arrowAngles.z = 0;
            arrowAngles.x = 0;
            activeOBJ.transform.Translate(placementTransform.position.x, activeOBJ.transform.position.y, placementTransform.transform.position.z);
            activeOBJ.transform.rotation = Quaternion.Euler(arrowAngles);
        }
        */
    }
}
