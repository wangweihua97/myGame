
using Item;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIMgr : MonoBehaviour
{
    public static UIMgr instance;
    public GameObject healthyUI;

    private void Awake()
    {
        instance = this;
    }
    
    public GameObject CreatHealthyUI()
    {
        GameObject go = Instantiate(healthyUI);
        go.transform.SetParent(transform);
        return go;
    }


}