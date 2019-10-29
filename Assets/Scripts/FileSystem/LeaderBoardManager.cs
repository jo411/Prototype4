using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LeaderBoardManager : MonoBehaviour
{

    public string path = "Assets/Resources/Leaderboard/score.txt";
    int numScores = 3;
    List<score> scores = new List<score>();
    // Start is called before the first frame update
    void Start()
    {
        readScores();
        //Debug.Log(getDisplayStringForAllScores());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string getDisplayStringForAllScores()
    {
        sortAndTruncateScores();
        StringBuilder sb = new StringBuilder();
        foreach (score current in scores)
        {
            sb.Append(current.getDisplayFormatString());
        }
        return sb.ToString();
    }

    void readScores()
    {
        try
        {
            scores.Clear();
            StreamReader reader = new StreamReader(path);
            string line;// = reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                string[] splits = line.Split(',');
                scores.Add(new score(int.Parse(splits[1]), splits[0]));
            }
            reader.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("File Read error:" + e.Message);
        }

    }

    public void writeScores()
    {
        try
        {
            StreamWriter writer = new StreamWriter(path, false);
            sortAndTruncateScores();
            StringBuilder sb = new StringBuilder();
            foreach (score current in scores)
            {
                sb.Append(current.getFileFormatString());
            }
            writer.Write(sb.ToString());
            writer.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("File Write Error: " + e.Message);
        }

    }

    void sortAndTruncateScores()
    {
        scores.Sort(delegate (score s1, score s2) { return s2.scoreVal.CompareTo(s1.scoreVal); });
        int toRemove = scores.Count - numScores;
        if (toRemove > 0)
        {
            scores.RemoveRange(numScores, toRemove);
        }
    }

    public void recordScore(string name, int score)
    {
        scores.Add(new score(score, name));
    }

    public bool isHighScore(int score)
    {
        sortAndTruncateScores();
        if (scores.Count < 0) { return true; }
        return score > scores[scores.Count - 1].scoreVal;

    }
}
class score
{
    public int scoreVal;
    public string name;
    public score(int score, string name)
    {
        this.scoreVal = score;
        this.name = name;
    }

    public string getDisplayFormatString()
    {
        return name + ": " + scoreVal + "\n";
    }
    public string getFileFormatString()
    {
        return name + "," + scoreVal + "\n";
    }

}
