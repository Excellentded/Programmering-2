using System;

namespace VehicleDemo
{
    public abstract class Vehicle
    {
        
        // Medlemsvariabler
        
        private String regNr;    // registreringsnummer
        private String make;     // bilmärke
        private String model;    // bilmodell
        private int year;        // årsmodell
        private bool forSale;    // bilen till salu?

        public Vehicle() { }

        public Vehicle(String regNr, String make, String model, int year, bool forSale)
        {
            this.RegNr = regNr;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.ForSale = forSale;
        }

        public String RegNr
        {
            get { return regNr; }
            set { regNr = value; }
        }

        public String Make
        {
            get { return make; }
            set { make = value; }
        }

        public String Model
        {
            get { return model; }
            set { model = value; }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (value < 1900)
                {
                    year = -1;
                }
                else
                {
                    year = value;
                }
            }
        }

        public bool ForSale
        {
            get { return forSale; }
            set { forSale = value; }
        }

        public String YearToString()
        {
            if (this.year == -1)
            {
                return "felaktigt årtal";
            }
            else
            {
                return Convert.ToString(year);
            }
        }

        public String ForSaleToString()
        {
            if (this.ForSale)
            {
                return "Bilen är till salu";
            }
            else
            {
                return "Bilen är inte till salu";
            }
        }

        public override String ToString()
        {
            return String.Format(
                "\nBilinformaton\nReg: {0}, {1} {2} {3}\n{4}",
                this.RegNr, this.Make, this.Model, this.YearToString(), this.ForSaleToString()
            );
        }

        public abstract String ToStringList();
    }
}
