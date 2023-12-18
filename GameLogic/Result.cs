namespace GameLogic
{
    public class Result
    {
        public Player Winner { get; }
        public EndReason Reason { get; }

        public Result(Player winner, EndReason reason)
        {
        }

        public static Result Win(Player winner)
        {
        }

        public static Result Draw(EndReason reason)
        {
        }
    }
}
