using WEB.Entity;

namespace WEB.Models
{
    public class NewCar
    {
        public List<Fuel> Fuels { get; set; }
        public List<CarType> CarType { get; set; }
        public List<Transmission> Transmission { get; set; }
        public List<Reservation> Reservation { get; set; }
        public List<Drive_type> Drive_type{ get; set; }
        public List<BodyType> BodyType { get; set; }
        public short car_id { get; set; }
        public string car_name { get; set; }
        public byte type_id { get; set; }
        public byte body_id { get; set; }
        public byte transmission_id { get; set; }
        public byte num_seats { get; set; }
        public byte fuel_id { get; set; }
        public byte drive_id { get; set; }
        public short price { get; set; }
        public string imagepath { get; set; }

    }
}
