namespace Atlas.Models
{
    // This mirrors the existing ErrorViewModel (which uses a different namespace)
    // and provides the type under Atlas.Models so existing views and controllers
    // that expect Atlas.Models.ErrorViewModel compile without changing controllers/views.
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
