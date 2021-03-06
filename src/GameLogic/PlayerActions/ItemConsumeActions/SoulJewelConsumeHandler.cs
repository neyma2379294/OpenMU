﻿// -----------------------------------------------------------------------
// <copyright file="SoulJewelConsumeHandler.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace MUnique.OpenMU.GameLogic.PlayerActions.ItemConsumeActions
{
    using System.Linq;
    using MUnique.OpenMU.DataModel.Configuration.Items;
    using MUnique.OpenMU.DataModel.Entities;
    using MUnique.OpenMU.Persistence;

    /// <summary>
    /// Consume handler for the Jewel of Soul which increases the item level by one until the level of 9 with a chance of 50%.
    /// </summary>
    public class SoulJewelConsumeHandler : ItemModifyConsumeHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoulJewelConsumeHandler"/> class.
        /// </summary>
        /// <param name="repositoryManager">The repository manager.</param>
        public SoulJewelConsumeHandler(IRepositoryManager repositoryManager)
            : base(repositoryManager)
        {
        }

        /// <inheritdoc/>
        protected override bool ModifyItem(Item item)
        {
            if (item.Level > 8)
            {
                return false;
            }

            int percent = 50;
            if (ItemHasLuck(item))
            {
                percent += 25;
            }

            if (Rand.NextRandomBool(percent))
            {
                item.Level++;
                return true; // true doesnt mean that it was successful, just that the consumption happend.
            }

            if (item.Level > 6)
            {
                item.Level = 0;
            }
            else
            {
                item.Level--;
            }

            return true;
        }

        private static bool ItemHasLuck(Item item)
        {
            return item.ItemOptions.Any(o => o.ItemOption.OptionType == ItemOptionTypes.Luck);
        }
    }
}
