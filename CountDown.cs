using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{

    [SerializeField]
    public GameObject FailMenu;
    [SerializeField]
    public GameObject VisualCount;
    public float timeLeft = 5.0f;
    public Text startText; // used for showing countdown from 3, 2, 1
    public bool InRoom;
    public bool levelFail;

    private void Start()
    {
        InRoom = false;
        levelFail = false;
        //PlayerPrefs.SetFloat("TimeLeft", 10.0F);
        //timeLeft = PlayerPrefs.GetFloat("TimeLeft", 20.0f);
    }

    private void Countdown ()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            VisualCount.SetActive(false);

//            FailMenu.SetActive(true);
            levelFail = true;
        }
    }
	void Update()
	{
        //InRoom = GameObject.FindGameObjectWithTag("DoorIn").GetComponent<DoorIn>().coundownActive;
        if (levelFail == true)
            return;
        if (VisualCount.activeSelf)
            Countdown();

    }
}
