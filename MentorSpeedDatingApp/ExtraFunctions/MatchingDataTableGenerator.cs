using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MentorSpeedDatingApp.Models;

namespace MentorSpeedDatingApp.ExtraFunctions
{
    public class MatchingDataTableGenerator
    {

        public DataTable GenerateTable(List<Mentor> mentors, List<DateTime> timeSlots)
        {
            var table = new DataTable();


            #region Add Columns

            var timeColumn = new DataColumn()
            {
                ColumnName = "Time",
                DataType = Type.GetType("System.DateTime"),
                ReadOnly = false
            };
            table.Columns.Add(timeColumn);
            foreach (var mentor in mentors)
            {
                var mentorColumn = new DataColumn()
                {
                    ColumnName = mentor.ToString(),
                    DataType = System.Type.GetType("System.String"),
                    ReadOnly = true
                };
                table.Columns.Add(mentorColumn);
            }

            #endregion

            #region Add Rows
            

            //DEBUGGEN: NOT WORKING YET
            foreach (var timeSlot in timeSlots)
            {
                var itemArray = new object[mentors.Count + 1];
                itemArray[0] = timeSlot;
                var offset = 0;
                for (var mentorIndex = 0; mentorIndex < mentors.Count + 1; mentorIndex++)
                {
                    var mentor = mentors[mentorIndex];
                    itemArray[mentorIndex + 1] = mentor.MatchedMentees[offset];
                    offset++;
                }
                table.Rows.Add(itemArray);
            }

            #endregion

            return table;
        }
    }
}