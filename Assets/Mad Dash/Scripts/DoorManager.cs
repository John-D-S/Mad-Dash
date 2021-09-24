using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this exists so that lists of lists of doors can be shown in the inspector
/// </summary>
[System.Serializable]
public class DoorGroupWrapper
{
    public List<Door> DoorGroup = new List<Door>();
}

public class DoorManager : MonoBehaviour
{
    [SerializeField, Tooltip("group the doors into islands of accessable terrain, since only one of each group will open at any given time.")]
    public List<DoorGroupWrapper> doorGroups = new List<DoorGroupWrapper>();

    [SerializeField, Tooltip("The time it takes for the open doors to switch.")] 
    private float doorSwitchTime;

    private bool switchDoorsComplete = true;

    private void Update()
    {
        // the switch doors coroutine sets switchdoorscomplete to true when it finishes.
        //if it has finished, start it again to continue the loop.
        if (switchDoorsComplete)
        {
            StartCoroutine(SwitchDoors());
            switchDoorsComplete = false;
        }
    }

    /// <summary>
    /// randomise the open and closed doors so that a random door from each door group is open and the rest are closed.
    /// </summary>
    private IEnumerator SwitchDoors()
    {
        switchDoorsComplete = false;
        foreach (DoorGroupWrapper doorGroupWrapper in doorGroups)
        {
            List<Door> doorGroup = doorGroupWrapper.DoorGroup;
            int doorIndexToOpen = Random.Range(0, doorGroup.Count);
            int currentDoorIndex = 0;
            foreach (Door door in doorGroup)
            {
                door.open = false;
                door.SetColor(Color.red);
                if (currentDoorIndex == doorIndexToOpen)
                {
                    door.open = true;
                    door.SetColor(Color.green);
                }
                currentDoorIndex += 1;
            }
        }
        yield return new WaitForSeconds(doorSwitchTime);
        switchDoorsComplete = true;
    }
}
