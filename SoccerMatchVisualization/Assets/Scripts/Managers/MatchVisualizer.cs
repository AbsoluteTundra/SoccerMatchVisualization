using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using Debug = UnityEngine.Debug;


/// <summary>
/// This class is responsible for visualizing each frame of the match
/// </summary>
public class MatchVisualizer : MonoBehaviour
{
    [SerializeField] private int frameRate = 25;
    [SerializeField] private PersonActor playerPrefab;
    [SerializeField] private BallActor ballPrefab;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private int frameCount;

    private List<MatchFrameData> matchFrameDataCollection;

    private Dictionary<string, PersonActor> players;
    private BallActor ball;
    
    private void PlayMatch()
    {
        InvokeRepeating(nameof(VisualizeFrame),0,1.0f/frameRate);
    }
    
    // Set the framecount to the given value
    public void SetFrameCount(int newIndex)
    {
        if (newIndex > 0 && newIndex != matchFrameDataCollection.Count)
            frameCount = newIndex;
        
        PlayMatch();
    }

    // Returns the index of the current frame
    public int GetFrameCount()
    {
        return frameCount;
    }

    // Returns the match frame rate playback speed
    public float GetFrameRate()
    {
        return frameRate;
    }
    
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

        //First setup and spawn the people & ball that are involved with the match
        // Using a dictionary to easily look up the players later when needed for the next frame
        SetupPlayers(matchFrameDataCollection.First().Persons);
        SetupBall(matchFrameDataCollection.First().Ball);
        
        //Now that the data is retrieved and player are setup let's start visualizing the frames
        PlayMatch();
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
            
            //If a jersey number is lower or equal to 0 disable the actor
            // Data showed a Yellow Judge Actor inside other players
            //I'm assuming it is a virtual judge or a camera
            if (person.JerseyNumber <= 0)
            {
                player.gameObject.SetActive(false);
            }
        }
    }

    private void SetupBall(Ball ballData)
    {
        ball = Instantiate(ballPrefab);
        ball.SetPosition(ballData);
        cinemachineCamera.LookAt = ball.transform;
    }

    //I noticed that the speed of the match didn't match with description of the assigment as the match was around 10 minutes
    //I assumed it would be around 60FPS but with the help of AI found out that the Framerate might be closer to 25.
    // 25FPS is common for real-time video recordings
    // To Test I set the FixedUpdate rate to 1/25 and used stopwatch to check the time
    // Based on that information changed from using couroutine to instead use InvokeRepeating 
    private void VisualizeFrame()
    {
        MatchFrameData matchFrameData = matchFrameDataCollection[frameCount];
        
        MatchFrameData nextMatchFrameData = null;

        if (frameCount + 1 <= matchFrameDataCollection.Count - 1)
            nextMatchFrameData = matchFrameDataCollection[frameCount + 1];

        //Set the new position for each player based on the current frame data
        foreach (Person person in matchFrameData.Persons)
        {
            PersonActor player = players[person.Id];
            player.SetPosition(person);
        }

        if (nextMatchFrameData != null)
        {
            //Player also need to match the direction they are headed in which will be based on the data for the next frame
            foreach (Person person in nextMatchFrameData.Persons)
            {
                PersonActor player = players[person.Id];
                player.SetRotation(person);
            }
        }

        //set the location of the ball every frame of the match
        ball.SetPosition(matchFrameData.Ball);
        
        frameCount++;

        if (frameCount == matchFrameDataCollection.Count)
        {
            Debug.Log("No more data Match stopped");
            CancelInvoke();
        }
    }
}
