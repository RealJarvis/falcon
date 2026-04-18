using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FeedManager : MonoBehaviour
{
    public GameObject postPrefab;
    public Transform lumenContent;
    public Transform boardContent;
    public Image header;
    public TextMeshProUGUI headerText;
    public TextAsset postsJson;

    private int currentNight = 1;

    void Start()
    {
        LoadPosts();
        ShowLumen();
    }

    void LoadPosts()
    {
        PostDatabase db = JsonUtility.FromJson<PostDatabase>(postsJson.text);

        foreach (PostData post in db.posts)
        {
            if (post.night != currentNight) continue;

            Transform parent = post.platform == "lumen" ? lumenContent : boardContent;
            GameObject obj = Instantiate(postPrefab, parent);

            TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = post.username;
            texts[1].text = post.text;

            if (post.platform == "lumen")
                texts[0].color = new Color(0.67f, 0.67f, 1f);
            else
                texts[0].color = new Color(1f, 0.13f, 0.27f);
        }
    }

    public void ShowLumen()
    {
        lumenContent.gameObject.SetActive(true);
        boardContent.gameObject.SetActive(false);
        header.color = new Color(0.11f, 0.31f, 0.85f);
        headerText.text = "LUMEN";
    }

    public void ShowBoard()
    {
        lumenContent.gameObject.SetActive(false);
        boardContent.gameObject.SetActive(true);
        header.color = new Color(1f, 0.13f, 0.27f);
        headerText.text = "b0ard";
    }

    public void NextNight()
    {
        currentNight++;
        foreach (Transform child in lumenContent) Destroy(child.gameObject);
        foreach (Transform child in boardContent) Destroy(child.gameObject);
        LoadPosts();
    }
}