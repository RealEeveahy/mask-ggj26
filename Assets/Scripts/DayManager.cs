using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages interactions that exist purely in the Game Scene
/// </summary>
public class DayManager : MonoBehaviour
{
    public GameObject sceneOverlay, TaskParent;
    public GameObject PlayerDeathSprite;
    public EventHandler<int> OnProgressChanged;
    int _tasksCompleted = 0;
    public int TasksCompleted
    {
        get { return _tasksCompleted; }
        set { 
            _tasksCompleted = value;
            // notify listeners of change in progression (progress bar)
            OnProgressChanged?.Invoke(this, _tasksCompleted);
        }
    }
    public List<string> OpeningDialogue = new List<string>();
    public List<string> GoodDialogue = new List<string>();
    public List<string> BadDialogue = new List<string>();
    bool inDialogue = false;
    bool hasSeenCompleteMessage = false;
    private void Start()
    {
        sceneOverlay.SetActive(false);
        PlayerDeathSprite.SetActive(false);
    }
    public IEnumerator Initiate()
    {
        //wait before showing dialogue
        yield return new WaitForSeconds(3f);
        GlobalManagement.instance.SetMessage(OpeningDialogue[UnityEngine.Random.Range(0, OpeningDialogue.Count)]);
        inDialogue = true;
    }
    public void ShowNextTask()
    {
        inDialogue = false;
        if (TasksCompleted < GlobalManagement.instance.GetDay().tasks.Count)
        {
            sceneOverlay.SetActive(true);
            sceneOverlay.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.8f);

            ITask nextTask = GlobalManagement.instance.GetDay().tasks[TasksCompleted];
            ClearTaskParent();

            string path = nextTask.PrefabPath;

            GameObject toRender = Resources.Load<GameObject>(path);
            GameObject instTask = Instantiate(toRender);
            if (instTask.GetComponent<JuggleBehaviour>() ==null)
            {
                instTask.GetComponent<LuteBehaviour>().task = nextTask;
            }
            else if (instTask.GetComponent<WheelBehaviour>() == null)
            {
                instTask.GetComponent<JuggleBehaviour>().task = nextTask;
            }
            else if (instTask.GetComponent<JuggleBehaviour>() == null)
            {
                instTask.GetComponent<WheelBehaviour>().task = nextTask;
            }
            instTask.transform.SetParent(TaskParent.transform);

            StartCoroutine(ElapseTask(nextTask.Duration));
        }
        else
        {
            GlobalManagement.instance.SetMessage("That's quite enough for one day. Get out of here!");
            inDialogue = true;
            hasSeenCompleteMessage = true;
        }
    }
    public void ClearTaskParent()
    {
        foreach(Transform child in TaskParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    IEnumerator ElapseTask(float time)
    {
        yield return new WaitForSeconds(time);
        ClearTaskParent();
        TasksCompleted++;

        sceneOverlay.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        GlobalManagement.instance.SetMessage(GoodDialogue[UnityEngine.Random.Range(0, GoodDialogue.Count)]);
        inDialogue = true;
    }
    public void OnClick()
    {
        if(inDialogue)
        {
            // clicked during a conversation
            if (GlobalManagement.instance.EndMessage())
            {
                if (hasSeenCompleteMessage)
                {
                    GlobalManagement.instance.DayComplete();
                    inDialogue = false;
                }
                else
                    ShowNextTask();
            }
        }
        else
        {
            // clicked during minigame
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
            if (hit)
            {
                if (hit.transform.gameObject.CompareTag("Pin"))
                    hit.transform.gameObject.SendMessage("Throw");
            }
        }
    }
    public void PlayerDeath()
    {
        TaskParent.SetActive(false);
        PlayerDeathSprite.SetActive(true);
        sceneOverlay.SetActive(true);
        sceneOverlay.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
    }
}
