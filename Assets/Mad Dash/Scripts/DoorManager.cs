using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorGroupWrapper
{
    public List<Door> DoorGroup = new List<Door>();
}

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    public List<DoorGroupWrapper> doorGroups = new List<DoorGroupWrapper>();

    [SerializeField, Tooltip("The time it takes for the open doors to switch.")] 
    private float doorSwitchTime;

    private bool switchDoorsComplete = true;

    private void Update()
    {
        if (switchDoorsComplete)
        {
            StartCoroutine(SwitchDoors());
            switchDoorsComplete = false;
        }
    }

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
