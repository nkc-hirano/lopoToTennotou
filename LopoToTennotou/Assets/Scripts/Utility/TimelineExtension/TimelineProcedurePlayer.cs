using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineProcedurePlayer : MonoBehaviour
{
    [SerializeField]
    bool playOnAwake = false;
    [SerializeField]
    List<TimlineInfo> timlineInfos = new List<TimlineInfo>();
    private bool isCurrentTimelineAdvance = false;
    private int currentProgress = 0;

    public int MaxProgress => timlineInfos.Count;
    public event Action timeLineFinishEventHandler;

    void Awake()
    {
        if (playOnAwake)
        {
            TimelineStart();
        }
    }

    void Update()
    {
        if (timlineInfos.Count == 0) { return; }

        bool isTimelineFinish = timlineInfos[currentProgress].timelineData.time >= timlineInfos[currentProgress].timelineData.duration;
        if (isTimelineFinish)
        {
            TimelineStop();
            if (timeLineFinishEventHandler != null) { timeLineFinishEventHandler(); }
            currentProgress++;
            TimelineStart();
        }
    }

    public void TimelineStart()
    {
        if (isCurrentTimelineAdvance) { return; }
        if (timlineInfos.Count - 1 > currentProgress) { return; }

        isCurrentTimelineAdvance = true;
        timlineInfos[currentProgress].timelineData.Play();
    }

    public void TimelineStop()
    {
        if (!isCurrentTimelineAdvance) { return; }

        isCurrentTimelineAdvance = false;
        timlineInfos[currentProgress].timelineData.Stop();
    }

    [System.Serializable]
    public class TimlineInfo
    {
        [SerializeField, Tooltip("”CˆÓ")]
        public string timelineName = null;

        [SerializeField]
        public PlayableDirector timelineData = null;
    }
}