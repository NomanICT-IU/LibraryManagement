namespace LibraryManegment.Api.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string MemberId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Status { get; set; }
    }
}
