using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Advanced_UserMetrics
{
	internal class TextWriter
	{
		private const string logPath = @"C:\temp\_MyUseageLog.csv";
		public static void WriteToCSV(string logItem)
		{
			System.IO.File.AppendAllText(logPath, logItem + Environment.NewLine);
		}
	}
}
