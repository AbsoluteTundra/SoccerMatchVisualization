using TMPro;
using UnityEngine;

public class PersonActor : BaseActor
{
    [SerializeField] private MeshRenderer jersey;
    [SerializeField] private TextMeshProUGUI jerseyNumberTextDisplay;

    public void SetJerseyColor(Person person)
    {
        if (jersey == null || person == null)
            return;

        //Judges are Yellow
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
