using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.IO;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Management;
using xNet;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace vAnonSniper
{
	//	Made by Valkoar#0001
	//	Help me get Coder and GodLike by leaving a like!
	class Program
	{
		static string hwid;
		static string key;
		private static string gethwid()
		{
			if (string.IsNullOrEmpty(hwid))
			{
				DriveInfo[] drives = DriveInfo.GetDrives();
				int num = drives.Length - 1;
				for (int i = 0; i <= num; i++)
				{
					DriveInfo driveInfo = drives[i];
					if (driveInfo.IsReady)
					{
						hwid = driveInfo.RootDirectory.ToString();
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(hwid) && hwid.EndsWith(":\\"))
			{
				hwid = hwid.Substring(0, hwid.Length - 2);
			}
			string result;
			using (ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + hwid + ":\""))
			{
				managementObject.Get();
				result = managementObject["VolumeSerialNumber"].ToString();
			}
			return result;
		}
		static void Main(string[] args)
		{
			Console.WriteLine("Cracked.to Auth Key?");
			key = Console.ReadLine();
			try
			{
				using (HttpRequest httpRequest = new HttpRequest())
				{
					httpRequest.IgnoreProtocolErrors = true;
					httpRequest.UserAgent = Http.ChromeUserAgent();
					httpRequest.ConnectTimeout = 30000;
					string text = httpRequest.Post("https://cracked.to/auth.php", "a=auth&k=" + key + "&hwid=" + gethwid(), "application/x-www-form-urlencoded").ToString();
					Dictionary<string, string> response = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
					if (text.Contains("error"))
					{
						MessageBox.Show("Error " + response["error"] + "!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else if (text.Contains("auth"))
					{
						MessageBox.Show("Welcome " + response["username"] + ", enjoy your exclusive Cracked.to tool!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				StartProgram();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
				Application.Exit(); return;
			}

		}

		static void StartProgram()
		{
			Console.Clear();
			Console.Title = "vAnonSniper | Snipe Hundreds of Private Files | Valkoar#0001";
			var Anon = new string[]
			{
				"			    .o oOOOOOOOo                                            OOOo",
				"			   Ob.OOOOOOOo  OOOo.      oOOo.                      .adOOOOOOO",
				@"			    OboO"""""""""""""""""""""""".OOo. .oOOOOOo.    OOOo.oOOOOOo..""""""""""""""""""""""'OO",
				@"			   OOP.oOOOOOOOOOOO ""POOOOOOOOOOOo.   `""OOOOOOOOOP,OOOOOOOOOOOB'",
				@"			   `O'OOOO'     `OOOOo""OOOOOOOOOOO` .adOOOOOOOOO""oOOO'    `OOOOo",
				"			    .OOOO'            `OOOOOOOOOOOOOOOOOOOOOOOOOO'            `OO",
				@"			    OOOOO                 '""OOOOOOOOOOOOOOOO""`                oOO",
				"			   oOOOOOba.                .adOOOOOOOOOOba               .adOOOOo.",
				"			  oOOOOOOOOOOOOOba.    .adOOOOOOOOOO@^OOOOOOOba.     .adOOOOOOOOOOOO",
				@"			 OOOOOOOOOOOOOOOOO.OOOOOOOOOOOOOO""`  '""OOOOOOOOOOOOO.OOOOOOOOOOOOOO",
				@"			 OOOOOOOOOOOOOOOOO.OOOOOOOOOOOOOO""`  '""OOOOOOOOOOOOO.OOOOOOOOOOOOOO",
				"			    Y           'OOOOOOOOOOOOOO: .oOOo. :OOOOOOOOOOO?'         :`",
				"			    :            .oO%OOOOOOOOOOo.OOOOOO.oOOOOOOOOOOOO?         .",
				"			    :            .oO%OOOOOOOOOOo.OOOOOO.oOOOOOOOOOOOO?         .",
				@"				                '%o  OOOO""%OOOO%""%OOOOO""OOOOOO""OOO':""",
				@"			                    `$""  `OOOO' `O""Y ' `OOOO'  o             .",
				@"			 .                  .     OP""          : o.",
				"			                             :",
				"			                            .",

			};

			foreach (string line in Anon) { Console.WriteLine(line, Color.Red); }
			Console.WriteLine();
			Console.WriteLine("What is the keyword you want to use?");
			string resp = Console.ReadLine();
			int count = 0;
			List<string> Links = new List<string>();
			using (WebClient wc = new WebClient())
			{
				string s = wc.DownloadString("https://www.google.com/search?q=site:anonfile.com+" + resp);
				Regex r = new Regex(@"https:\/\/anonfile.com\/\w+\/\w+");
				foreach (Match m in r.Matches(s))
				{
					count++;
					Links.Add(m.ToString());
				}
			}

			using (TextWriter tw = new StreamWriter(@"links.txt"))
			{
				foreach (string line in Links)
				{
					tw.WriteLine(line.ToString());
				}
			}

			Console.WriteLine();
			Console.WriteLine("Scraped " + count.ToString() + " links!");
			Console.ReadLine();
		}
	}
}
