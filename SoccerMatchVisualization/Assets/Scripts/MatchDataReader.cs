using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MatchDataReader : MonoBehaviour
{
    [SerializeField]
    private DefaultAsset MatchData;

    [SerializeField]
    private List<MatchFrameData> matchFrameDataCollection = new List<MatchFrameData>();

    public static Action<List<MatchFrameData>> OnGeneratingMatchData;

    private void Start()
    {
        //We first need to analyze the data and read from the provided file
        ExtractDataFromFile();
    }

    private void ExtractDataFromFile()
    {
        //TEMP Solution to start reading the data not desirable as this path doesn't exist inside a build and want I something more dynamic later
        foreach (string frameData in File.ReadLines("Assets/Resources/Applicant-test.idf"))
        {
            //Convert the string retrieved from the file to a class/struct using JsonUtility as the string retrieved is correct json format
            matchFrameDataCollection.Add(JsonUtility.FromJson<MatchFrameData>(frameData));
        }

        //When done we need to tell the class responsible for the visualization that the data is done
        OnGeneratingMatchData?.Invoke(matchFrameDataCollection);
    }
}
