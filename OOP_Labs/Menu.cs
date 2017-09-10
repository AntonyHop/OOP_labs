using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace OOP_Labs
{
	public class Menu
	{
		public String PROJ_PATH = "//home//antony//OOP//OOP_Labs//"; 
		public Menu (){
			show_main_screen ();
		}

		public void show_main_screen(){
			String[] folders = ProjectFolders ();
			String select_item = BootstrapMenu (folders,"Exit","Select Folder");
			if (select_item != "Exit") {
				string path_to_lib = select_item+"//bin//Debug//";
				try{
					String[] files = Directory.GetFiles (path_to_lib,"*.dll");
					String curr_file = BootstrapMenu (files,"Back","Select DLL:");

					if (curr_file == "Back") {
						BootstrapMenu (folders);
					} else {
						LoadDll (curr_file);
					}
				}catch(Exception ex){
					Console.WriteLine ("DLL NOT FOUND: "+ex.Message);
					Console.ReadKey ();
					show_main_screen ();
				}
			}
		}

		public String[] ProjectFolders(){
			String[] ret;
			try{
				ret = Directory.GetDirectories (this.PROJ_PATH,"lab*");
				return ret;
			}catch(DirectoryNotFoundException ex){
				Console.WriteLine (ex.Message);
				throw;
			}

		}

		public String BootstrapMenu(String[] folders,String option = "Exit",String msg = "Select:"){
			Boolean Close = false;

			Array.Resize(ref folders, folders.Length + 1);
			folders[folders.Length - 1] = option;

			int curr = 0;
			while (!Close) {

				Console.Clear ();
				Console.WriteLine (msg);
				for (int i= 0; i<Console.BufferWidth; i++) {
					Console.Write ("-");
				}
				for (int i = 0; i<folders.Length; i++) {

					DirectoryInfo di = new DirectoryInfo (folders [i]);

					if (i == curr) {
						Console.BackgroundColor = ConsoleColor.DarkCyan;
						Console.WriteLine (" >" + di.Name);
						Console.ResetColor ();
					} else {
						Console.WriteLine (di.Name);
					}
				}
				for (int i= 0; i<Console.BufferWidth; i++) {
					Console.Write ("-");
				}
				ConsoleKey key = Console.ReadKey ().Key;
				if (ConsoleKey.UpArrow == key) {
					if (curr > 0) {
						curr--;
					} else {
						curr = folders.Length - 1;
					}
				} else if (ConsoleKey.DownArrow == key) {
					if (curr < folders.Length - 1) {
						curr++;
					} else {
						curr = 0;
					}
				} else if (ConsoleKey.Enter == key) {
					Console.Clear ();
					return folders[curr];
				}
			}
			Console.Clear ();
			return "Nothing";
		}

		void LoadDll(String dll_path){
			Assembly asm = Assembly.LoadFile(dll_path);
			Type[] types = asm.GetTypes ();
			List<string> type_names = new List<string> ();
			foreach (Type t in types) {
				type_names.Add (t.Name);
			}
			String sel_type = BootstrapMenu (type_names.ToArray (),"Back","Select Class:");
			if (sel_type == "Back") {
				show_main_screen ();
			}
			int sel = type_names.IndexOf(sel_type);

			MethodInfo[] methods = types[sel].GetMethods (BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.Static);

			List <string> method_names = new List<string> ();
			foreach (var m in methods) {
				method_names.Add(m.Name);
			}
			String method  = BootstrapMenu (method_names.ToArray (),"Back","Select Method");
			if (method == "Back") {
				show_main_screen ();
			}
			int m_sel = method_names.IndexOf (method);
			try{
				methods [m_sel].Invoke (null, new object[] { });
			}catch(Exception ex){
				Console.WriteLine(ex.Message);
			}
			show_main_screen();
		}

		public void ShowAuthor(){
			Console.WriteLine ("Created BY Anton Kukushkin (MsRotate)");
		}
	}
}





