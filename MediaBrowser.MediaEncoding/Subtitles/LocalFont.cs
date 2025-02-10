using System;
using MediaBrowser.Model.IO;

namespace MediaBrowser.MediaEncoding.Subtitles;

#pragma warning disable CS1591
#pragma warning disable SA1611, SA1615

/// <summary>
/// A single local font and its associated metadata.
/// </summary>
public class LocalFont
{
    public LocalFont(FileSystemMetadata file)
    {
        // TODO harfbuzz parse
        FamilyName = string.Empty;
        FileLastWriteTime = file.LastWriteTimeUtc;
    }

    public string FamilyName { get; }

    public DateTime FileLastWriteTime { get; }
}
