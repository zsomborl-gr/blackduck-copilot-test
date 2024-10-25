using System.IO;
using System.Threading.Tasks;

namespace CodeSecurityGuardrails.FindingMatching;

/// <summary>
///     Loads file source code from disk
/// </summary>
public class FileSrcPairLoader
{
    private readonly string _baseRepoDir;
    private readonly string _headRepoDir;

    public FileSrcPairLoader(string baseRepoDir, string headRepoDir)
    {
        _baseRepoDir = baseRepoDir;
        _headRepoDir = headRepoDir;
    }

    public async Task<(string BaseSrc, string HeadSrc)> Load(string baseFile, string headFile)
    {
        var baseFileSrc = string.Empty;
        var baseFilePath = Path.Combine(_baseRepoDir, baseFile);
        if (Path.Exists(baseFilePath))
        {
            baseFileSrc = await File.ReadAllTextAsync(baseFilePath);
        }
        var headFileSrc = string.Empty;
        var headFilePath = Path.Combine(_headRepoDir, headFile);
        if (Path.Exists(headFilePath))
        {
            headFileSrc = await File.ReadAllTextAsync(headFilePath);
        }

        return (baseFileSrc, headFileSrc);
    }
}
