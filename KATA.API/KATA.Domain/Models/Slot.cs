namespace KATA.Domain.Models;

public class Slot
{
    public int StartSlot { get; set; }
    public int EndSlot { get; set; }
    public Slot(int startSlot, int endSlot)
    {
        StartSlot = startSlot;
        EndSlot = endSlot;
    }
}
