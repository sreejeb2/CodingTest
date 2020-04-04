namespace AGLCodingTest.Application.Models
{
    /// <summary>
    /// Represents the model entity for pet.
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> value.
        /// </summary>
        public AnimalType Type { get; set; }
    }
}
