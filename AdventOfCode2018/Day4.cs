﻿using System;
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

        public static int SolveFirst()
        {
            var i = Input().OrderBy(x => x.Date);

            return 0;
        }
    }
}