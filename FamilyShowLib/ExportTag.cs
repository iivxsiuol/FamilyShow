﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.FamilyShowLib
{
    public enum ExportTagPersonType
    {
        Root,
        BirthDate,
        BirthPlace,
        DeathDate,
        DeathPlace,
        LastName,
        FirstName,
        FatherFirstName,
        FatherLastName,
        MotherFirstName,
        MotherLastName,
        OrderIntoSiblings,
        GenealogicalNumber,
        MariageDate,
        MariagePlace,
        MariagePartnerGenre,
        MariagePartnerFirstName,
        MariagePartnerLastName
    }

    public enum ExportTagGenerationType
    {
        Root,
        NumberBirth,
        NumberDeath,
        NumberPerson
    }

    public interface IExportTag
    {
        string Name { get; set; }
        List<IExportTag> Childs { get; set; }

        string GetValue(Person rootPers, string genealogicalNumber, int childNumber);
        string GetValue(Person rootPers, SpouseRelationship spouseRelationShip, string genealogicalNumber, int childNumber);
    }

    public class ExportTagPerson : IExportTag
    {
        public string Name { get; set; }

        public List<IExportTag> Childs { get; set; }

        public ExportTagPersonType Type { get; set; }
        //public ExportTagGenerationType? GenerationType { get; set; }


        public ExportTagPerson(string name, ExportTagPersonType persontype)
        {
            Childs = new List<IExportTag>();
            Name = name;
            Type = persontype;
        }

        public string GetValue(Person rootPers, string genealogicalNumber, int childNumber)
        {
            switch (Type)
            {
                case ExportTagPersonType.Root:
                    return string.Empty;

                case ExportTagPersonType.BirthDate:
                    return rootPers.BirthDate?.ToString("dd MMMM yyyy");

                case ExportTagPersonType.BirthPlace:
                    return rootPers.BirthPlace;

                case ExportTagPersonType.DeathDate:
                    return rootPers.DeathDate?.ToString("dd MMMM yyyy");

                case ExportTagPersonType.DeathPlace:
                    return rootPers.DeathPlace;

                case ExportTagPersonType.LastName:
                    return rootPers.LastName;

                case ExportTagPersonType.FirstName:
                    return rootPers.FirstName;

                case ExportTagPersonType.FatherFirstName:
                    return rootPers.Father?.FirstName;

                case ExportTagPersonType.FatherLastName:
                    return rootPers.Father?.LastName;

                case ExportTagPersonType.MotherFirstName:
                    return rootPers.Mother?.FirstName;

                case ExportTagPersonType.MotherLastName:
                    return rootPers.Mother?.LastName;

                case ExportTagPersonType.OrderIntoSiblings:
                    return $"{childNumber}";

                case ExportTagPersonType.GenealogicalNumber:
                    return genealogicalNumber;

                default:
                    break;
            }

            return "NO YET IMPLEMENTED";
        }

        public string GetValue(Person rootPers, SpouseRelationship spouseRelationShip, string genealogicalNumber, int childNumber)
        {
            switch (Type)
            {
                case ExportTagPersonType.MariageDate:
                    return spouseRelationShip.MarriageDate?.ToString("dd MMMM yyyy");
                case ExportTagPersonType.MariagePlace:
                    return spouseRelationShip.MarriagePlace;
                case ExportTagPersonType.MariagePartnerGenre:
                    return spouseRelationShip.RelationTo?.Gender.ToString();
                case ExportTagPersonType.MariagePartnerFirstName:
                    return spouseRelationShip.RelationTo?.FirstName;
                case ExportTagPersonType.MariagePartnerLastName:
                    return spouseRelationShip.RelationTo?.LastName;
                default:
                    return GetValue(rootPers, genealogicalNumber, childNumber);
            }
        }

        internal static List<SpouseRelationship> ListSpouseRelationShip(Person person, int startYear)
        {
            List<SpouseRelationship> lst = new List<SpouseRelationship>();

            // on cherche toutes les relations
            foreach (Relationship rel in person.Relationships)
            {
                if (rel.RelationshipType == RelationshipType.Spouse)
                {
                    SpouseRelationship spouseRel = ((SpouseRelationship)rel);
                    if (spouseRel.MarriageDate != null && spouseRel.MarriageDate?.Year >= startYear)
                    {
                        lst.Add(spouseRel);
                    }
                }
            }

            return lst;
        }

        //internal Person GetMariage(Person root)
        //{
        //    foreach (SpouseRelationship spouseRel in root.ListSpousesRelationShip)
        //    {
        //        if (spouseRel.MarriageDate != null && spouseRel.MarriageDate?.Year >= annéeDepart)
        //        {
        //            Person spouse = spouseRel.RelationTo;
        //            Mariage mar = new Mariage();
        //            mar.Nom = person.LastName;
        //            mar.Prenom = person.FirstName;
        //            mar.NomRapportée = spouse.LastName;
        //            mar.PrenomRapportée = spouse.FirstName;
        //            mar.NumeroGenealogique = currentArbreLevelStr;
        //            mar.DateMariage = (DateTime)spouseRel.MarriageDate;
        //            mar.LieuMariage = spouseRel.MarriagePlace;

        //            if (spouse.Gender == Gender.Female)
        //                mar.GenreRapportée = "Mlle";
        //            else
        //                mar.GenreRapportée = "Mr.";
        //            lstMariage.Add(mar);
        //            //mar.LieuMariage = spouseRel.
        //        }
        //    }
        //}
    }
}
