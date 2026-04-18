using System.Collections.Generic;

[System.Serializable]
public class PostData
{
    public string username;
    public string text;
    public string platform;
    public int night;
}

[System.Serializable]
public class PostDatabase
{
    public List<PostData> posts;
}