using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    PlayerJump playerJump;
    PlayerInput playerInput;
    GameObject player;
    MapManager mapManager;
    TextMeshProUGUI text;

    private RectTransform window;
    public bool canToking;
    int nowStage;
    int speakCnt;

    public void StartScript()
    {
        mainCam = Camera.main;
        canvas = FindObjectOfType<Canvas>();
        window = Instantiate(speakWindow, canvas.transform).GetComponent<RectTransform>();
        text = window.GetChild(0).GetComponent<TextMeshProUGUI>();
        playerJump = FindObjectOfType<PlayerJump>();
        playerInput = FindObjectOfType<PlayerInput>();
        player = playerInput.gameObject;
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
        Vector3 windowVec = new Vector3(player.transform.position.x, player.transform.position.y + 2, 0);
        Vector3 windowPos = mainCam.WorldToScreenPoint(windowVec);
        window.position = windowPos;
    }

    void TokingEnd()
    {
        if (speak[nowStage - 1]._peaks.Count <= speakCnt && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("���ӵ尡��");
            canToking = false;
            playerInput.enabled = true;
            playerJump.AddEvent();
            window.gameObject.SetActive(false);
            //���� ����
        }
    }

    public void Toking()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && canToking) || speakCnt == 0)
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