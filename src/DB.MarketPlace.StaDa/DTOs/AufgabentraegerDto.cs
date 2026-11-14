namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class AufgabentraegerDto
{
    /// <summary>
    /// Unique identifier of the Aufgabentraeger
    /// </summary>
    /// <example>VBB</example>
    public string? ShortName { get; set; }

    /// <summary>
    /// Full name of the Aufgabentraeger
    /// </summary>
    /// <example>Verkehrsverbund Berlin-Brandenburg GmbH</example>
    public string? Name { get; set; }
}