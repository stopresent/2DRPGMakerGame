﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Image portraitImg;
    public Sprite prePortrait;
    public Animator portraitAnim;
    public TypeEffect talk;
    public Text questText;
    public GameObject menuSet;
    public GameObject scanObject;
    public GameObject player;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    private void Update()
    {
        // Sub Menu
        if (Input.GetButtonDown("Cancel"))
            SubMenuActive();
    }

    public void SubMenuActive()
    {
        if (menuSet.activeSelf)
            menuSet.SetActive(false);
        else
            menuSet.SetActive(true);
    }

    public void Action(GameObject scanObj)
    {
        // Get Current Object
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        // Visible Talk For Action
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        // Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        }

        // End Talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            return;
        }

        // Continue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            // Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            // Animation Portrait
            if (prePortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prePortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            // Hide Portrait
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        // Next Talk
        isAction = true;
        talkIndex++;
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt ("QuestId", questManager.questId);
        PlayerPrefs.SetInt ("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
