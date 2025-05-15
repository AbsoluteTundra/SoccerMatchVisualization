using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MeshRenderer jersey;
    [SerializeField] private TextMeshProUGUI jerseyNumberTextDisplay;

    public void SetPosition(Person person)
    {
        if (person != null)
            transform.position = new Vector3(person.Position[0], 0, person.Position[2]);
    }
    
    public void SetRotation(Person nextFrameDataPerson)
    {
        //TODO Make this rotation smoother when the next position is really small the rotation is jittery
        if (nextFrameDataPerson != null)
            transform.LookAt(new Vector3(nextFrameDataPerson.Position[0], 0, nextFrameDataPerson.Position[2]));
    }

    public void SetJerseyColor(Person person)
    {
        if (jersey == null || person == null)
            return;

        switch (person.TeamSide)
        {
            case 1:
                jersey.material.color = Color.blue;
                break;
            case 2:
                jersey.material.color = Color.red;
                break;
            default:
                jersey.material.color = Color.yellow;
                break;
        }
    }

    public void SetJerseyNumber(Person person)
    {
        if (jerseyNumberTextDisplay != null || person != null)
            jerseyNumberTextDisplay.text = person.JerseyNumber.ToString();
    }
}
