using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // Talk Data
        // NPC A : 1000, NPC B : 2000
        // Box: 3000, Desk: 4000
        talkData.Add(1000, new string[] { "안녕?:0"
            , "반가워:1" });
        talkData.Add(2000, new string[] { "뭐야?:0"
            , "할 말이 있는거야?:1"
            , "없으면 말구:2" });

        talkData.Add(3000, new string[] { "데스크엔 이런 말이 있다."
            , "이곳이 다 막혀있다고 생각하는 가?"
            , "새 길을 찾아 따라 가다보면 값진 무언가를 얻을 수 있을 것이다." });
        talkData.Add(4000, new string[] { "상자 속에 냅킨이 하나 있다."
            , "010 - **** - ****\n친구 닮은 사람이예요 ^^"
            , "왠지 모를 옅은 미소가 새어 나왔다." });

        // Quest Talk
        talkData.Add(10 + 1000, new string[] { "어서와:0"
            ,"이 마을에 놀라운 전설이 있다는데:1"
            ,"오른쪽에 있는 루도가 알려줄거야:0" });
        talkData.Add(11 + 2000, new string[] { "여어:1"
            , "이 호수의 전설을 들으러 온거야?:0"
            , "그럼 일 좀 하나 해주면 좋을텐데...:1"
            , "이 곳 어딘가에 있는 떨어진 동전 좀 주워줬으면 좋겠어.:2" });

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1"
            ,"돈을 흘리고 다니면 못쓰지!:3"
            ,"나중에 루도에게 한마디 해야겠어.:3"});
        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘:1" });
        talkData.Add(20 + 5000, new string[] { "동전을 찾았다"
            ,"이런곳에 있었군.."});
        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워.:2" });

        // Portrait Data
        // 0: Normal, 1: Speak, 2: Happy, 3: Angry
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex); // Get First Talk            
            else
                return GetTalk(id - id % 10, talkIndex); // Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
