using System;

namespace VehicleDemo
{
    public class Car : Vehicle
    {

        public Car(String regNr, String make, String model, int year, bool forSale)
            : base(regNr, make, model, year, forSale)
        {
        }


        public override String ToStringList()
        {
            // Formaterar strängen med alla medlemmar förutom ...
            String s = String.Format("\t{0}\t{1}\t{2}\t{3}",
                this.RegNr, this.Make, this.Model, this.Year.ToString());

            if (this.ForSale) // ... ForSale som behöver..
            {
                s += "\tJA"; // ..annan formatering än..
            }
            else
            {
                s += "\tNEJ"; // ..den som redan finns.
            }

            return s; // returnerar strängen
        }
    }
}
