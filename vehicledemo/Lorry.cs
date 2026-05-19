using System;

namespace VehicleDemo
{
    public class Lorry : Vehicle
    {
        // Medlemsvariabler

        int load;    // maxlast, kg


        public Lorry(String regNr, String make, String model, int year, bool forSale, int load)
            : base(regNr, make, model, year, forSale)
        {
            this.Load = load;
        }


        public int Load
        {
            get { return load; }
            set { load = value; }
        }


        // ToStringList()
        // Metod som förbereder utskrift av lastbilsinformation i listform

        public override String ToStringList()
        {
            String fs;
            if (this.ForSale)
            {
                fs = "\tJA";
            }
            else
            {
                fs = "\tNEJ";
            }

            return String.Format("{0}\t{1}\t{2}\t{3} {4}\tMaxlast: {5}kg.",
                this.RegNr, this.Make, this.Model, this.Year.ToString(), fs, this.Load);
        }


        // ToString()
        // Metod som förbereder utskrift av lastbilsobjekt

        public new String ToString()
        {
            String s = base.ToString();
            s += String.Format("\nMaxlast: {0}kg.", this.Load);
            return s;
        }
    }
}
