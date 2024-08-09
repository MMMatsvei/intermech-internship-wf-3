using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace wf_3
{
    [Serializable]
    public class CalendarEvent
    {
        private string name;
        private DateTime dateTime;
        private string category;
        private Color color;

        public CalendarEvent() { }

        public CalendarEvent(string name, DateTime dateTime, string category)
        {
            this.name = name;
            this.dateTime = dateTime;
            this.category = category;
            this.color = ConvertColor(category);
        }

        public CalendarEvent(string name, DateTime dateTime, Color color)
        {
            this.name = name;
            this.dateTime = dateTime;
            this.color = color;
            this.category = ConvertColor(color);
        }

        private static readonly Dictionary<string, Color> categoryColors = new Dictionary<string, Color>
        {
            { "Встреча", Color.LightSteelBlue },   
            { "Дедлайн", Color.LightCoral },
            { "Праздник", Color.LightGoldenrodYellow },  
            { "Мероприятие", Color.LightGreen },
            { "Тренировка", Color.LightSkyBlue },  
            { "Задача", Color.LightGray }
        };

        public static Color ConvertColor(string category)
        {
            return categoryColors.TryGetValue(category, out var color) ? color : Color.White;
        }

        public static string ConvertColor(Color color)
        {
            var category = categoryColors.FirstOrDefault(pair => pair.Value == color).Key;
            return category ?? "Встреча";
        }

        public Color getColor()
        {
            return color;
        }

        public string getName()
        {
            return string.IsNullOrEmpty(name) ? "Без имени" : name;
        }

        public DateTime getDateTime()
        {
            return dateTime;
        }

        public void setDateTime(DateTime dt)
        {
            dateTime = dt;
        }
    }

}
