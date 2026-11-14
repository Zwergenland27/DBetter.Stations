namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class LocalizedNamesDto
{
    /// <summary>
    /// Danish name of the station
    /// </summary>
    /// <example>Flensborg</example>
    public string? Dan { get; set; }

    /// <summary>
    /// Lower Sorbian name of the station
    /// </summary>
    /// <example>Chóśebuz gł.dw.</example>
    public string? Dsb { get; set; }

    /// <summary>
    /// North Frisian name of the station
    /// </summary>
    /// <example>Naibel</example>
    public string? Frr { get; set; }

    /// <summary>
    /// Upper Sorbian name of the station
    /// </summary>
    /// <example>Budyšin</example>
    public string? Hsb { get; set; }

    /// <summary>
    /// Low German name of the station
    /// </summary>
    /// <example>Hamburg Hbf</example>
    public string? Nds { get; set; }
}