﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using MentorSpeedDatingApp.Views;
using MentorSpeedDatingApp.ViewModel;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        public static void ShowMatchingWindow(MainViewModel mVm)
        {
            var window = new MatchingWindow();
            window.DataContext = new MatchingViewModel(mVm.Mentors, mVm.Mentees, mVm.StartTime, mVm.EndTime);
            window.Show();
        }
    }
}