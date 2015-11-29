using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public class ContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DalContext>
    {
        protected override void Seed(DalContext context)
        {
            var equipments = new List<Equipment>
            {
                new Equipment {Name = "Caterpillar bulldozer", Type = InventoryType.Heavy},
                new Equipment {Name = "KamAZ truck", Type = InventoryType.Regular},
                new Equipment {Name = "Komatsu crane", Type = InventoryType.Heavy},
                new Equipment {Name = "Volvo steamroller", Type = InventoryType.Regular},
                new Equipment {Name = "Bosch jackhammer", Type = InventoryType.Specialized}
            };
            equipments.ForEach(eq => context.Equipments.Add(eq));
            context.SaveChanges();

            var fees = new List<Fee>
            {
                new Fee {Type = FeeType.OneTime, FeeValue = 100},
                new Fee {Type = FeeType.PremiumDaily, FeeValue = 60},
                new Fee {Type = FeeType.RegularDaily, FeeValue = 40},
            };
            fees.ForEach(f => context.Fees.Add(f));
            context.SaveChanges();

            var loyaltyPoints = new List<LoyaltyPoint>
            {
                new LoyaltyPoint { InventoryType = InventoryType.Heavy, Points = 2 },
                new LoyaltyPoint { InventoryType = InventoryType.Regular, Points = 1 },
                new LoyaltyPoint { InventoryType = InventoryType.Specialized, Points = 1 }
            };
            loyaltyPoints.ForEach(lp => context.LoyaltyPoints.Add(lp));
            context.SaveChanges();
        }

    }
}
