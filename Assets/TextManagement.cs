using System.Collections;
using UnityEngine;

public class TextManagement : MonoBehaviour
{
    bool busy = false;
    public float timeBetweenPrints = 0.05f;
    private float _printTime;
    public bool isBusy { get { return busy; } }
    public IEnumerator ShowMessage(string message)
    {
        // prevent overlapping calls
        busy = true;
        _printTime = timeBetweenPrints;

        // the current message being displayed on screen
        string showing = "";

        // repeat until messages are identical
        while (showing.Length < message.Length)
        {
            showing += message[showing.Length];
            
            // TODO:
            // update text on screen
            // control for html tags (bold, colour, etc)

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
}
