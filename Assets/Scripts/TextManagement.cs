using System.Collections;
using UnityEngine;
using TMPro;
public class TextManagement : MonoBehaviour
{
    bool busy = false;
    float timeBetweenPrints = 0.01f;
    private float _printTime;
    public bool isBusy { get { return busy; } }
    public TMP_Text ConversationTextField;
    public GameObject textParent;
    bool soundCooldown = false;
    private void Awake()
    {
        if(ConversationTextField == null)
        {
            ConversationTextField = FindFirstObjectByType<TMP_Text>(FindObjectsInactive.Include);
        }
    }
    public IEnumerator ShowMessage(string message, string speaker = null)
    {
        // prevent overlapping calls
        busy = true;
        if (ConversationTextField == null) { ConversationTextField = FindFirstObjectByType<TMP_Text>(FindObjectsInactive.Include); }
        if (textParent == null) { textParent = ConversationTextField.gameObject.transform.parent.parent.gameObject; }
        textParent.SetActive(true);
        _printTime = timeBetweenPrints;

        // the current message being displayed on screen
        string showing = "";

        // repeat until messages are identical
        while (showing.Length < message.Length)
        {
            showing += message[showing.Length];
            
            // TODO:
            ConversationTextField.text = showing;
            // control for html tags (bold, colour, etc)

            //play a pitch randomised sound
            if (!soundCooldown && speaker != null)
            {
                GlobalManagement.instance.PlaySound(speaker, GlobalManagement.SoundType.SFX, true);
                StartCoroutine(SoundCD());
            }

            yield return new WaitForSeconds(_printTime);
        }

        busy = false;
    }
    /// <summary>
    /// Temporarily speeds up the text engine by cutting the print time in half.
    /// </summary>
    public void Hurry()
    {
        _printTime = timeBetweenPrints / 2;
    }

    public void Close()
    {
        busy = false;
        textParent.SetActive(false);
    }
    IEnumerator SoundCD()
    {
        soundCooldown = true;
        yield return new WaitForSeconds(0.15f);
        soundCooldown = false;
    }
}
