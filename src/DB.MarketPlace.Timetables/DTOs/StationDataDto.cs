using System.Xml.Serialization;

namespace DB.MarketPlace.Timetables.DTOs;

/// <summary>
/// A transport object which keeps data for a station.
/// </summary>
public sealed class StationDataDto
{
    /// <summary>
    /// DS100 station code.
    /// </summary>
    [XmlAttribute("ds100")]
    public required string Ds100 { get; set; }

    /// <summary>
    /// EVA station number.
    /// </summary>
    [XmlAttribute("eva")]
    public required long Eva { get; set; }

    /// <summary>
    /// List of meta stations, separated by "|".
    /// </summary>
    [XmlAttribute("meta")]
    public string? Meta { get; set; }

    /// <summary>
    /// Station name.
    /// </summary>
    [XmlAttribute("name")]
    public required string Name { get; set; } = string.Empty;

    /// <summary>
    /// List of platforms, separated by "|".
    /// </summary>
    [XmlAttribute("p")]
    public string? Platforms { get; set; }
}