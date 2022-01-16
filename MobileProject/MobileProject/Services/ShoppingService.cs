﻿using System;
using System.Collections.Generic;
using MobileProject.Models;
using Xamarin.Forms;

namespace MobileProject.Services
{
    public class ShoppingService
    {
        static ShoppingService _instance;
        ColorTypeConverter converter = new ColorTypeConverter();
        DataBase db = new DataBase();
        

        public static ShoppingService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ShoppingService();
                return _instance;
            }
        }

        public List<ShoppingItem> GetShoppingList()
        {
            List<ShoppingItem> shoppingItems = new List<ShoppingItem>();
            
            List<ReminderDB> reminder = db.GetReminder();
            List<ReminderItemDB> reminderitem = db.GetReminderItem();
            reminder.ForEach(re =>
            {
                List<ShoppingDetailItem> Items = new List<ShoppingDetailItem>();

                reminderitem.ForEach(reitem =>
                {


                    if (reitem.ReminderId == re.Id) {
                        Console.WriteLine("reminder:" + reitem.ReminderId);
                        Console.WriteLine("reminderitem:" + re.Id);
                        
                        Items.Add(
                        new ShoppingDetailItem {Id=reitem.Id.ToString(), Name = reitem.Name,Done=reitem.IsDone,IsLatest=false,IsDoneImg=reitem.IsDoneImg }
                    );
                    }
                });
                Console.WriteLine("asw:" + reminderitem.Count);
                Items[Items.Count-1].IsLatest=true;
                shoppingItems.Add(new ShoppingItem
            {
                Id=re.Id,
                Name = re.Name,
                Icon = re.Icon,
                Color = (Color)(converter.ConvertFromInvariantString(re.Color)),
                Items = Items,
                });
            });

            return shoppingItems;
    }
    }
}