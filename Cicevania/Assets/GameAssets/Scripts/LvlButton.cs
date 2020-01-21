using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LvlButton : MonoBehaviour
{
    //[SerializeField]
    private int lvl;
    [SerializeField]
    private TextMeshProUGUI txtNumLvl;
    LvlButton btn;

    public void InitBtn(int numLvl, bool active)
    {
        this.lvl = numLvl;
        txtNumLvl.text = lvl.ToString();
        GetComponent<Button>().interactable = active;
    }

    public void BtnLoadLevel()
    {
        GameManager._instance.sceneC.LoadSceneLvl(lvl);
    }

}
