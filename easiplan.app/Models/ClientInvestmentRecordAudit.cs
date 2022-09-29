using Finx.App.Enums;

namespace Finx.App.Models
{
    
    public sealed class ClientInvestmentRecordImportAudit
    {
        public ClientInvestmentRecordImportStatus ClientInvestmentRecordImportStatus {get; private set;}
        public string Message { get; private set; }
        public int? PercentageCompleted { get; private set; }
        public void SetImportStatus(ClientInvestmentRecordImportStatus clientInvestmentRecordImportStatus)
        {
            this.ClientInvestmentRecordImportStatus = clientInvestmentRecordImportStatus;
        }
        public void SetMessage(string Message)
        {
            this.Message = Message;
        }
        public void SetPercentageCompleted(int? PercentageCompleted)
        {
            this.PercentageCompleted = PercentageCompleted;
        }
    }
}