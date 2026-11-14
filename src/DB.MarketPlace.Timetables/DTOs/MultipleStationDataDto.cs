using System.Xml.Serialization;

namespace DB.MarketPlace.Timetables.DTOs;

/// <summary>
/// A wrapper that represents multiple StationData objects.
/// </summary>
[XmlRoot("stations")]
public class MultipleStationDataDto
{
    /// <summary>
    /// List of stations with additional data.
    /// </summary>
    [XmlElement("station")]
    public List<StationDataDto> Station { get; set; } = [];
}