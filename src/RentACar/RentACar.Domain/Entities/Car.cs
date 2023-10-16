using Core.Persistence.Repositories;
using RentACar.Domain.Enums;

namespace RentACar.Domain.Entities;

public class Car : Entity<Guid>
{
    public Guid ModelId { get; set; }
    public int Km { get; set; }
    public short Year { get; set; }
    public string Plate { get; set; }
    public short MinFindexScore { get; set; }
    public CarState CarState { get; set; }

    public virtual Model Model { get; set; }

    public Car()
    {
    }

    public Car(Guid id, Guid modelId, int km, short year, string plate, short minFindexScore, CarState carState) : this()
    {
        Id = id;
        ModelId = modelId;
        Km = km;
        Year = year;
        Plate = plate;
        MinFindexScore = minFindexScore;
        CarState = carState;
    }
}