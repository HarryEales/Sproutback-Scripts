using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool paused;

    public GameObject pausedCanvas;
    public Canvas canvasMenu;

    // Start is called before the first frame update
    void Start()
    {
        canvasMenu = pausedCanvas.GetComponent<Canvas>();
        canvasMenu.enabled = false;
        paused = false;
        pausedCanvas.SetActive(true);
        StartCoroutine(CloseMenuStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                paused = true;
                Time.timeScale = 0;
                pausedCanvas.SetActive(true);
            }
            else if (paused == true)
            {
                paused = false;
                Time.timeScale = 1;
                pausedCanvas.SetActive(false);
            }
        }
    }

    IEnumerator CloseMenuStart()
    {
        yield return new WaitForSeconds(0.1f);
        pausedCanvas.SetActive(false);
        canvasMenu.enabled = true;
    }
}
