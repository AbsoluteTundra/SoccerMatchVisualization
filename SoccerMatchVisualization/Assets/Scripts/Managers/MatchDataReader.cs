using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// This class is responsible for reading the data from the data file
/// </summary>
public class MatchDataReader : MonoBehaviour
{
    [SerializeField] private string matchFileName = "MatchData.idf";

    private List<MatchFrameData> matchFrameDataCollection = new List<MatchFrameData>();

    public static Action<List<MatchFrameData>> OnGeneratingMatchData;

    private void Start()
    {
        //We first need to analyze the data and read from the provided file
        _ = ExtractDataFromFile();
    }

    //Decided to do the data conversion in the background thread because it can be quite expensive and don't want to freeze the main thread
    private async Awaitable ExtractDataFromFile()
    {
        await Awaitable.BackgroundThreadAsync();
        
        // Decided to use StreamingAssets since I need the content of the file and the type of file wasn't a TextAsset
        // I'm assuming that within the company they use a system where users can download the matches they want to see or use the Unity Addressable System to deliver content to the users
        string file =  Path.Combine(Application.streamingAssetsPath, matchFileName);

        if (!File.Exists(file))
        {
            Debug.LogError($"Could not find file:{file}");
            return;
        }
        
        foreach (string frameData in File.ReadLines(file))
        {
            try
            {
                //Convert the string retrieved from the file to a class using JsonUtility as the string retrieved is in correct json format and it to the list of frame data
                matchFrameDataCollection.Add(JsonUtility.FromJson<MatchFrameData>(frameData));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        await Awaitable.MainThreadAsync();
        
        //When done we need to tell the class responsible for the visualization that the data is done and the match can begin
        OnGeneratingMatchData?.Invoke(matchFrameDataCollection);
    }
}
