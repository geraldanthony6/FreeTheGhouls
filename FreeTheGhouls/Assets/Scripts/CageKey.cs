using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageKey : MonoBehaviour
{
    [SerializeField]private GameObject cageCeiling;
    [SerializeField]private GameObject levelDoor;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            cageCeiling.gameObject.SetActive(false);
            levelDoor.gameObject.SetActive(true);
        }
    }
}
