using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Model.IO;

namespace MediaBrowser.MediaEncoding.Subtitles;

#pragma warning disable CS1591
#pragma warning disable SA1611, SA1615

/// <summary>
/// Database of all local fonts used for subtitle subsetting.
/// </summary>
public class LocalFonts
{
    private readonly IServerConfigurationManager _serverConfigurationManager;
    private readonly IFileSystem _fileSystem;

    private Dictionary<string, LocalFont> _fonts = new();

    public LocalFonts(
        IFileSystem fileSystem,
        IServerConfigurationManager serverConfigurationManager)
    {
        _fileSystem = fileSystem;
        _serverConfigurationManager = serverConfigurationManager;
    }

    public void Refresh()
    {
        var encodingOptions = _serverConfigurationManager.GetEncodingOptions();
        var fallbackFontPath = encodingOptions.FallbackFontPath;

        // TODO remove old, missing files?
        foreach (var file in _fileSystem.GetFiles(fallbackFontPath))
        {
            if (!_fonts.TryGetValue(file.Name, out var localFont) // Corresponding LocalFont does not exist yet
                || localFont.FileLastWriteTime != file.LastWriteTimeUtc) // File has changed
            {
                _fonts[file.Name] = new LocalFont(file);
            }
        }
    }

    /// <summary>
    /// Retrieve the font with the associated family name, if any.
    /// </summary>
    public LocalFont? QueryByFamilyName(string familyName)
    {
        return _fonts.GetValueOrDefault(familyName);
    }
}
