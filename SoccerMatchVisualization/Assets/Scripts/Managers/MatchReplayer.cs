using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class will be responsible for allowing watchers to rewind to their favorite moments
public class MatchReplayer : MonoBehaviour
{
    [SerializeField] private Slider rewindSlider;
    [SerializeField] private MatchVisualizer matchVisualizer;
    
    private void OnEnable()
    {
        MatchDataReader.OnGeneratingMatchData += OnMatchFrameDataRetrieved;
        rewindSlider.onValueChanged.AddListener(OnRewindSliderValueChanged);
        
        //Use nameof to make sure no typos are made and to make it easier to change the name of the function in the future
        InvokeRepeating(nameof(UpdateSlider),0,1f/matchVisualizer.GetFrameRate());
    }

    private void OnDestroy()
    {
        MatchDataReader.OnGeneratingMatchData -= OnMatchFrameDataRetrieved;
    }

    private void UpdateSlider()
    {
        rewindSlider.SetValueWithoutNotify(matchVisualizer.GetFrameCount());
    }

    private void OnMatchFrameDataRetrieved(List<MatchFrameData> matchFrameDataCollection)
    {
        rewindSlider.maxValue = matchFrameDataCollection.Count;
    }

    private void OnRewindSliderValueChanged(float value)
    {
        if (matchVisualizer != null)
        {
            matchVisualizer.SetFrameCount((int) value);
        }
    }
}
