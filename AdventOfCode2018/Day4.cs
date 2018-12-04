using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{

    public class Schedule
    {
        public Schedule(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }
    }

    public class WakeUp : Schedule
    {
        public WakeUp(DateTime date) : base(date)
        {
        }
    }

    public class Sleep : Schedule
    {
        public Sleep(DateTime date) : base(date)
        {
        }
    }

    public class Shift : Schedule
    {
        public Shift(DateTime date, int id) : base(date)
        {
            Id = id;
        }

        public int Id { get; }
    }

    class Matcher
    {
        public Regex Regex { get; set; }
        public Func<Match, Schedule> Extractor { get; set; }
    }

    public class Day4
    {
        public static IEnumerable<Schedule> Input()
        {
            // [1518-07-04 00:01] falls asleep
            // [1518-11-01 00:25] wakes up
            // [1518-11-01 00:00] Guard #10 begins shift

            var matchers = new[] {
                new Matcher()
                {
                    Regex = new Regex(@"\[(?<date>[0-9]+\-[0-9]+\-[0-9]+ [0-9]+:[0-9]+)\] falls asleep"),
                    Extractor = m => new Sleep(DateTime.Parse(m.Groups["date"].Value))
                },
                new Matcher()
                {
                    Regex = new Regex(@"\[(?<date>[0-9]+\-[0-9]+\-[0-9]+ [0-9]+:[0-9]+)\] wakes up"),
                    Extractor = m => new WakeUp(DateTime.Parse(m.Groups["date"].Value))
                },
                new Matcher()
                {
                    Regex = new Regex(@"\[(?<date>[0-9]+\-[0-9]+\-[0-9]+ [0-9]+:[0-9]+)\] Guard #(?<id>[0-9]+) begins shift"),
                    Extractor = m => new Shift(DateTime.Parse(m.Groups["date"].Value), int.Parse(m.Groups["id"].Value))
                },
            };

            return File.ReadAllLines("input4.txt")
                .Select(x => matchers.Where(m => m.Regex.IsMatch(x))
                    .Select(m => m.Extractor(m.Regex.Match(x)))
                    .Single());
        }

        private static IDictionary<int, int[]> GuardStatistics()
        {
            var schedule = Input().OrderBy(x => x.Date);
            var guards = new ConcurrentDictionary<int, int[]>();
            int currentGuard = 0;
            int sleepStart = 0;

            foreach (var s in schedule)
            {
                switch (s)
                {
                    case Shift shift:
                        currentGuard = shift.Id;
                        break;
                    case Sleep sleep:
                        sleepStart = sleep.Date.Minute;
                        break;
                    case WakeUp wake:
                        var sleepStop = wake.Date.Minute;
                        var sleepSchedule = new int[60];
                        for (int i = sleepStart; i < sleepStop; i++)
                        {
                            sleepSchedule[i]++;
                        }

                        guards.AddOrUpdate(currentGuard, sleepSchedule, (_, existing) => existing.Plus(sleepSchedule));
                        break;
                }
            }

            return guards;
        }

        private static int MultiplyGuardIdByMostSleepyHour(KeyValuePair<int, int[]> mrSleepy)
        {
            var sleepySchedule = mrSleepy.Value;
            int max = 0;
            for (int i = 0; i < sleepySchedule.Length; i++)
            {
                if (sleepySchedule[i] > sleepySchedule[max])
                {
                    max = i;
                }
            }

            var result1 = mrSleepy.Key * max;

            return result1;
        }

        public static int SolveFirst()
        {
            var guards = GuardStatistics();

            var mrSleep1 = guards.OrderByDescending(x => x.Value.Sum()).First();

            return MultiplyGuardIdByMostSleepyHour(mrSleep1);
        }

        public static int SolveSecond()
        {
            var guards = GuardStatistics();

            var mrSleep2 = guards.OrderByDescending(x => x.Value.Max()).First();

            return MultiplyGuardIdByMostSleepyHour(mrSleep2);
        }

    }
}
