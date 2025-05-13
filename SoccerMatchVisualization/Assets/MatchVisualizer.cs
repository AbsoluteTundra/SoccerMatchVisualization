using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchVisualizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MatchDataReader.OnGeneratingMatchData += OnMatchFrameDataRetrieved;
    }

    private void OnDestroy()
    {
        MatchDataReader.OnGeneratingMatchData -= OnMatchFrameDataRetrieved;
    }

    private void OnMatchFrameDataRetrieved(List<MatchFrameData> matchFrameDataCollection)
    {
        //Now that the data is retrieved let's setup the player as they are standing in the first frame
    }
}
