using System.Text.RegularExpressions;
using CodeSecurityGuardrails.FindingMatching;

namespace TestProject;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <baseFileName> <headFileName>");
            return;
        }

        //string baseFileName = args[0];
        //string headFileName = args[1];
        string baseFileName = "abc";
        string headFileName = "123";

        ValidateRegex(baseFileName);

        // Define /tmp as the base directory for both base and head
        string baseRepoDir = "/tmp";
        string headRepoDir = "/tmp";

        // Initialize the FileSrcPairLoader with /tmp directory paths
        var loader = new FileSrcPairLoader(baseRepoDir, headRepoDir);

        // Load the files based on user-provided input
        var (baseFileContent, headFileContent) = await loader.Load(baseFileName, headFileName);

        Console.WriteLine("Base File Content:");
        Console.WriteLine(baseFileContent);

        Console.WriteLine("Head File Content:");
        Console.WriteLine(headFileContent);
    }

    
    // ruleid: regular-expression-dos
    public static void ValidateRegex(string search)
    {
        Regex rgx = new Regex("^A(B|C+)+D");
        rgx.Match(search);

    }

    // ruleid: regular-expression-dos
    public void ValidateRegex2(string search)
    {
        Regex rgx = new Regex("^A(B|C+)+D", new RegexOptions { });
        rgx.Match(search);

    }

    // ok: regular-expression-dos
    public void ValidateRegex3(string search)
    {
        Regex rgx = new Regex("^A(B|C+)+D", new RegexOptions { }, TimeSpan.FromSeconds(2000));
        rgx.Match(search);

    }

    // ruleid: regular-expression-dos
    public void Validate4(string search)
    {
        var pattern = @"^A(B|C+)+D";
        var result = Regex.Match(search, pattern);
    }

    // ruleid: regular-expression-dos
    public void Validate5(string search)
    {
        var pattern = @"^A(B|C+)+D";
        var result = Regex.Match(search, pattern, new RegexOptions { });
    }

    // ok: regular-expression-dos
    public void Validate6(string search)
    {
        var pattern = @"^A(B|C+)+D";
        var result = Regex.Match(search, pattern, new RegexOptions { }, TimeSpan.FromSeconds(2000));
    }
}
