using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace MentorSpeedDatingApp.ExtraFunctions
{
    public class TimeSlotCalculator
    {
        public List<DateTime> CalculateTimeSlots(DateTime startTime, DateTime endTime, int minDates)
        {
            var totalMinutes = endTime.Subtract(startTime).TotalMinutes;
            var minutesInATimeSlot = totalMinutes / minDates;
            var timeSlots = new List<DateTime>(); 

            #region Calculate next Date Start Time

            var nextBreakTime = startTime.AddMinutes(120);
            var nextTimeSlotStartTime = startTime;
            var breakDuration = 30;
            for (int i = 0; i < minDates; i++)
            {
                if (i == 0)
                {
                    //Die Startzeit als TimeSlot hinzufügen
                    timeSlots.Add(startTime);
                    continue;
                }
                //Nächsten TimeSlot berechnen
                nextTimeSlotStartTime = nextTimeSlotStartTime.AddMinutes(minutesInATimeSlot);
                //Die Startzeit des nächsten TimeSlots darf nicht über der nächsten Pause (dynamisch berechnet) liegen...
                if (nextTimeSlotStartTime <= nextBreakTime)
                {
                    timeSlots.Add(nextTimeSlotStartTime);
                }
                else
                {
                    //Die nächste Pause wird als TimeSlot hinzugefügt
                    timeSlots.Add(nextBreakTime);
                    //Die StartZeit für das nächste Date wird nach der Pause festgelegt.
                    nextTimeSlotStartTime = nextBreakTime.AddMinutes(breakDuration);
                    //Die Zeit für die nächste Pause wird festgelegt.
                    nextBreakTime = nextBreakTime.AddMinutes(120 + breakDuration);
                }
            }

            #endregion





            return timeSlots;
        }
    }
}