using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchVisualizer : MonoBehaviour
{
    [SerializeField]
    public Player playerPrefab;
    
    public int frameCount;
    
    private List<MatchFrameData> matchFrameDataCollection;
   
    private void OnEnable()
    {
        MatchDataReader.OnGeneratingMatchData += OnMatchFrameDataRetrieved;
    }

    private void OnDestroy()
    {
        MatchDataReader.OnGeneratingMatchData -= OnMatchFrameDataRetrieved;
    }

    private void OnMatchFrameDataRetrieved(List<MatchFrameData> matchFrameDataCollection)
    {
        Debug.Log("Data Received");
        this.matchFrameDataCollection = matchFrameDataCollection;
        
        //Now that the data is retrieved let's start visualizing the frame
        StartCoroutine(VisualizeFrame());
    }

    private IEnumerator VisualizeFrame()
    {
        
        //TODO This is horrible way to update the player they should always stay in the scene and just have
        // their position updated instead of spawning new ones each frame and destroying the ones from the previous frame
        // But for quick iteration/prototyping this was used
        foreach (var player in FindObjectsByType<Player>(FindObjectsSortMode.None))
        {
            Destroy(player.gameObject);
        }
        
        MatchFrameData matchFrameData = matchFrameDataCollection[frameCount];
        foreach (Person person in matchFrameData.Persons)
        {
            Player personGameObject = Instantiate(playerPrefab);
            personGameObject.transform.position = new Vector3(person.Position[0], 0, person.Position[2]);

            switch (person.TeamSide)
            {
                case 1:
                    personGameObject.SetJerseyColor(Color.blue);
                    break;
                case 2:
                    personGameObject.SetJerseyColor(Color.red);
                    break;
                default:
                    personGameObject.SetJerseyColor(Color.yellow);
                    break;
            }
        }

        yield return new WaitForEndOfFrame();
        frameCount++;
        StartCoroutine(VisualizeFrame());
    }
}
