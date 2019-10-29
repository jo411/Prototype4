using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LeaderBoardManager : MonoBehaviour
{

    public string path = "Assets/Resources/Leaderboard/score.txt";
    int numScores=3;
    List<score> scores = new List<score>();
    // Start is called before the first frame update
    void Start()
    {
        readScores();

        recordScore("jane", 10);
        recordScore("jon", 4);
        recordScore("jill", 6);
        recordScore("maya", 3);

        Debug.Log(getDisplayStringForAllScores());
        writeScores();
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
        }catch(System.Exception e)
        {
            Debug.Log("File Read error:" + e.Message);
        }   

    }
    
    public void writeScores()
    {
        StreamWriter writer = new StreamWriter(path, false);
        sortAndTruncateScores();
        StringBuilder sb = new StringBuilder();
        foreach (score current in scores)
        {
            sb.Append(current.getFileFormatString());
        }
        writer.Write(sb.ToString());
    }    
   
    void sortAndTruncateScores()
    {
        scores.Sort(delegate (score s1, score s2) { return s1.scoreVal.CompareTo(s2.scoreVal); });
        int toRemove = scores.Count - numScores;
        if(toRemove>0)
        {
            scores.RemoveRange(numScores, toRemove);
        }
    }

    public void recordScore(string name, int score)
    {
        scores.Add(new score(score, name));       
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
        return name + ": " + scoreVal+"\n";
    }
    public string getFileFormatString()
    {
        return name + "," + scoreVal+"\n";
    }
   
}
