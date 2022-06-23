﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VisitorManagment.Core.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            var aa = pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value) + " " +pc.GetHour(value).ToString("00") + ":" + pc.GetMinute(value).ToString("00") + ":" + pc.GetSecond(value).ToString("00");
            return aa;
        }
        public static string StringToDate(this string value)
        {
            if (value==null || value=="")
            {
                return value;
            }
            string Year = value.Substring(0, 4);
            string month = value.Substring(4, 2);
            string day = value.Substring(6, 2);
            string date = Year + "/" + month + "/" + day;
            return date;
        }

    }
}
