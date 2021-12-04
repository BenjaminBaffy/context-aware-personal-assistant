using System.Collections.Generic;
using System.Linq;
using Assistant.Domain;
using Assistant.Domain.DatabaseModel;

namespace Assistant.Application.Interfaces
{
    public interface ISlotDifferenceCalculator
    {
        IEnumerable<UserSlot> CalculateDifferencesForDatabase(IEnumerable<UserSlot> databaseSlots, IEnumerable<Slot> rasaSlots);
        IEnumerable<Slot> CalculateDifferencesForRasa(IEnumerable<UserSlot> databaseSlots, IEnumerable<Slot> rasaSlots);
    }

    public class SlotDifferenceCalculator : ISlotDifferenceCalculator
    {
        public IEnumerable<UserSlot> CalculateDifferencesForDatabase(IEnumerable<UserSlot> databaseSlots, IEnumerable<Slot> rasaSlots)
        {
            var deltaSlots = new List<UserSlot>();

            // TODO: review and simplify
            foreach (var rasaSlot in rasaSlots)
            {
                var matchingDbSlot = databaseSlots.Where(s => s.Key == rasaSlot.Key).FirstOrDefault();

                if (matchingDbSlot == null && (rasaSlot.Value != null || rasaSlot.Values != null))
                {
                    deltaSlots.Add(new UserSlot
                    {
                        Key = rasaSlot.Key,
                        Value = rasaSlot.Value,
                        Values = rasaSlot.Values
                    });

                    continue;
                }

                if (matchingDbSlot != null)
                {
                    if (matchingDbSlot.Value != rasaSlot.Value)
                    {
                        matchingDbSlot.Value = rasaSlot.Value;
                        deltaSlots.Add(matchingDbSlot);
                        continue;
                    }

                    if ((matchingDbSlot.Values != null || rasaSlot.Values != null))
                    {
                        if (matchingDbSlot.Values != null && rasaSlot.Values != null && !matchingDbSlot.Values.All(rasaSlot.Values.Contains))
                        {
                            matchingDbSlot.Values = rasaSlot.Values;
                            deltaSlots.Add(matchingDbSlot);
                        }
                        else
                        {
                            matchingDbSlot.Values = rasaSlot.Values;
                            deltaSlots.Add(matchingDbSlot);
                        }
                    }
                }
            }

            return deltaSlots;
        }

        public IEnumerable<Slot> CalculateDifferencesForRasa(IEnumerable<UserSlot> databaseSlots, IEnumerable<Slot> rasaSlots)
        {
            var deltaSlots = new List<Slot>();

            foreach (var databaseSlot in databaseSlots)
            {
                var matchingRasaSlot = rasaSlots.Where(s => s.Key == databaseSlot.Key).FirstOrDefault();

                if (matchingRasaSlot != null)
                {
                    if (matchingRasaSlot.Value != databaseSlot.Value)
                    {
                        matchingRasaSlot.Value = databaseSlot.Value;
                        deltaSlots.Add(matchingRasaSlot);
                    }
                    else if (matchingRasaSlot.Values == null ||
                        (matchingRasaSlot.Values != null && !matchingRasaSlot.Values.All(databaseSlot.Values.Contains)))
                    {
                        matchingRasaSlot.Values = databaseSlot.Values;
                        deltaSlots.Add(matchingRasaSlot);
                    }
                }
            }

            return deltaSlots;
        }
    }
}
