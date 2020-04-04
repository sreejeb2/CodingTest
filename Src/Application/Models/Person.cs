using System.Collections.Generic;
using AGLCodingTest.Application.Models;

namespace AGLCodingTest.Application.Common.Models
{
    /// <summary>
    /// Represents the model entity for person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.Pets = new List<Pet>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Gender"/> value.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets the list of <see cref="Pet"/> objects.
        /// </summary>
        public IEnumerable<Pet> Pets { get; }
    }
}
