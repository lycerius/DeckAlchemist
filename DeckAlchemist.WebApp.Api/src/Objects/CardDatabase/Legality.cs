namespace DeckAlchemist.WebApp.Api.Objects.CardDatabase {
    public class Legality : ILegality {
        public string Format { get; set; }
        public string Status { get; set; }

        public bool Equals(ILegality other)
        {
            return Legality.IsEqual(this, other);
        }

        public override bool Equals(object obj){
            if(obj is ILegality)
            {
                return Equals(obj as ILegality);
            }
            else return base.Equals(obj);
        }
        public static bool IsEqual (Legality l1, ILegality l2) {
            if(l2 == null) return false;
            return l1.Format == l2.Format && l1.Status == l2.Status;
        }
    }
}