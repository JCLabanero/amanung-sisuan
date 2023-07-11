using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
public class DIALOGUE_DATA_SPEAKER
{
    public string name, castName;
    public string displayName => (castName != string.Empty ? castName : name);
    public Vector2 castPosition;
    public List<(int layer, string expression)> CastExpressions { get; set; }
    private const string CASTNAME_ID = " as ";
    private const string CASTPOSITION_ID = " at ";
    private const string CASTEXPRESSION_ID = " [";
    private const char AXISDELIMITER = ':';
    private const char EXPRESSIONLAYER_JOINDER = ',';
    private const char EXPRESSIONLAYER_DELIMITER = ':';
    public DIALOGUE_DATA_SPEAKER(string rawSpeaker)
    {
        string pattern = @$"{CASTNAME_ID}|{CASTPOSITION_ID}|{CASTEXPRESSION_ID.Insert(CASTEXPRESSION_ID.Length - 1, @"\")}";
        MatchCollection matches = Regex.Matches(rawSpeaker, pattern);
        //avoi null reference
        castName = "";
        castPosition = Vector2.zero;
        CastExpressions = new List<(int layer, string expression)>();
        if (matches.Count == 0)
        {
            name = rawSpeaker;
            return;
        }
        int index = matches[0].Index;
        name = rawSpeaker.Substring(0, index);
        for (int i = 0; i < matches.Count; i++)
        {
            Match match = matches[i];
            int startIndex = 0, endIndex = 0;
            if (match.Value == CASTNAME_ID)
            {
                startIndex = match.Index + CASTNAME_ID.Length;
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);
            }
            else
            if (match.Value == CASTPOSITION_ID)
            {
                startIndex = match.Index + CASTPOSITION_ID.Length;
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);
                string[] axis = castPos.Split(AXISDELIMITER, System.StringSplitOptions.RemoveEmptyEntries);
                float.TryParse(axis[0], out castPosition.x);
                if (axis.Length > 1)
                    float.TryParse(axis[1], out castPosition.y);
            }
            else
            if (match.Value == CASTEXPRESSION_ID)
            {
                startIndex = match.Index + CASTEXPRESSION_ID.Length;
                endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex + 1));
                CastExpressions = castExp.Split(EXPRESSIONLAYER_JOINDER).Select(x =>
                {
                    var parts = x.Trim().Split(EXPRESSIONLAYER_DELIMITER);
                    return (int.Parse(parts[0]), parts[1]);
                }).ToList();
            }
        }
    }
}