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


    //markovs chains


    private Matrix4x4 matrix;
    void Awake() {
        side = new Side();
        reset();
    }

    public void onStart() {
        InvokeRepeating("gen", 0f, 1f);
    }
    void gen() {
        // print("gen");
        if (Random.value < 0.4f) side.flip();
        float localSpacing = Random.Range(1f, 1.75f) * betweenSpace;

        lastGenY = lastGenY + localSpacing;
        // print("lastGen " + lastGenY);

        addSpike(lastGenY);

        while (spikes.Count > maxTargets) {
                // remove the oldest
                Destroy(spikes[0]);
                spikes.RemoveAt(0);
            }
    }

    void Update() {
        // print(lastGenY);
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

        spikes.Clear();

        var children = new List<GameObject>();
        foreach (Transform child in container) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}