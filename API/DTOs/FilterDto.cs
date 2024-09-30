using API.Types;

namespace API.DTOs
{
    public class FilterDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 30;

        public string UserSearch { get; set; }

        public TimePeriod Period { get; set; }

        public string TechIds { get; set; }

        public int[] GetIds()
        {
            if (String.IsNullOrEmpty(TechIds))
                return [];

            return TechIds.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(i => int.Parse(i)).ToArray();
        }
    }
}