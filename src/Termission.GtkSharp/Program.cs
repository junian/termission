using System;
using Eto.Forms;

namespace Juniansoft.Termission.GtkSharp
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			new Application(Eto.Platforms.Gtk).Run(new Form());
		}
	}
}
