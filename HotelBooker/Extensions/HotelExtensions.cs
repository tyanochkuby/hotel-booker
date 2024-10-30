using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Extensions
{
    public static class HotelExtensions
    {
        public static Dictionary<string, int> ToRoomCountDictionary(this Hotel hotel)
        {
            return hotel.Rooms
                    .GroupBy(room => room.RoomType)
                    .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
