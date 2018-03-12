using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Collection
{
    /*
     * { "_id" : ObjectId("5a74a02781e696500fbbb1fe"), "username" : "<Username>", "userID" : "<stuff here>", 
     * "OwnedCards" : 
     * [ 
     *  { "cardid" : 
     *      { "amountTotal" : 4.0, 
     *          "amountAvailable" : 1.0, 
     *          "InDecks" : [ 
     *              { "<DeckID>" : 1.0 }, 
     *              { "<DeckID>" : 2.0 } 
     *          ], 
     *          "lentTo" : [
     *              {"<Users1>" : 1.0}, 
     *              {"<Users2>" : 2.0}
     *           ]
     *              
     *     } 
     *  }
     * ], "BorrowedCards" : [ 
     *      { 
     *          "cardid" : [ 
     *              { "Total" : 2.0 }, { "From" : "User1", "Amount" : 1.0 }, { "From" : "User2", "Amount" : 1.0 }, { "InDecks" : [ { "<DeckID>" : 1.0 }, { "<DeckID>" : 2.0 } ] } ] } ] }
     */
    public interface ICollection
    {
        string UserId { get; set; }
        List<IOwnedCard> OwnedCards { get; set; }
        List<IBorrowedCard> BorrowedCards { get; set; }
    }
}
