using System.Reflection;
using System.IO.Compression;

const string pluginFolderName = "DamageHistogramMod";
const string bepinexResourceName = "bepinex/BepInEx.zip";

var gameRoot = args.Length > 0 ? args[0] : PromptForGameRoot();
gameRoot = Path.GetFullPath(gameRoot.Trim('"'));
var gameRootWithSeparator = Path.EndsInDirectorySeparator(gameRoot)
	? gameRoot
	: gameRoot + Path.DirectorySeparatorChar;

var bepInExRoot = Path.Combine(gameRoot, "BepInEx");
var pluginsRoot = Path.Combine(bepInExRoot, "plugins");
var assembly = Assembly.GetExecutingAssembly();

if (!Directory.Exists(bepInExRoot))
{
	Console.WriteLine("BepInEx was not found. Installing bundled BepInEx IL2CPP files...");
	if (!InstallBepInEx(assembly, gameRoot, gameRootWithSeparator))
	{
		return 2;
	}
}
else
{
	Console.WriteLine("BepInEx already exists. Leaving the existing install in place.");
}

var targetDir = Path.Combine(pluginsRoot, pluginFolderName);
Directory.CreateDirectory(targetDir);

var resources = assembly
	.GetManifestResourceNames()
	.Where(name => name.StartsWith("payload/", StringComparison.OrdinalIgnoreCase))
	.ToArray();

if (resources.Length == 0)
{
	Console.Error.WriteLine("This installer was built without an embedded mod payload.");
	return 3;
}

foreach (var resource in resources)
{
	var relative = resource.Substring("payload/".Length).Replace('/', Path.DirectorySeparatorChar);
	var destination = Path.Combine(targetDir, relative);
	var destinationDir = Path.GetDirectoryName(destination);
	if (!string.IsNullOrEmpty(destinationDir))
	{
		Directory.CreateDirectory(destinationDir);
	}

	using var input = assembly.GetManifestResourceStream(resource);
	if (input == null)
	{
		Console.Error.WriteLine("Could not read embedded file: " + resource);
		return 4;
	}

	using var output = File.Create(destination);
	input.CopyTo(output);
	Console.WriteLine("Installed " + relative);
}

Console.WriteLine();
Console.WriteLine("Damage Histogram Mod installed to:");
Console.WriteLine(targetDir);
Console.WriteLine();
Console.WriteLine("Start Olden Era normally. BepInEx will finish its first-run setup if this is a fresh install.");
Console.WriteLine("The histogram appears on the battle damage preview.");
return 0;

static bool InstallBepInEx(Assembly assembly, string gameRoot, string gameRootWithSeparator)
{
	using var stream = assembly.GetManifestResourceStream(bepinexResourceName);
	if (stream == null)
	{
		Console.Error.WriteLine("This installer was built without an embedded BepInEx package.");
		return false;
	}

	try
	{
		using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
		foreach (var entry in archive.Entries)
		{
			if (string.IsNullOrEmpty(entry.Name))
			{
				continue;
			}

			var destination = Path.GetFullPath(Path.Combine(gameRoot, entry.FullName.Replace('/', Path.DirectorySeparatorChar)));
			if (!destination.StartsWith(gameRootWithSeparator, StringComparison.OrdinalIgnoreCase))
			{
				Console.Error.WriteLine("Refusing to extract unsafe BepInEx zip entry: " + entry.FullName);
				return false;
			}

			var dir = Path.GetDirectoryName(destination);
			if (!string.IsNullOrEmpty(dir))
			{
				Directory.CreateDirectory(dir);
			}

			entry.ExtractToFile(destination, overwrite: true);
		}
		Console.WriteLine("BepInEx installed.");
		return true;
	}
	catch (Exception ex)
	{
		Console.Error.WriteLine("BepInEx install failed: " + ex.Message);
		return false;
	}
}

static string PromptForGameRoot()
{
	Console.WriteLine("Olden Era Damage Histogram installer");
	Console.WriteLine();
	Console.WriteLine("Enter your Heroes of Might and Magic Olden Era install folder.");
	Console.WriteLine("Example: C:\\Program Files (x86)\\Steam\\steamapps\\common\\Heroes of Might and Magic Olden Era");
	Console.Write("> ");
	var value = Console.ReadLine();
	if (string.IsNullOrWhiteSpace(value))
	{
		Console.Error.WriteLine("No install folder was entered.");
		Environment.Exit(1);
	}
	return value;
}
