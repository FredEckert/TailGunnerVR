using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class TailgUI : MonoBehaviour
{

    [UnityEngine.HideInInspector]
    public int score;
    [UnityEngine.HideInInspector]
    public int highscore;
//    private VectorLine uiLine;
    private VectorLine[] charLines;
//    private VectorLine livesLine;
    private string scoreString;
    private string scoreString2;
    private string[] currentStrings;

    public static TailgUI use;

    public virtual void Awake()
    {
        TailgUI.use = this;
    }

    public void StartUp()
    {
        this.scoreString = "    ";
        this.scoreString2 = "    ";
        this.charLines = new VectorLine[5];
        this.currentStrings = new string[5];

        // score
        this.charLines[0] = this.MakeLine("Score", new System.Collections.Generic.List<Vector3>(2));
        this.charLines[1] = this.MakeLine("TheScore", new System.Collections.Generic.List<Vector3>(2));
        //  high score
        this.charLines[2] = this.MakeLine("Highscore", new System.Collections.Generic.List<Vector3>(2));
        this.charLines[3] = this.MakeLine("TheHighscore", new System.Collections.Generic.List<Vector3>(2));

        //this.AddToScore(1);
        //this.AddToScore(2439);

    }


    public virtual VectorLine MakeLine(string name, System.Collections.Generic.List<Vector3> list)
    {
        var line = new VectorLine(name, list, Manager.use.lineWidth)
        {
            texture = Manager.use.lineTexture,
            material = Manager.use.lineMaterial,
            color = Manager.use.colorNormal,
            capLength = Manager.use.capLength
        };
        return line;
    }

    public virtual void AddToScore(int points)
    {
        this.score = this.score + points;
        string thisScore = this.score.ToString("00"); // Always have 2+ digits even when score is 0
        this.currentStrings[0] = "Score";
        // Format score using a substring from scoreString, depending on thisScore's length, so it's right-justified
        this.currentStrings[1] = scoreString.Substring(0, 4 - thisScore.Length) + thisScore;
        this.charLines[0].MakeText(this.currentStrings[0], new Vector3(-750, 600, 1800), 50);
        this.charLines[1].MakeText(this.currentStrings[1], new Vector3(-720, 500, 1800), 50);
        // Roll score over if high enough (surely nobody would play that long?!)
        if (score > 9999)
        {
            score = 0;
        }
        // High score?
        if (this.score > this.highscore)
        {
            this.highscore = this.score;
            this.PrintHighScore();
        }
        this.charLines[0].Draw3D();
        this.charLines[1].Draw3D();
    }

    public virtual void PrintHighScore()
    {
        string thisScore = this.highscore.ToString("00");
        this.currentStrings[2] = "High Score";
        this.currentStrings[3] = scoreString2.Substring(0, 4 - thisScore.Length) + thisScore;
        this.charLines[2].MakeText(this.currentStrings[2], new Vector3(250, 600, 1800), 50);
        this.charLines[3].MakeText(this.currentStrings[3], new Vector3(370, 500, 1800), 50);
        this.charLines[2].Draw3D();
        this.charLines[3].Draw3D();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
