using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent._2018.Classes
{
    public enum GuardRecordType
    {
        ShiftStart,
        FallAsleep,
        WakeUp,
        Unset
    }

    public class GuardRecord
    {
        public const int DefaultGuardID = -1;

        public GuardRecordType Type { get; private set; } = GuardRecordType.Unset;

        public int GuardID { get; private set; } = DefaultGuardID;
        public DateTime Timestamp { get; private set; } = DateTime.MinValue;

        public string Message { get; } = null;

        public GuardRecord(string input)
        {
            var arr = input.Substring(1).Split(']');

            //< Parse the full DateTime of this record
            this.Timestamp = ParseTimestamp(arr[0]);
            //< Retain the message segment
            this.Message = arr[1].Trim();
            //< Parse the message body
            ParseMessage(Message);
        }

        static DateTime ParseTimestamp(string dtStr)
        {
            //< Format of 'YYYY-MM-DD HH:mm'
            var arr = dtStr.Split(' ');

            //< Parse the component values into integers
            var ymd = arr[0].Split('-').Select(int.Parse).ToList();
            var hm = arr[1].Split(':').Select(int.Parse).ToList();

            //< Compose and return dat DateTime
            return new DateTime(ymd[0], ymd[1], ymd[2], hm[0], hm[1], 0);
        }

        void ParseMessage(string message)
        {
            if (message.StartsWith("Guard"))
            {
                this.Type = GuardRecordType.ShiftStart;
                //< Grab the Guard's ID from the message string
                var str = message.Substring(message.IndexOf("#") + 1)
                                 .Split(' ').First();
                this.GuardID = int.Parse(str);
            }
            else if (message.StartsWith("wakes"))
            {
                this.Type = GuardRecordType.WakeUp;
            }
            else if (message.StartsWith("falls"))
            {
                this.Type = GuardRecordType.FallAsleep;
            }
        }
    }

    public class GuardSleepTracker
    {
        public List<GuardRecord> Records { get; } = null;

        public Dictionary<int, int[]> GuardMap { get; private set; } = null;

        public GuardSleepTracker(IEnumerable<string> input)
        {
            this.Records = input.Select(x => new GuardRecord(x)).ToList();

            CalcuateMinutesAsleep();
        }

        void CalcuateMinutesAsleep()
        {
            this.GuardMap = new Dictionary<int, int[]>();

            GuardRecord lastRec = null;
            int lastID = GuardRecord.DefaultGuardID;
            foreach (var rec in Records.OrderBy(x => x.Timestamp))
            {
                switch(rec.Type)
                {
                    case GuardRecordType.ShiftStart:
                        lastID = rec.GuardID;
                        CheckGuardMap(lastID);
                        break;
                    case GuardRecordType.FallAsleep:
                        break;
                    case GuardRecordType.WakeUp:
                        AddGuardSleep(lastID, lastRec.Timestamp, rec.Timestamp);
                        break;
                }
                lastRec = rec;
            }
        }

        const int ArrSize = 60;
        void CheckGuardMap(int id)
        {
            if (!GuardMap.ContainsKey(id))
            {
                GuardMap.Add(id, new int[ArrSize]);
            }
        }

        void AddGuardSleep(int ID, DateTime sleep, DateTime wake)
        {
            foreach (int t in Enumerable.Range(sleep.Minute, wake.Minute - sleep.Minute))
            {
                GuardMap[ID][t]++;
            }
        }

        public Tuple<int, int, int> GetMostAsleepGuard()
        {
            var recs = GuardMap.Select(kvp => Tuple.Create(kvp.Key, kvp.Value.Sum(), GetMinuteMostAsleep(kvp.Value)))
                               .OrderBy(x => x.Item2).ToList();

            return recs.Last();
        }

        int GetMinuteMostAsleep(int[] arr)
        {
            int maxVal = arr.Max();
            return arr.ToList().IndexOf(maxVal);
        }

        public Tuple<int, int, int> GetGuardMostRegularlyAsleep()
        {
            var recs = GuardMap.Select(kvp => Tuple.Create(kvp.Key, kvp.Value.Max(), GetMinuteMostAsleep(kvp.Value)))
                               .OrderBy(x => x.Item2).ToList();

            return recs.Last();
        }
    }
}
