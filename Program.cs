namespace DoomFire;

using System;
using System.Threading;
using DoomFire.Domain;  // ajuste namespace conforme seu projeto

class Program
{
	static void Main()
	{
		int w = 20, h = 10;
		int max = 36;

		var field = new FireField(w, h, max);
		var algo = new DoomFireAlgorithm(maxDecay: 3, windBias: 1);
		var rng = new Random(0);

		field.SeedBottomRow(max);

		for (int frame = 0; frame < 30; frame++)
		{
			Console.Clear();
			PrintNumbers(field);
			algo.Step(field, rng);
			Thread.Sleep(100);
		}
	}

	static void PrintNumbers(FireField field)
	{
		for (int y = 0; y < field.Height; y++)
		{
			for (int x = 0; x < field.Width; x++)
			{
				// "D2" alinha bonitinho: 00..36
				Console.Write(field.Get(x, y).ToString("D2"));
				Console.Write(' ');
			}
			Console.WriteLine();
		}
	}
	
	static void PrintAscii(FireField field)
	{
		const string ramp = " .:-=+*#%@"; // do “frio” pro “quente”
		int max = field.MaxIntensity;

		for (int y = 0; y < field.Height; y++)
		{
			for (int x = 0; x < field.Width; x++)
			{
				int v = field.Get(x, y);
				int idx = (int)Math.Round((double)v / max * (ramp.Length - 1));
				Console.Write(ramp[idx]);
				Console.Write(ramp[idx]); // duplica pra ficar mais “quadrado”
			}
			Console.WriteLine();
		}
	}
}




