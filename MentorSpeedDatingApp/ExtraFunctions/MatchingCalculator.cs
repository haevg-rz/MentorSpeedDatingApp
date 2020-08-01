using MentorSpeedDatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MentorSpeedDatingApp.ExtraFunctions
{
    public class MatchingCalculator
    {
        private DateTime startTime;
        private DateTime endTime;
        private List<Mentor> mentors;
        private List<Mentee> mentees;
        private List<TimeSlot> timeSlots;
        private double amountOfDates;
        public List<Matching> Matchings { get; set; }
        public List<IDate> MatchingDates { get; set; }
        public double DateDuration { get; set; }
        private int mentorIndex;

        public MatchingCalculator(DateTime startTime, DateTime endTime, List<Mentor> mentors, List<Mentee> mentees)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.mentors = mentors;
            this.mentees = mentees;
            this.Initiate();
        }

        public void Initiate()
        {
            this.amountOfDates = CalculateAmountOfDates(this.mentors.Count, this.mentees.Count);
            if (this.mentees.Count != this.mentors.Count)
            {
                this.mentees = this.FixMenteeListSize(this.mentees);
            }

            this.timeSlots = this.CalculateTimeSlots();
            this.Matchings = this.CalculateMatchings();
            this.Matchings = this.TrimMatchings(this.Matchings);
            this.MatchingDates = this.RetrieveDates(this.Matchings);
        }

        public List<Matching> CalculateMatchings()
        {
            var matchings = new List<Matching>();
            foreach (var mentor in this.mentors)
            {
                var match = new Matching {Mentor = mentor, Dates = this.CalculateDates()};
                matchings.Add(match);
                this.mentorIndex++;
            }

            this.mentorIndex = 0;

            return matchings;
        }

        public List<IDate> CalculateDates()
        {
            var dates = new List<IDate>();
            var timeSlotIndex = 0;

            foreach (var timeSlot in this.timeSlots)
            {
                if (timeSlotIndex > this.mentees.Count - 1)
                {
                    dates.Add(new BreakDate() {Mentee = "-", TimeSlot = timeSlot});
                }
                else
                {
                    var potentialBreakDate = new Date()
                    {
                        Mentee = this.SelectMentee(this.mentorIndex, timeSlotIndex),
                        TimeSlot = timeSlot
                    };
                    Mentee fillerMentee = (Mentee)potentialBreakDate.Mentee;

                    if (timeSlot.IsBreak)
                    {
                        var breakDate = new BreakDate {Mentee = "-", TimeSlot = timeSlot};
                        dates.Add(breakDate);
                    }else if (fillerMentee != null && fillerMentee.IsFiller)
                    {
                        var breakDate = new BreakDate { Mentee = "-", TimeSlot = timeSlot };
                        dates.Add(breakDate);
                        timeSlotIndex++;
                    }
                    else if (!timeSlot.IsBreak)
                    {
                        var date = new Date
                            { Mentee = this.SelectMentee(this.mentorIndex, timeSlotIndex), TimeSlot = timeSlot };
                        dates.Add(date);
                        timeSlotIndex++;
                    }
                }
            }

            return dates;
        }

        public Mentee SelectMentee(int offset, int currentPosition)
        {
            var selectedMentee = new Mentee();
            //When there is no offset (or it is the length of the menteeList), just use the 'normal' MenteeList
            if (offset == 0 || offset == this.mentees.Count)
            {
                selectedMentee = this.mentees[currentPosition];
            }
            //When there is an offset, use a 'wrapped' MenteeList
            else if (offset > 0)
            {
                var updatedMenteeList = this.WrapMenteeList(this.mentees, offset);
                selectedMentee = updatedMenteeList[currentPosition];
            }

            return selectedMentee;
        }

        //Wraps around and Creates an updates List of Mentees for the offset
        public List<Mentee> WrapMenteeList(List<Mentee> mentees, int offset)
        {
            var wrappedMentees = new List<Mentee>();
            var internalCountdown = mentees.Count;
            int i = offset;
            while (internalCountdown >= 0)
            {
                wrappedMentees.Add(mentees[i]);
                i++;
                i %= mentees.Count;
                internalCountdown--;
            }

            return wrappedMentees;
        }

        private List<TimeSlot> CalculateTimeSlots(double breakDuration = 30)
        {
            var (dateDuration, amountOfTimeSlots) = CalculateDateDurationAndAmountOfTimeSlots(
                this.startTime.TimeOfDay.TotalMinutes, this.endTime.TimeOfDay.TotalMinutes, this.amountOfDates,
                breakDuration);
            this.DateDuration = dateDuration;
            DateTime timeSlotStartTime = this.startTime;
            DateTime nextDateTime = this.startTime;
            DateTime lastBreakTime = this.startTime;
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            for (var i = 0; i < amountOfTimeSlots; i++)
            {
                if (i == 0)
                {
                    var firstDateTimeSlot = new TimeSlot {IsBreak = false, Time = this.startTime};
                    timeSlots.Add(firstDateTimeSlot);
                    nextDateTime = this.startTime.AddMinutes(dateDuration);
                    timeSlotStartTime = nextDateTime;
                }
                else
                {
                    if (nextDateTime.TimeOfDay.TotalMinutes - lastBreakTime.TimeOfDay.TotalMinutes >= 120)
                    {
                        var breakTimeSlot = new TimeSlot {IsBreak = true, Time = timeSlotStartTime};
                        timeSlots.Add(breakTimeSlot);
                        lastBreakTime = breakTimeSlot.Time;
                        nextDateTime = timeSlotStartTime.AddMinutes(breakDuration);
                        timeSlotStartTime = nextDateTime;
                    }
                    else
                    {
                        var dateTimeSlot = new TimeSlot {IsBreak = false, Time = timeSlotStartTime};
                        timeSlots.Add(dateTimeSlot);
                        nextDateTime = timeSlotStartTime.AddMinutes(dateDuration);
                        timeSlotStartTime = nextDateTime;
                    }
                }
            }

            return timeSlots;
        }

        #region static helper methods

        private static List<Matching> ReplaceNoGoDatesWithBreaks(List<Matching> matchings,
            List<(Mentor, Mentee)> noGoDates)
        {
            foreach (var matching in matchings)
            {
                var dates = matching.Dates;
                //noGoDates.Any(m => m.Item1 == matching.Mentor)
                //var noGoDate = noGoDates.Select(n => (n.Item1, n.Item2)).Where());
                //noGoDates.Any(m => m.Item1 == matching.Mentor)
                //var noGomentee = dates.Where(d => d.Mentee==);
            }

            return matchings;
        }

        private static double CalculateAmountOfDates(int mentorCount, int menteeCount)
        {
            return mentorCount > menteeCount ? mentorCount : menteeCount;
        }

        private static (double, double) CalculateDateDurationAndAmountOfTimeSlots(double startTime, double endTime,
            double amountOfDates,
            double breakDuration)
        {
            var totalTime = (endTime - startTime);
            var amountOfBreaks = totalTime / 120;
            var amountOfTimeSlots = amountOfBreaks + amountOfDates;
            var reservedTimeForBreaks = amountOfBreaks * breakDuration;
            var reservedTimeForDates = (endTime - startTime) - reservedTimeForBreaks;

            var dateDuration = reservedTimeForDates / amountOfDates;
            ///////////////////////////////////////////////////////////////////////
            //Thresholds for Date-Durations, later to be replaced with variables.//
            ///////////////////////////////////////////////////////////////////////
            if (dateDuration < 20)
            {
                dateDuration = 20;
            }

            if (dateDuration > 45)
            {
                dateDuration = 45;
            }

            return (dateDuration, amountOfTimeSlots);
        }

        private List<Mentee> FixMenteeListSize(List<Mentee> mentees)
        {
            List<Mentee> newMentees = new List<Mentee>();
            foreach (var mentee in mentees)
            {
                newMentees.Add(mentee);
            }

            var missingNo = this.mentors.Count - mentees.Count;
            for (int i = 0; i < missingNo; i++)
            {
                var breakMentee = new Mentee() {IsFiller = true};
                newMentees.Add(breakMentee);
            }

            return newMentees;
        }

        private void DeleteLastDatesIfBreak(List<IDate> dates)
        {
            var mentee = dates.Last().Mentee;
            if (mentee is string)
            {
                dates.RemoveAt(dates.Count - 1);
                this.DeleteLastDatesIfBreak(dates);
            }
        }

        private List<Matching> TrimMatchings(List<Matching> matchings)
        {
            foreach (var matching in matchings)
            {
                this.DeleteLastDatesIfBreak(matching.Dates);
            }

            return matchings;
        }

        private List<IDate> RetrieveDates(List<Matching> matchings)
        {
            var count = 0;
            var longestDates = new List<IDate>();
            foreach (var matching in matchings)
            {
                if (matching.Dates.Count >= count)
                {
                    count = matching.Dates.Count;
                    longestDates = matching.Dates;
                }
            }

            return longestDates;
        }

        #endregion
    }
}