using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRealLevel : MonoBehaviour
{
    private GameObject Building;
    [SerializeField]
    private GameObject RespawnPedParent;
    public Vector3 BuildingPos;
    public Quaternion BuildingRot;
    private GameObject NewLevel;
    [SerializeField]
    private GameObject Arrow;
    public Vector3 RespawnPos;
    public Quaternion RespawnRot;

    void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.tag == "Player")
		{
            //find the current levels
            Building = GameObject.Find("EditLevel");
            NewLevel = GameObject.Find("RealLevel");
            //Set building to active and parent this gameobject under it
            Building.SetActive(true);
			this.transform.SetParent(Building.transform);
            //set real level active and put all items to the edit equivalents
            NewLevel.SetActive(true);
			GameObject RealObject;
			string RealName;
			GameObject[] EditedObjects = GameObject.FindGameObjectsWithTag("EditOBJ");
			foreach (GameObject eobj in EditedObjects)
			{
				RealName = string.Concat("Real", eobj.name);
				RealObject = GameObject.Find(RealName);
				RealObject.transform.position = eobj.transform.position;
				RealObject.transform.rotation = eobj.transform.rotation;
				RealObject.transform.localScale = eobj.transform.localScale;
			}
            //set edit level false
            Building.SetActive(false);

		}
	}
	// Update is called once per frame
	void Update()
	{
		RespawnPos = GameObject.FindGameObjectWithTag("PlaceInteract").GetComponent<PlaceBuildingInteraction>().finalBuildingPos;
		RespawnRot = GameObject.FindGameObjectWithTag("PlaceInteract").GetComponent<PlaceBuildingInteraction>().finalBuildingRot;
		//RespawnPedParent = Transform.Fin("FailMenu").GetComponent<FailMenuInteraction>().originalParent;

	}
}
