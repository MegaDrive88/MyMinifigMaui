using MauiBeadando2.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBeadando2.Database {
    public class MinifigDatabase
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database != null)
                return;
            
            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            
            await database.CreateTableAsync<Part>();
            await database.CreateTableAsync<Minifig>();
        }

        public async Task<ObservableCollection<Minifig>> GetAllItemsAsync() {
            await Init();
            
            var minifigs = await database!.Table<Minifig>().ToListAsync();
            
            foreach (var minifig in minifigs) {
                await LoadMinifigParts(minifig);
                minifig.IsDeleteReady = true;
            }
            
            return new ObservableCollection<Minifig>(minifigs);
        }

        private async Task LoadMinifigParts(Minifig minifig) {
            if (!string.IsNullOrEmpty(minifig.HeadPartId))
                minifig.HeadPart = await database!.FindAsync<Part>(minifig.HeadPartId);
            
            if (!string.IsNullOrEmpty(minifig.TorsoPartId))
                minifig.TorsoPart = await database!.FindAsync<Part>(minifig.TorsoPartId);
            
            if (!string.IsNullOrEmpty(minifig.LegPartId))
                minifig.LegPart = await database!.FindAsync<Part>(minifig.LegPartId);
            
            if (!string.IsNullOrEmpty(minifig.HeadItemId))
                minifig.HeadItem = await database!.FindAsync<Part>(minifig.HeadItemId);
            
            if (!string.IsNullOrEmpty(minifig.BackItemId))
                minifig.BackItem = await database!.FindAsync<Part>(minifig.BackItemId);
            
            if (!string.IsNullOrEmpty(minifig.AccessoryId))
                minifig.Accessory = await database!.FindAsync<Part>(minifig.AccessoryId);
            
            if (!string.IsNullOrEmpty(minifig.HeadwearAccessoryId))
                minifig.HeadwearAccessory = await database!.FindAsync<Part>(minifig.HeadwearAccessoryId);
            
            if (!string.IsNullOrEmpty(minifig.HipwearId))
                minifig.Hipwear = await database!.FindAsync<Part>(minifig.HipwearId);
        }

        public async Task<int> SaveMinifigAsync(Minifig minifig) {
            await Init();

            minifig.HeadPartId = minifig.HeadPart?.part_num;
            minifig.TorsoPartId = minifig.TorsoPart?.part_num;
            minifig.LegPartId = minifig.LegPart?.part_num;
            minifig.HeadItemId = minifig.HeadItem?.part_num;
            minifig.BackItemId = minifig.BackItem?.part_num;
            minifig.AccessoryId = minifig.Accessory?.part_num;
            minifig.HeadwearAccessoryId = minifig.HeadwearAccessory?.part_num;
            minifig.HipwearId = minifig.Hipwear?.part_num;

            if (minifig.HeadPart != null) {
                await database!.InsertOrReplaceAsync(minifig.HeadPart);
            }
            if (minifig.TorsoPart != null) {
                await database!.InsertOrReplaceAsync(minifig.TorsoPart);
            }
            if (minifig.LegPart != null) {
                await database!.InsertOrReplaceAsync(minifig.LegPart);
            }
            if (minifig.HeadItem != null) {
                await database!.InsertOrReplaceAsync(minifig.HeadItem);
            }
            if (minifig.BackItem != null) {
                await database!.InsertOrReplaceAsync(minifig.BackItem);
            }
            if (minifig.Accessory != null) {
                await database!.InsertOrReplaceAsync(minifig.Accessory);
            }
            if (minifig.HeadwearAccessory != null) {
                await database!.InsertOrReplaceAsync(minifig.HeadwearAccessory);
            }
            if (minifig.Hipwear != null) {
                await database!.InsertOrReplaceAsync(minifig.Hipwear);
            }
            
            if (minifig.Id == 0)
                return await database!.InsertAsync(minifig);
            
            return await database!.UpdateAsync(minifig);
        }

        public async Task<int> DeleteMinifigAsync(Minifig minifig) {
            await Init();
            return await database!.DeleteAsync(minifig);
        }
    }
}
