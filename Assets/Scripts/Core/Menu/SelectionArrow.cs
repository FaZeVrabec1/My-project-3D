using UnityEngine.UI;
using UnityEngine;

public class SelectionArrow : MonoBehaviour
{
    private RectTransform rect;
    public RectTransform[] options;
    private int currentPosition;
    public AudioClip changeSound;
    public AudioClip InteractSound;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }


    }


    private void ChangePosition(int change)
    {
        currentPosition += change;

        if (change != 0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }



        if (currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if (currentPosition > options.Length -1)
        {
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(InteractSound);

        options[currentPosition].GetComponent<Button>().onClick.Invoke();


    }


}
