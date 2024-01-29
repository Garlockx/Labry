using UnityEngine;
using System.Collections.Generic;

public class W1L8Rules : MonoBehaviour
{
    GameObject currentTrap;
    List<GameObject> trapList = new List<GameObject>();

    private void Start()
    {
        createNewTrap();
    }

    // Update is called once per frame
    void Update()
    {
        deleteTrap();
        if (currentTrap.transform.position.x > 3.5f)
        {
            createNewTrap();
        }
    }

    private void createNewTrap()
    {
        currentTrap = Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(1.5f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        currentTrap.GetComponent<TrapOne>().trapSpeed = 6.0f;
        currentTrap.GetComponent<TrapOne>().trapIsMovable = true;
        currentTrap.GetComponent<TrapOne>().startPosition = new Vector3(1.5f, 0.5f, 0.0f);
        currentTrap.GetComponent<TrapOne>().firstPosition = new Vector3(25.5f, 0.5f, 0.0f);
        currentTrap.GetComponent<TrapOne>().secondPosition = new Vector3(25.5f, 0.5f, 0.0f);
        trapList.Add(currentTrap);
    }

    private void deleteTrap()
    {
        if (trapList[0].transform.position == trapList[0].GetComponent<TrapOne>().firstPosition)
        {
            GameObject trapToRemove = trapList[0];
            Destroy(trapList[0].gameObject);
            trapList.Remove(trapToRemove);
        }
    }
}
