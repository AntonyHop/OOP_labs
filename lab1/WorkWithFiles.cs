using System;
using System.IO;
namespace lab1
{
	public class WorkWiithFiles
	{
		const String PATH = "//home//antony//OOP//test_f//";
		public WorkWiithFiles ()
		{
			Console.WriteLine ("Work With File:");
		}

		public static void FileCreate(){
			Console.WriteLine ("Insert filename");
			String filename = Console.ReadLine ();
			if(File.Exists(PATH+filename+".txt")){
				Console.WriteLine ("file exists please insert other filename");
				FileCreate ();
			}else{
				File.Create (PATH + filename + ".txt");
				FileInfo fi = new FileInfo (PATH + filename + ".txt");
				Console.WriteLine ("length:" + fi.Length);
				Console.WriteLine ("Extantion" + fi.Extension);
				Console.WriteLine ("Directory " + fi.Directory);
				Console.WriteLine (" Directory name: " + fi.DirectoryName);
			}
			Console.ReadKey ();
		}

		public static void DirecroryCreate(){
			Console.WriteLine ("Insert dirname");
			String dirname = Console.ReadLine ();
			if (Directory.Exists (PATH+dirname)) {
				Console.WriteLine ("directory exists please insert other dirname");
			} else {
				Directory.CreateDirectory (PATH + dirname);
				ShowFolder ();
			}
			Console.ReadKey ();
		}

		public static void ShowFolder(){
			Console.Clear ();
			String[] arr = Directory.GetFiles (PATH);
			String[] dirs = Directory.GetDirectories (PATH);

			for (int  i = 0; i < arr.Length; i++) {
				Console.WriteLine ("FILE => " + arr [i]);
			}

			for (int  i = 0; i < dirs.Length; i++) {
				Console.WriteLine ("FOLDER => " + dirs [i]);
			}
			Console.ReadKey ();
		}


		public static void delete_file(){
			ShowFolder ();
			Console.WriteLine ("Print Delete File");
			String filename = Console.ReadLine ();
			if (File.Exists (PATH + filename + ".txt")) {
				File.Delete (PATH + filename + ".txt");
			} else {
				Console.WriteLine ("file not exisths");
			}
			ShowFolder ();
			Console.ReadKey ();
		}

		public static void rename_file(){
			ShowFolder ();
			Console.WriteLine ("Print Rename File");
			String filename = Console.ReadLine ();
			if (File.Exists (PATH + filename + ".txt")) {
				String new_filename = Console.ReadLine ();
				File.Move (PATH + filename + ".txt", PATH + new_filename + ".txt");
			} else {
				Console.WriteLine ("file not exisths");
			}
			ShowFolder ();
			Console.ReadKey ();
		}
	}
}

