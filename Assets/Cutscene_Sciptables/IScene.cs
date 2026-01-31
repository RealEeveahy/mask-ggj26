using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a single scene in an overall cutscene, 
/// with one image and a set of messages that are to be shown during that images lifetimme
/// </summary>
[CreateAssetMenu(fileName = "IScene", menuName = "Scriptable Objects/IScene")]
public class IScene : ScriptableObject
{
    public Sprite sceneImage;
    [TextArea]
    public List<string> messages = new List<string>();
}
