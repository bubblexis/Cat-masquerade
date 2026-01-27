using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    //I did the audio in here for ease of use (trust)

    [SerializeField] float walkingBobbingSpeed = 14f;
    [SerializeField] float bobbingAmount = 0.05f;

    [SerializeField] PlayerController playerController;

    float defaultPosY = 0;
    float timer = 0;

    AudioSource audioSource;

    bool walkIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(playerController.move.x) > 0.1f || Mathf.Abs(playerController.move.z) > 0.1f)
        {
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);

            if(!walkIsPlaying)
            {
                audioSource.Play();
                walkIsPlaying = !walkIsPlaying;
            }
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        
            if(walkIsPlaying)
            {
                audioSource.Stop();
                walkIsPlaying = !walkIsPlaying;
            }
        }
    }
}
