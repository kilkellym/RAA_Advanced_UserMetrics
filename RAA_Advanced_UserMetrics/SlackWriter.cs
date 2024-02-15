using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Advanced_UserMetrics
{
	internal class SlackWriter
	{
		// step 1. create a channel in Slack for your add-in notifications
		//          call it "Revit Add-in Notifications" or something like that

		// step 2. create a new app in Slack
		//          File > Settings & Administration > Manage Apps
		//          Click "Build" in upper right corner
		//          Click "Create New App" then click "From scratch"
		//          Give your app a name like "Revit Add-in Messenger" and select a workshop it will

		// step 3. install the Slack Webhooks NuGet package
		//          
		//          In Visual Studio, go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution to open the package manager
		//          Search for "slack webhooks" and select the Slack.Webhooks package. 
		//          Select your project in the list and click "Install"

		// step 4. write your Slack connection code

		internal static void WriteToSlack(string message)
		{
			string slackURL = @"https://hooks.slack.com/services/ENTER-YOUR-HOOK-URL-HERE";
			
			SlackClient slackClient = new SlackClient(slackURL);
			SlackMessage slackMessage = new SlackMessage();

			slackMessage.Text = message;

			slackClient.Post(slackMessage);
		
		}
	}
}
