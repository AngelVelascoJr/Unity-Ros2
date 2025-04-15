using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;

public class TestPublisher : MonoBehaviour
{
    ROSConnection ros2;
    public string topic_name = "pos_rot";

    public GameObject Objective;

    public float PUBLISH_MESSAGE_FREQUENCY = 0.5f;

    private float time_elapsed;

    private void Start()
    {
        ros2 = ROSConnection.GetOrCreateInstance();
        ros2.RegisterPublisher<PosRotMsg>(topic_name);
    }

    private void Update()
    {
        time_elapsed += Time.deltaTime;
        if (time_elapsed < PUBLISH_MESSAGE_FREQUENCY)
            return;

        PosRotMsg msg = new PosRotMsg(
            Objective.transform.position.x,
            Objective.transform.position.y,
            Objective.transform.position.z,

            Objective.transform.rotation.x,
            Objective.transform.rotation.y,
            Objective.transform.rotation.z,
            Objective.transform.rotation.w
        );

        ros2.Publish(topic_name, msg);
        Debug.Log($"Publishing to topic {topic_name}: {msg}");
        time_elapsed -= PUBLISH_MESSAGE_FREQUENCY;

    }

}
