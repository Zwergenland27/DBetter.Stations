namespace DB.MarketPlace.StaDa.DTOs;

internal class StationSearchResponseDto
{
    /// <summary>
    /// Maximum number of result objects to be returned
    /// </summary>
    /// <example>10000</example>
    public long? Limit { get; set; }

    /// <summary>
    /// Offset of the first result object with respect to the total number of hits produced by the query
    /// </summary>
    /// <example>0</example>
    public long? Offset { get; set; }

    /// <summary>
    /// Total number of hits produced by the query
    /// </summary>
    /// <example>1</example>
    public long? Total { get; set; }

    /// <summary>
    /// Result objects produced by the query
    /// </summary>
    public List<StationDto>? Result { get; set; }
}