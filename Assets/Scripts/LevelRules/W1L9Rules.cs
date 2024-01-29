using System.Collections.Generic;
using UnityEngine;

public class W1L9Rules : MonoBehaviour
{
    GameObject currentTrapOne;
    List<GameObject> trapOneList = new List<GameObject>();

    private void Start()
    {
        Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(5.0f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(10.0f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(15.0f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(20.0f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(25.0f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        createNewTrapOne();
    }

    // Update is called once per frame
    void Update()
    {
        deleteTrapOne();
        if (currentTrapOne.transform.position.x > 6.5f)
        {
            createNewTrapOne();
        }
    }

    private void createNewTrapOne()
    {
        currentTrapOne = Instantiate((GameObject)Resources.Load("Prefabs/Trap1", typeof(GameObject)), new Vector3(1.5f, 0.5f, 0.0f), transform.rotation * Quaternion.Euler(0f, 0.0f, -90f));
        currentTrapOne.GetComponent<TrapOne>().trapSpeed = 4.0f;
        currentTrapOne.GetComponent<TrapOne>().trapIsMovable = true;
        currentTrapOne.GetComponent<TrapOne>().startPosition = new Vector3(1.5f, 0.5f, 0.0f);
        currentTrapOne.GetComponent<TrapOne>().firstPosition = new Vector3(25.5f, 0.5f, 0.0f);
        currentTrapOne.GetComponent<TrapOne>().secondPosition = new Vector3(25.5f, 0.5f, 0.0f);
        trapOneList.Add(currentTrapOne);
    }

    private void deleteTrapOne()
    {
        if (trapOneList[0].transform.position == trapOneList[0].GetComponent<TrapOne>().firstPosition)
        {
            GameObject trapToRemove = trapOneList[0];
            Destroy(trapOneList[0].gameObject);
            trapOneList.Remove(trapToRemove);
        }
    }
}
