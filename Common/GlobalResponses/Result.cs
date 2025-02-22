namespace Common.GlobalResponses
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public Result(List<string> erors) 
        {
            Errors = erors;
            IsSuccess = false;
        }

        public Result() 
        {
            Errors = [];
            IsSuccess = true;
        }
    }
}
