/*
* JSON_Beautify.cs
* by Joe DF
* 
*/
using System.IO;
using System.Text;
using System;

namespace JSON_BnU
{

	/* Example
	class Program
	{
		static void Main(string[] args)
		{
			string raw = File.ReadAllText("example.json");
			Console.WriteLine(JSON.Beautify(raw,"4"));
			Console.ReadKey();
		}
	}
	*/

	public class JSON
	{
		
		public static string Uglify(string JSON) {
			JSON = JSON.Trim();
			int len = JSON.Length;
			if (len==0)
				return "";
			StringBuilder j = new StringBuilder(JSON);
			j.Replace(System.Environment.NewLine, string.Empty);
			j.Replace("\n", string.Empty);
			j.Replace("\r", string.Empty);
			j.Replace("\t", string.Empty);
			j.Replace("\f", string.Empty);
			j.Replace("\b", string.Empty);
			JSON = j.ToString();
			
			string _JSON = string.Empty;
			bool in_str = false;
			int c;
			char ch;
			char l_char = '\0';
			
			for(c = 0; c < len; c++) {
				ch = JSON[c]; 
				if ( (!in_str) && (ch==' ') )
					continue;
				if( (ch=='\"') && (l_char!='\\') )
					in_str = (!in_str);
				l_char = ch;
				_JSON += ch.ToString();
			}
			return _JSON;
		}
		
		public static string Beautify(string JSON, string gap) {
			//fork of http://pastebin.com/xB0fG9py
			JSON = Uglify(JSON);
			
			string indent = string.Empty;
			
			int _gap = 0;
			if ( int.TryParse(gap, out _gap) == true) {
				int i=0;
				while (i < _gap) {
					indent += " ";
					i+=1;
				}
			} else {
				indent = gap;
			}
			
			string _JSON = string.Empty;
			bool in_str = false;
			int k = 0;
			int c;
			int x;
			string _s = string.Empty;
			char ch = '\0';
			char l_char = '\0';
			int len = JSON.Length;
			string nl = System.Environment.NewLine;
			
			for(c = 0; c < len; c++) {
				ch = JSON[c];
				if (!in_str) {
					if ( (ch=='{') || (ch=='[') ) {
						
						_s = string.Empty;	
						++k;						
						for (x = 1; x < (k)+1; x++)
							_s += indent;
						
						_JSON += ch.ToString() + nl + _s;
						continue;
					}
					else if ( (ch=='}') || (ch==']') ) {
						
						_s = string.Empty;	
						--k;						
						for (x = 1; x < (k)+1; x++)
							_s += indent;
						
						_JSON += nl + _s + ch.ToString();
						continue;
					}
					else if ( (ch==',') ) {
						
						_s = string.Empty;					
						for (x = 1; x < (k)+1; x++)
							_s += indent;
						
						_JSON += ch.ToString() + nl + _s;
						continue;
					}
				}
				if( (ch=='\"') && (l_char!='\\') )
					in_str = (!in_str);
				l_char = ch;
				_JSON += ch.ToString();
			}
			return _JSON;
		}
		
	}

}