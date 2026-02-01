using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

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
    public float spinStrength = 500f;
    public float idleSpeed = 10f;
    public float spawnRadius = 10f;

    void Start()
    {
        StartCoroutine(StartTask());
        rigidBody = playerObject.GetComponent<Rigidbody2D>();
    }
    float GetXPosition()
    {
        float mx = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
        return mx;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rigidBody.angularVelocity = GetXPosition() * idleSpeed;
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
        instantiatedSword.transform.SetParent(transform, true);
        int randomn = Random.Range(0, targets.Count); 
        instantiatedSword.GetComponent<Sword>().swordLandingPoint = targets[randomn];
        Vector2 differenceVector = targets[randomn].position - playerObject.transform.position;
        instantiatedSword.transform.position = differenceVector* spawnRadius;
        instantiatedSword.GetComponent<Sword>().creatorObject = this.gameObject;
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
        
        Debug.Log($"WheelBehaviour.OnClick");
        SpinWheel();
    }
    public void SpinWheel()
    {

        rigidBody.AddTorque(spinStrength, ForceMode2D.Impulse);
    }
}
