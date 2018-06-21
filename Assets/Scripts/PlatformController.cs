using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public GameObject[] cylinders;
    int currentCylinder = 4;
    bool started = false;
    
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R) && !started)
        {
            startRepeating();
            started = true;
        }
    }

    void startRepeating()
    {
        InvokeRepeating("DropPlatform", 2.0f, 5f);
    }

    void DropPlatform()
    {
        if (currentCylinder >= 0)
        {
            StartCoroutine(MoveOverSeconds(cylinders[currentCylinder], new Vector3(0.0f, -10f, 0f), 5f));
            currentCylinder--;
        }
        else
        {
            CancelInvoke();
        }
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }
}
