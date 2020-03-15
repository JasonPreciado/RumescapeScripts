using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CoinPrefab;
    [SerializeField]
    private GameObject PigPrefab;
    [SerializeField]
    private GameObject BuildingPrefab;
    [SerializeField]
    private GameObject PedestalPrefab;

    public Transform placementTransform;
    private GameObject EditLevel;
    private GameObject RealLevel;
    private LevelEditList level;
    public string LevelSaveName;
    public string LevelLoadName;



    LevelEditList CreateEditor()
    {
        // make new list of editmodeobj data
        level = new LevelEditList();
        level.editorObjects = new List<EditModeOBJ.Data>(); 
        return level;
    }
    public void SaveLevel()
    {
        // find all edit mode objects
        EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
        foreach (EditModeOBJ obj in foundObjects)
        {
            // add objects to list
            level.editorObjects.Add(obj.data);
        }
        // write level list to json file
        string EditFilejson = JsonUtility.ToJson(level);
        // make folder
        string EditLevelsFolder = Application.dataPath + "/LevelData/"; 
        string EditLevelFileName = "";

        //default name if blank
        if (LevelSaveName == "")
        {
            //if user has never made a level set to 1 otherwise increment
            if (!PlayerPrefs.HasKey("LevelsMade"))
                PlayerPrefs.SetInt("LevelsMade", 1);
            else
                PlayerPrefs.SetInt("LevelsMade", PlayerPrefs.GetInt("LevelsMade") + 1);

            //set default name 
            EditLevelFileName = string.Concat("edit_level", PlayerPrefs.GetInt("LevelsMade"), ".json");
        }
        else
            EditLevelFileName = LevelSaveName + ".json";

        //make directory to folder if not there
        if (!Directory.Exists(EditLevelsFolder))
            Directory.CreateDirectory(EditLevelsFolder);

        // set path to file
        string PathToEditFile = Path.Combine(EditLevelsFolder, EditLevelFileName);

        //delete file of same name if exists
        if (File.Exists(PathToEditFile))
        {

            File.Delete(PathToEditFile);

        }
        //make and save file
        File.WriteAllText(PathToEditFile, EditFilejson);
        //zero out save name
        LevelSaveName = ""; 
    }

    public void LoadLevel()
    {
        //set folder
        string EditLevelFolder = Application.dataPath + "/LevelData/";

        string EditLevelFile = "";
        //set a default file name if no name found
        if (LevelLoadName == "")
        {
            EditLevelFile = string.Concat("edit_level",PlayerPrefs.GetInt("LevelsMade"),".json");
        }
        else
            EditLevelFile = LevelLoadName + ".json";

        // set path to edit file
        string PathToEditFile = Path.Combine(EditLevelFolder, EditLevelFile);

        // if the file found in folder
        if (File.Exists(PathToEditFile)) 
        {
            // delete current level objs
            EditModeOBJ[] foundObjects = FindObjectsOfType<EditModeOBJ>();
            foreach (EditModeOBJ obj in foundObjects)
                Destroy(obj.gameObject);

            //delete current level emptys
            Destroy(GameObject.Find("EditLevel"));
            Destroy(GameObject.Find("RealLevel"));

            // read from edit file
            string LevelEditjson = File.ReadAllText(PathToEditFile);

            // make list of read info
            level = JsonUtility.FromJson<LevelEditList>(LevelEditjson); 


            CreateGamestateFromFile(); 
        }
        else // file not found
        {
            //shit
        }
    }

    // create objects based on data within level.
    void CreateGamestateFromFile()
    {
        //objects for spawning
        GameObject EditObj;
        GameObject RealOBJ;
        //emptys for setting building parenting
        EditLevel = new GameObject();
        RealLevel = new GameObject();
        for(int editcount = 0; editcount < level.editorObjects.Count; editcount++)
        {
            if(level.editorObjects[editcount].objectType == EditModeOBJ.ObjectType.Room)
            {
                //put position of edit level empty to building position from file
                EditLevel.transform.position = level.editorObjects[editcount].pos;
                EditLevel.transform.rotation = level.editorObjects[editcount].rot;
                EditLevel.transform.localScale = level.editorObjects[editcount].scale;
                //change name of empty for re-editing 
                EditLevel.name = "EditLevel";
                //set parent for set active states 
                EditLevel.transform.parent = GameObject.Find("LevelCreater").transform;

                //put position of edit level empty to building position from file
                RealLevel.transform.position = level.editorObjects[editcount].pos;
                RealLevel.transform.rotation = level.editorObjects[editcount].rot;
                RealLevel.transform.localScale = level.editorObjects[editcount].scale;
                //change name of empty for re-editing and setting active on play
                RealLevel.name = "RealLevel";
                //set parent for set active states 
                RealLevel.transform.parent = GameObject.Find("LevelCreater").transform;
                //set false until test/play
                RealLevel.SetActive(false);

                //spawn real building and set transforms  
                EditObj = Instantiate(BuildingPrefab);
                EditObj.transform.position = level.editorObjects[editcount].pos;
                EditObj.transform.rotation = level.editorObjects[editcount].rot;
                EditObj.transform.localScale = level.editorObjects[editcount].scale;
                EditObj.name = level.editorObjects[editcount].OBJname;

                //set building parent for spawning level
                EditObj.transform.parent = EditLevel.transform;
                //EditLevel.transform = EditObj.transform;

                //spawn real building and set transforms;
                RealOBJ = Instantiate(BuildingPrefab);
                RealOBJ.transform.position = level.editorObjects[editcount].pos;
                RealOBJ.transform.rotation = level.editorObjects[editcount].rot;
                RealOBJ.transform.localScale = level.editorObjects[editcount].scale;
                //set to false and parenting
                RealOBJ.transform.parent = RealLevel.transform;

                //save data for edit obj
                EditModeOBJ eo = EditObj.AddComponent<EditModeOBJ>();
                eo.data.pos = EditObj.transform.position;
                eo.data.rot = EditObj.transform.rotation;
                eo.data.objectType = EditModeOBJ.ObjectType.Room;
            }
        }

        for (int i = 0; i < level.editorObjects.Count; i++)
        {
            if (level.editorObjects[i].objectType == EditModeOBJ.ObjectType.RealCoin1) 
            {
                //spawn edit coin with transforms
                EditObj = Instantiate(CoinPrefab);
                EditObj.transform.position = level.editorObjects[i].pos; 
                EditObj.transform.rotation = level.editorObjects[i].rot;
                EditObj.transform.localScale = level.editorObjects[i].scale;
                EditObj.name = level.editorObjects[i].OBJname;
                //set parent for spawning level
                EditObj.transform.parent = EditLevel.transform;
                //remove scripts from edit obj
                EditObj.RemoveComponent<HoldItem>();
                EditObj.RemoveComponent<ItemRespawn>();

                //spawn real obj
                RealOBJ = Instantiate(CoinPrefab);
                //set script variables
                RealOBJ.GetComponent<HoldItem>().destinationTransform = placementTransform;
                //set transforms
                RealOBJ.transform.position = level.editorObjects[i].pos;
                RealOBJ.transform.rotation = level.editorObjects[i].rot;
                RealOBJ.transform.localScale = level.editorObjects[i].scale;   
                RealOBJ.name = string.Concat("Real", level.editorObjects[i].OBJname);
                //set false and parent
                RealOBJ.transform.parent = RealLevel.transform;

                //save data for edit obj
                EditModeOBJ eo = EditObj.AddComponent<EditModeOBJ>();
                eo.data.pos = EditObj.transform.position;
                eo.data.rot = EditObj.transform.rotation;
                eo.data.objectType = EditModeOBJ.ObjectType.RealCoin1;
            }
            else if (level.editorObjects[i].objectType == EditModeOBJ.ObjectType.Pedestal)
            {
                //spawn edit pedestal with transforms
                EditObj = Instantiate(PedestalPrefab);
                EditObj.transform.position = level.editorObjects[i].pos;
                EditObj.transform.rotation = level.editorObjects[i].rot;
                EditObj.transform.localScale = level.editorObjects[i].scale;
                EditObj.name = level.editorObjects[i].OBJname;
                //set parent for spawning level
                EditObj.transform.parent = EditLevel.transform;

                //make real pedestal with transforms
                RealOBJ = Instantiate(PedestalPrefab);
                RealOBJ.transform.position = level.editorObjects[i].pos;
                RealOBJ.transform.rotation = level.editorObjects[i].rot;
                RealOBJ.transform.localScale = level.editorObjects[i].scale;
                RealOBJ.name = string.Concat("Real", level.editorObjects[i].OBJname);

                //save data for editobj
                EditModeOBJ eo = EditObj.AddComponent<EditModeOBJ>();
                eo.data.pos = EditObj.transform.position;
                eo.data.rot = EditObj.transform.rotation;
                eo.data.objectType = EditModeOBJ.ObjectType.Pedestal;
            }
            else if (level.editorObjects[i].objectType == EditModeOBJ.ObjectType.RealPig)
            {
                //make edit pig with transforms
                EditObj = Instantiate(PigPrefab);
                EditObj.transform.position = level.editorObjects[i].pos;
                EditObj.transform.rotation = level.editorObjects[i].rot;
                EditObj.transform.localScale = level.editorObjects[i].scale;
                EditObj.name = level.editorObjects[i].OBJname;
                //set parent for spawning level
                EditObj.transform.parent = EditLevel.transform;

                //make real pig with transforms
                RealOBJ = Instantiate(PigPrefab);
                RealOBJ.transform.position = level.editorObjects[i].pos;
                RealOBJ.transform.rotation = level.editorObjects[i].rot;
                RealOBJ.transform.localScale = level.editorObjects[i].scale;
                RealOBJ.name = string.Concat("Real", level.editorObjects[i].OBJname);

                //save data for edit obj
                EditModeOBJ eo = EditObj.AddComponent<EditModeOBJ>();
                eo.data.pos = EditObj.transform.position;
                eo.data.rot = EditObj.transform.rotation;
                eo.data.objectType = EditModeOBJ.ObjectType.Pedestal;
            }
            /*
            else if (level.editorObjects[i].objectType == EditModeOBJ.ObjectType.Something)
            {

            }
            else if (level.editorObjects[i].objectType == EditModeOBJ.ObjectType.Something)
            {

            }
            else if (level.editorObjects[i].objectType == EditModeOBJ.ObjectType.Something)
            {

            }
            */

        }
        //set new building and place them
        GameObject.Find("PlaceBuildingInteraction").GetComponent<PlaceBuildingEdit>().Building = EditLevel;
        GameObject.Find("PlaceBuildingInteraction").GetComponent<PlaceBuildingEdit>().RealLevel = RealLevel;
        GameObject.Find("PlaceBuildingInteraction").GetComponent<PlaceBuildingEdit>().placementOver = false;
        GameObject.Find("PlaceBuildingInteraction").GetComponent<PlaceBuildingEdit>().redoPlacement();
        // set spawn obj count for re-editing
        GameObject.Find("EditInteraction").GetComponent<EditItemTranslations>().SpawnOBjCount = level.editorObjects.Count+1; 
    }
}
