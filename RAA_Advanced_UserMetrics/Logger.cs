using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Advanced_UserMetrics
{
	internal class Logger
	{
		public static void Enter(bool logToDB = false, Document doc = null)
		{
			LogInfo("Entered Method", logToDB, doc);
		}

		public static void Exit(bool logToDB, Document doc,
			string results, ExpandoObject analytics)
		{
			LogInfo("Exited Method", logToDB, doc, results, analytics);
		}

		internal static void LogInfo(string message, bool logToDb = false, Document doc = null, string results = "", ExpandoObject analytics = null)
		{
			int frame = 1;
			var callingMethod = new System.Diagnostics.StackTrace(frame, false).GetFrame(0).GetMethod();
			var callingClass = callingMethod.DeclaringType;
			var classMethodName = $"{callingClass.FullName}.{callingMethod.Name}";
			var version = "v" + callingClass.Assembly.GetName().Version;

			var docPathName = GetDocName(doc);

			var logItem = new LogItem(results, analytics, classMethodName, version, docPathName, message);

			if(logToDb)
			{
				TextWriter.WriteToCSV(logItem.GetLogDataAsCSVString());
				SlackWriter.WriteToSlack(logItem.GetLogDataAsText());
				doc.Application.WriteJournalComment(logItem.GetLogDataAsText(), false);
			}
		}

		private static void LogError()
		{

		}

		private static void LogDebug()
		{

		}
		private static string GetDocName(Document doc)
		{
			var docTitle = "";
			if (doc != null)
			{
				var app = doc.Application;
				var username = app.Username;
				docTitle = doc.Title;
				if (docTitle.Contains(username))
					docTitle = docTitle.Replace("_" + username, "");
			}
			return docTitle;
		}
		internal static string ConvertDynamicToString(dynamic dataObject)
		{
			string result = "";
			string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dataObject);
			if (jsonStr != null && jsonStr != "{}")
			{
				result = jsonStr.Replace("\"", "").Replace(",", "|");
			}
			return result;
		}
	}

    public class  LogItem
    {
		// who
		public string User { get; set; } = Environment.UserName;

		// what 
		public string Results { get; set; } // results report
		public ExpandoObject Analytics { get; set; } // expando object
		public string AnalyticsStr { get; set; } 

		// where
		public string Method { get; set; }
		public string Version { get; set; }
		public string Filename { get; set; }

		// when 
		public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss");

		// why
		public string Message { get; set; }

		public LogItem(string results, ExpandoObject analytics, string method, string version, string filename, string message)
		{
			Results = results;
			Method = method;
			Version = version;
			Filename = filename;
			Message = message;
			Analytics = analytics;
			AnalyticsStr = Logger.ConvertDynamicToString(analytics);
		}
		public string GetLogDataAsCSVString()
		{
			return $"{Method}.{Version},{User},{Filename},{TimeStamp},{Results},{AnalyticsStr},{Message}";
		}

		public string GetLogDataAsText()
		{
			return $"{Method}.{Version} - {User} - {Filename} - {TimeStamp} - {Results} - {AnalyticsStr} - {Message}";
		}
	}
}
