using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour {
    public Transform container;
    public GameObject spike;
    public List<GameObject> spikes;
    private const int maxTargets = 50;
    private float spikeHeight = 0.25f;
    public float playerHeight;
    public float betweenSpace = 12.5f;
    private float initialGenY = 4f;
    private float lastGenY;
    public Side side;
    private float period;
    private int spikesPerScreen;
    private int spikeCounter = 0;

    void Awake() {
        side = new Side();
        reset();
        float height = Constants.instance.screenHeight;
        spikesPerScreen = (int)Mathf.Floor(height / (spikeHeight + betweenSpace));
        float speed = Constants.instance.cameraSpeed;

        period = (height / speed / spikesPerScreen ) * 0.95f;
    }

    public void onStart() {
        InvokeRepeating("tryGen", 0f, period);
    }

    void tryGen() {
        if (spikeCounter <= spikesPerScreen * (GameController.passedScreens + 1)) {
            gen();
        }
    }

    bool lastFlipped = false;
    void gen() {
        spikeCounter++;

        if (Random.value < 0.6f) {
            side.flip();
            lastFlipped = true;
        } else {
            lastFlipped = false;
        }
        float localSpacing = 0f;
        if (!lastFlipped) {
            localSpacing = Random.Range(0.75f, 1.35f) * betweenSpace;
        } else {
            //has to be slightly less or more than player jump
            float offset = Random.value < 0.5 ? Random.Range(0.70f, 0.8f) : Random.Range(1.25f, 1.45f);
            localSpacing = betweenSpace * offset;
        }

        lastGenY = lastGenY + localSpacing;

        addSpike(lastGenY);

        while (spikes.Count > maxTargets) {
                // remove the oldest
                Destroy(spikes[0]);
                spikes.RemoveAt(0);
            }
    }
    void addSpike(float y) {
        bool isLeft = side.side == side.left;

        GameObject newSpike = Instantiate(spike);
        newSpike.GetComponent<Spike>().init(isLeft, y);

        newSpike.GetComponent<Transform>().SetParent(container);
        spikes.Add(newSpike);
    }

    public void reset() {
        CancelInvoke();

        lastGenY = initialGenY;
        spikeCounter = 0;

        spikes.Clear();

        var children = new List<GameObject>();
        foreach (Transform child in container) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}