using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a group of scenes that make up a cutscene.
/// </summary>
[CreateAssetMenu(fileName = "CutsceneGroup", menuName = "Scriptable Objects/CutsceneGroup")]
public class CutsceneGroup : ScriptableObject
{
    public List<IScene> sceneList = new List<IScene>();
    /// <summary>
    /// Not used in game in any way.
    /// Please write any notes about the expected use of this cutscene, i.e. sanity level, preceding events
    /// </summary>
    [TextArea]
    public string description;
}
