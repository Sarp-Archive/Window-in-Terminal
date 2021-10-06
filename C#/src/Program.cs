using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleWindow
{
	class Program
	{
		#region Copy Part
		/*

			╔ ═ ╗

			║   ║

			╚ ═ ╝
			┌ ─ ┐

			│   │

			└ ─ ┘

		*/
		#endregion

		#region App Variables
		//App variables
		static int cW = Console.WindowWidth;
		static int cH = Console.WindowHeight;

		static int iW = cW - 2;
		static int iH = cH - 2;

		static int pwX = 52;
		static int pwY = 13;

		static int pbX = (iW - pwX) / 2;
		static int pbY = (iH - pwY) / 2;

		static int bW = Console.BufferWidth;
		static int bH = Console.BufferHeight;

		static bool logstate = false;
		static string passwd = "TermUser01";

		#endregion

		#region DLL
		[DllImport("User32.dll", CharSet = CharSet.Unicode)]
		public static extern int MessageBox(IntPtr h, string m, string c, int type);
		#endregion

		static void Main(string[] args)
		{
			//Draw Window
			init();

			//Start resize listener
			Thread t = new Thread(new ThreadStart(ConsoleResized));
			t.Start();

			//Entering password
			string pass = "";
			int l = 0;

			Console.SetCursorPosition(pbX + 16, pbY + 6);
			while (logstate == false)
			{

				ConsoleKeyInfo keyinfo = Console.ReadKey(true);
				ConsoleKey key = keyinfo.Key;
				char c = keyinfo.KeyChar;

				if (key == ConsoleKey.Backspace)
				{
					try
					{
						pass = pass.Substring(0,pass.Length - 1);
					}
					catch (Exception)
					{

					}

					if (l > 0) 
					{
						Console.CursorLeft -= 1;
						Console.Write(" ");
						Console.CursorLeft -= 1;
					}

					l--;
					if (l < 0)
					{
						l = 0;
					}
				}
				else if (key == ConsoleKey.Enter)
				{

					Console.SetCursorPosition(pbX + 16, pbY + 6);
					for (int i = 0; i < 20; i++)
					{
						Console.Write(" ");
					}
					Console.SetCursorPosition(pbX + 16, pbY + 6);
					l = 0;
					if (pass == passwd)
					{
						Console.SetCursorPosition(pbX + 18, pbY + 9);
						Console.Write("Correct password");
						Console.SetCursorPosition(pbX + 16, pbY + 6);
						logstate = true;
						init();
					}
					else
					{
						Console.SetCursorPosition(pbX + 19, pbY + 9);
						Console.Write("Wrong password");
						Console.SetCursorPosition(pbX + 16, pbY + 6);
					}
					pass = "";

				}
				else
				{
					//"[\u0000-\uFFFC]" => Includes some character which we may not want. Add characters manually instead.
					if (c != ' ' && l < 20 && Regex.IsMatch(c.ToString(), "[QWWERTYUIOPĞÜASDFGHJKLŞİ;ZXCVBNMÖÇ:qwertyuıopğüasdfghjklşi,zxcvbnmöç.1234567890!#$@%&?<>+'₺€-]"))
					{
						pass += c.ToString();
						Console.Write("*");
						l++;
					}
				}

			}
			
		}


		static void ConsoleResized()
		{
			while (true)
			{
				if (bW != Console.BufferWidth || bH != Console.BufferHeight) 
				{
					init();
				}
				bW = Console.BufferWidth;
				bH = Console.BufferHeight;
			}
		}

		static void init()
		{
			Console.Clear();

			reassignVars();
			drawWorkspace();
			drawPassWin();
		}

		static void reassignVars()
		{
			//Reassign variables
			cW = Console.WindowWidth;
			cH = Console.WindowHeight;
			iW = cW - 2;
			iH = cH - 2;
		}

		static void drawWorkspace(string title = "ConsoleWindow")
		{
			Console.Clear();
			Console.SetCursorPosition(0,0);

			//Top Part
			Console.Write("╔");
			for (int q = 0; q < iW; q++)
			{
				Console.Write("═");
			}
			Console.WriteLine("╗");

			//Middle Part
			for (int i = 0; i < iH; i++)
			{
				Console.Write("║");
				for (int q = 0; q < iW; q++)
				{
					Console.Write(" ");
				}
				Console.WriteLine("║");
			}

			//Bottom Part
			Console.Write("╚");
			for (int q = 0; q < iW; q++)
			{
				Console.Write("═");
			}
			Console.Write("╝");

			//Set Title
			Console.SetCursorPosition(3, 0);
			Console.WriteLine("| " + title + " |");
			Console.Title = title;
		}
	
		static void drawPassWin()
		{
			//Password Window

			if (logstate == false) 
			{
				int i1 = pbY;
				Console.SetCursorPosition(pbX, i1++);
				Console.Write("╔══════════════════════════════════════════════════╗"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║                                                  ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║                Welcome, Main User                ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║               Enter your password:               ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║                                                  ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║             ┌──────────────────────┐             ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║             │                      │             ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║             └──────────────────────┘             ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║                                                  ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║                                                  ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║               Press ENTER to login               ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("║                                                  ║"); Console.SetCursorPosition(pbX, i1++);
				Console.Write("╚══════════════════════════════════════════════════╝"); Console.SetCursorPosition(pbX, i1++);
			}
		}	
	}
}

#region LOGIN CODE BACKUP
/*
while (logstate == false)
{

	ConsoleKeyInfo keyinfo = Console.ReadKey(true);
	ConsoleKey key = keyinfo.Key;
	char c = keyinfo.KeyChar;

	if (key == ConsoleKey.Backspace)
	{
		try
		{
			pass = pass.Substring(0,pass.Length - 1);
		}
		catch (Exception)
		{

		}

		if (l > 0) 
		{
			Console.CursorLeft -= 1;
			Console.Write(" ");
			Console.CursorLeft -= 1;
		}

		l--;
		if (l < 0)
		{
			l = 0;
		}
	}
	else if (key == ConsoleKey.Enter)
	{
		MessageBox((IntPtr)0,pass,"Alert",0);
	}
	else
	{
		//MessageBox((IntPtr)0, "-" + c.ToString() + "-", "Alert", 0);
		if (c != ' ' && c != null && l < 20 && Regex.IsMatch(c.ToString(), "[A-Za-z0-9]"))
		{
			pass += c.ToString();
			Console.Write("*");
			l++;
		}
	}

}
*/
#endregion