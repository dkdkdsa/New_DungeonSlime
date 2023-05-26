using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpeakStage
{
    public List<string> _peaks;
}

public class SpeakManager : MonoBehaviour
{
    [SerializeField] private List<SpeakStage> speak;
    [SerializeField] GameObject speakWindow;
    Camera mainCam;
    Canvas canvas;
    PlayerInput playerInput;
    MapManager mapManager;
    Text text;

    private RectTransform window;
    public bool canToking;
    int nowStage;
    int speakCnt;

    public void StartScritp()
    {
        mainCam = Camera.main;
        canvas = FindObjectOfType<Canvas>();
        window = Instantiate(speakWindow, canvas.transform).GetComponent<RectTransform>();
        Debug.Log("�ù�");
        text = window.GetChild(0).GetComponent<Text>();
        playerInput = FindObjectOfType<PlayerInput>();
        mapManager = FindObjectOfType<MapManager>();
        nowStage = mapManager.currentStageNum;
    }

    private void Update()
    {
        if (canToking)
        {
            WindowUp();
            TokingEnd();
            Toking();
        }
    }

    void WindowUp()
    {
        Vector3 windowVec = new Vector3(transform.position.x, transform.position.y + 1, 0);
        Vector3 windowPos = mainCam.WorldToScreenPoint(windowVec);
        window.position = windowPos;

    }

    void TokingEnd()
    {
        if (speak[nowStage - 1]._peaks.Count <= speakCnt)
        {
            Debug.Log("���ӵ尡��");
            canToking = false;
            playerInput.enabled = true;
            //Destroy(window);
            //���� ����
        }
    }

    public void Toking()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canToking)
        {
            StartCoroutine(Speak(speak[nowStage - 1]._peaks[speakCnt]));
            speakCnt++;
        }
    }

    IEnumerator Speak(string s)
    {
        text.text = "";
        for (int i = 0; i < s.Length; i++)
        {
            text.text += s[i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
