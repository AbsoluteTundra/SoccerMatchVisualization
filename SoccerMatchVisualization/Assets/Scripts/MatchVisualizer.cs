using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class MatchVisualizer : MonoBehaviour
{
    [SerializeField] private PersonActor playerPrefab;
    [SerializeField] private BallActor ballPrefab;

    private int frameCount;

    private List<MatchFrameData> matchFrameDataCollection;

    private Dictionary<string, PersonActor> players;
    private BallActor ball;
    [SerializeField] private CinemachineCamera cinemachineCamera;

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

        //First setup and spawn the people that are involved with the match
        // Using a dictionary to easisly look them up later when need for the next frame
        SetupPlayers(matchFrameDataCollection.First().Persons);

        SetupBall(matchFrameDataCollection.First().Ball);

        //Now that the data is retrieved and player are setup let's start visualizing the frames
        StartCoroutine(VisualizeFrame());
    }

    private void SetupPlayers(List<Person> persons)
    {
        players = new Dictionary<string, PersonActor>();

        //Players need to have different jersey colors and position when spawned
        // Also the jersey number needs to be visible
        foreach (Person person in persons)
        {
            PersonActor player = Instantiate(playerPrefab);
            player.SetPosition(person);
            player.SetJerseyColor(person);
            player.SetJerseyNumber(person);
            //Add the player to the dictionary for later use
            players.Add(person.Id, player);
        }
    }

    private void SetupBall(Ball ballData)
    {
        ball = Instantiate(ballPrefab);
        ball.SetPosition(ballData);
        cinemachineCamera.LookAt = ball.transform;
    }

    private IEnumerator VisualizeFrame()
    {
        MatchFrameData matchFrameData = matchFrameDataCollection[frameCount];
        
        //TODO Fix the error that appears at the end of the match because the index doesn't exist
        MatchFrameData nextMatchFrameData = matchFrameDataCollection[frameCount + 1];

        foreach (Person person in matchFrameData.Persons)
        {
            PersonActor player = players[person.Id];
            player.SetPosition(person);
        }
        
        //Player also need to match the direction they are headed in which will be based on the data for the next frame
        foreach (Person person in nextMatchFrameData.Persons)
        {
            PersonActor player = players[person.Id];
            player.SetRotation(person);
        }
        
        //set the location of the ball every frame of the match
        ball.SetPosition(matchFrameData.Ball);

        yield return new WaitForEndOfFrame();
        frameCount++;
        StartCoroutine(VisualizeFrame());
    }
}
