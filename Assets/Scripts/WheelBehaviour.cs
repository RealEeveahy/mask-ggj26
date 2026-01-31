using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class WheelBehaviour : MonoBehaviour
{
    public ITask task;
    public GameObject swordPrefab;
    public Transform spawnPosition = null;
    public List<Transform> targets = new List<Transform>();
    public float spawnTime = 1f;
    public int maxNumberOfSwords = 3;
    public List<GameObject> swords = new List<GameObject>();
    public GameObject playerObject = null;
    public Rigidbody2D rigidBody = null;
    public float spinStrength = 10f;

    void Start()
    {
        spawnPosition = transform;
        StartCoroutine(StartTask());
        rigidBody = playerObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //task.CurrentDuration += Time.deltaTime;
        //if (task.CurrentDuration >= task.Duration)
        //{
        //    task.TaskComplete = true;
        //    //Day.Equals=
        //}
    }
    public GameObject CreateSword()
    {
        Debug.Log("WheelTask.CreateSword()");
        if (swordPrefab == null)
        {
            Debug.Log("No sword prefab assigned");
            return null;
        }
        GameObject instantiatedSword = Instantiate(swordPrefab, spawnPosition);
        instantiatedSword.transform.SetParent(transform, false);
        instantiatedSword.GetComponent<FallingObject>().creatorObject = this.gameObject;
        int randomn = Random.Range(0, maxNumberOfSwords); 
        instantiatedSword.transform.GetComponent<Sword>().swordLandingPoint = targets[randomn]; // how to do this a better way?
        return instantiatedSword;
    }
    IEnumerator StartTask()
    {
        while (swords.Count<maxNumberOfSwords)
        {
            yield return new WaitForSeconds(spawnTime);
            swords.Add(CreateSword());
        }
    }
    public void OnClick()
    {
        SpinWheel();
    }
    public void SpinWheel()
    {

        rigidBody.AddTorque(spinStrength);
    }
}
