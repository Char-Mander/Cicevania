using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    CheckPoint[] cpList; // 0 default CP

    private void Start()
    {
        cpList = FindObjectsOfType<CheckPoint>();
    }

    public void ActiveCP(CheckPoint cp)
    {
        foreach(CheckPoint checkPoint in cpList)
        {
            checkPoint.SetAsCurrentCP(false);
        }
        cp.SetAsCurrentCP(true);
    }

    public void Respawn()
    {
        foreach (CheckPoint checkPoint in cpList)
        {
            if (checkPoint.IsActive())
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Health>().GainHealth(100000000);
                player.transform.position = checkPoint.GetSpawnPoint().position;
                player.GetComponent<PlayerController>().GodModeOn(2);
                if(FindObjectOfType<BlockMechanism>() != null) FindObjectOfType<BlockMechanism>().EnableOrDisableElements(false);
                GameManager._instance.sound.PlayMainTheme();
            }
        }
    }
}
