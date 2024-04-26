using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Mainmenu Control")]
public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Mainmenu scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickOriginal()
    {
        Debug.Log("Load Original scene");
        SceneManager.LoadSceneAsync("Original");
    }

    public void OnClickAnchoring()
    {
        Debug.Log("Load Original scene");
        SceneManager.LoadSceneAsync("Anchoring");
    }

    public void OnClickHinge()
    {
        Debug.Log("Load Original scene");
        SceneManager.LoadSceneAsync("HingeAware");
    }
}