﻿using Community_Intel_Reporting_System.DAL;
using Community_Intel_Reporting_System.Service_Layer;
using Community_Intel_Reporting_System.UI;
using System;

namespace Community_Intel_Reporting_System.Service_LayerQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n--- Welcome to the  MALSHINON System ---");
            SystemUI systemUI = new SystemUI();
            systemUI.Start();

        }
    }
}
