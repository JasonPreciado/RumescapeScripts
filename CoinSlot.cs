using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSlot : MonoBehaviour
{
    private GameObject key;
    private GameObject coinSlot;
    public Transform keySpawn;
    private int numCoins = 0;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        key = GameObject.Find("PiggyBank/Pedestal/KeySpawn/Key");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider col)
    {
        Rigidbody rb = col.GetComponent<Rigidbody>();
        if (col.gameObject.tag == "coin" )//&& rb.velocity.magnitude > 0f)
        {
            col.gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Play();
            numCoins++;
            Debug.Log("Number of Coins: " + numCoins);
            if (numCoins > 2)
                key.SetActive(true);
                
        }
    }
}
