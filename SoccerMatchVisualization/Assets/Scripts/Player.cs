using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MeshRenderer Jersey;

    public void SetPosition(Person person)
    {
        transform.position = new Vector3(person.Position[0], 0, person.Position[2]);
    }

    public void SetJerseyColor(Person person)
    {
        switch (person.TeamSide)
        {
            case 1:
                Jersey.material.color = Color.blue;
                break;
            case 2:
                Jersey.material.color = Color.red;
                break;
            default:
                Jersey.material.color = Color.yellow;
                break;
        }
    }
}
