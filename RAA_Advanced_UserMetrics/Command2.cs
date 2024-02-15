#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Dynamic;

#endregion

namespace RAA_Advanced_UserMetrics
{
	[Transaction(TransactionMode.Manual)]
	public class Command2 : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// this is a variable for the Revit application
			UIApplication uiapp = commandData.Application;

			// this is a variable for the current Revit model
			Document doc = uiapp.ActiveUIDocument.Document;

			Logger.Enter(false, doc);

			// Your code goes here
			TaskDialog.Show("Command 2", "This is Command 2");

			string results = "This is the result of Command 2";
			dynamic analytics = new ExpandoObject();
			analytics.numberOfScopeBoxes = 3;

			Logger.Exit(true, doc, results, analytics);


			return Result.Succeeded;
		}
		internal static PushButtonData GetButtonData()
		{
			// use this method to define the properties for this command in the Revit ribbon
			string buttonInternalName = "btnCommand2";
			string buttonTitle = "Button 2";

			ButtonDataClass myButtonData1 = new ButtonDataClass(
				buttonInternalName,
				buttonTitle,
				MethodBase.GetCurrentMethod().DeclaringType?.FullName,
				Properties.Resources.Blue_32,
				Properties.Resources.Blue_16,
				"This is a tooltip for Button 2");

			return myButtonData1.Data;
		}
	}
}
