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
    public static DayManager instance;
    public GameObject sceneOverlay, TaskParent;
    public GameObject PlayerDeathSprite;
    int tasksCompleted = 0;
    //public int currentDay= 1;
    //public int numberOfDays = 5;
    //public List<Day> days = new List<Day>();
    //public bool currentPhaseComplete = false;
    //public bool currentDayComplete = false;
    //public PlayerData player;
    private void Start()
    {
        sceneOverlay.SetActive(false);
        PlayerDeathSprite.SetActive(false);
    }
    public void ShowNextTask()
    {
        if (tasksCompleted < GlobalManagement.instance.GetDay().tasks.Count)
        {
            sceneOverlay.SetActive(true);
            sceneOverlay.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.8f);

            ITask nextTask = GlobalManagement.instance.GetDay().tasks[tasksCompleted];
            ClearTaskParent();

            string path = nextTask.PrefabPath;

            GameObject toRender = Resources.Load<GameObject>(path);
            GameObject instTask = Instantiate(toRender);
            if (instTask.GetComponent<JuggleBehaviour>() ==null)
            {
                instTask.GetComponent<LuteBehaviour>().task = nextTask;
            }
            else
            {
                instTask.GetComponent<JuggleBehaviour>().task = nextTask;
            }
            instTask.transform.SetParent(TaskParent.transform);

            StartCoroutine(ElapseTask(nextTask.Duration));
        }
        else
        {
            // signal end of tasks for day
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
        ShowNextTask();
    }
    public void OnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        if (hit)
        {
            if(hit.transform.gameObject.CompareTag("FallingObject"))
            hit.transform.gameObject.SendMessage("Throw");
        }
    }
    public void PlayerDeath()
    {
        PlayerDeathSprite.SetActive(true);
        sceneOverlay.SetActive(true);
        sceneOverlay.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
    }
}
