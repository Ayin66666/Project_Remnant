using Item;
using UnityEngine;


[CreateAssetMenu(fileName = "Exp Ticket", menuName = "Item/Material/ExpTicket", order = int.MaxValue)]
public class ExpTicketSO : ItemSO
{
    [Header("---Ticket Setting---")]
    [SerializeField] private int expValue;
    public int ExpValue => expValue;
}
