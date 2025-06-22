using System;
using UnityEngine;
using Firebase.Firestore;

public static class DisplayTimestamp
{
    public static string GetPrettyTimeAgo(Timestamp firebaseTimestamp)
    {
        DateTime timestampUtc = firebaseTimestamp.ToDateTime().ToUniversalTime();
        TimeSpan timeDiff = DateTime.UtcNow - timestampUtc;

        if (timeDiff.TotalDays >= 1)
        {
            int days = (int)Math.Floor(timeDiff.TotalDays);
            return $"{days}일 전";
        }
        else if (timeDiff.TotalHours >= 1)
        {
            int hours = (int)Math.Floor(timeDiff.TotalHours);
            return $"{hours}시간 전";
        }
        else
        {
            int minutes = Math.Max(1, (int)Math.Floor(timeDiff.TotalMinutes)); // 최소 1분
            return $"{minutes}분 전";
        }
    }
}