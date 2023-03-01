using Finx.App.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easiplan.domain.Entities;

namespace Finx.App.Enums
{
    //public enum InstructionStatus
    //{

    //    Pending,
    //    AddPending,
    //    UpdatePending,

    //    InProgress,
    //    AddInProgress,
    //    UpdateInProgress,

    //    Submitted,
    //    AddSubmitted,
    //    UpdateSubmitted,

    //    ReadyForAuth,
    //    AddReadyForAuth,
    //    UpdateReadyForAuth,

    //    Authorised,
    //    AddAuthorised,
    //    UpdateAuthorised,

    //    Completed,
    //    AddCompleted,
    //    UpdateCompleted,

    //    Cancelled,
    //    AddCancelled,
    //    UpdateCancelled,

    //}
    public static class InstructionStatusExt
    {
        public static string ToText(this InstructionStatus instructionStatus)
        {
            return instructionStatus.ToString();
        }
        public static Color ToColour(this InstructionStatus instructionStatus)
        {

            switch (instructionStatus)
            {
                case InstructionStatus.Pending: return Color.Red;
                case InstructionStatus.AddPending: return Color.Red;
                case InstructionStatus.UpdatePending: return Color.Red;
                case InstructionStatus.InProgress: return Color.Orange;
                case InstructionStatus.AddInProgress: return Color.Orange;
                case InstructionStatus.UpdateInProgress: return Color.Orange;
                case InstructionStatus.Submitted: return Color.LightGreen;
                case InstructionStatus.AddSubmitted: return Color.LightGreen;
                case InstructionStatus.UpdateSubmitted: return Color.LightGreen;
                case InstructionStatus.ReadyForAuth: return Color.SlateBlue;
                case InstructionStatus.AddReadyForAuth: return Color.SlateBlue;
                case InstructionStatus.UpdateReadyForAuth: return Color.SlateBlue;
                case InstructionStatus.Authorised: return Color.Blue;
                case InstructionStatus.AddAuthorised: return Color.Blue;
                case InstructionStatus.UpdateAuthorised: return Color.Blue;
                case InstructionStatus.Completed: return Color.Green;
                case InstructionStatus.AddCompleted: return Color.Green;
                case InstructionStatus.UpdateCompleted: return Color.Green;
                case InstructionStatus.Cancelled: return Color.Gray;
                case InstructionStatus.AddCancelled: return Color.Gray;
                case InstructionStatus.UpdateCancelled: return Color.Gray;

                default: return Color.Gray;

            }
        }

        public static IDictionary<string, Color> ColourScheme()
        {
            IDictionary<string, Color> colorScheme = new Dictionary<string, Color>();
            foreach (InstructionStatus e in Enum.GetValues(typeof(InstructionStatus)))
                colorScheme.Add(e.ToString(), e.ToColour());

            return colorScheme;
        }

        public static List<ListDataItem> ToListDataItem()
        {
            List<ListDataItem> list = new List<ListDataItem>();
            foreach (InstructionType e in Enum.GetValues(typeof(InstructionStatus)))
                list.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, e.ToString(), e.ToText()));

            return list;
        }
    }

    public static class InstructionDaysExt
    {
        public static string ToText(int days)
        {
            return days.ToString();
        }
        public static Color ToColour(int? days)
        {
            if (days == null)
                return Color.Transparent;

            if (days >= 0 && days <= 21)
                return Color.Green;

            if (days > 21 && days <= 33)
                return Color.Orange;

            if (days > 33)
                return Color.Red;

            return Color.Transparent;
        }

    }
    //public enum InstructionType
    //{
    //    POLICY_CREATE,
    //    POLICY_UPDATE,
    //    POLICY_REVIEW,
    //    POLICY_CANCEL,
    //    RETIRE_POLICY_NOTE,
    //    INVEST_POLICY_NOTE,
    //    MEDICAL_POLICY_NOTE,
    //    LIFE_POLICY_NOTE,
    //    ASSET_POLICY_NOTE,
    //    EDU_POLICY_NOTE,
    //    OTHER
    //}
    public static class InstructionTypeExt
    {
        public static string ToText(this InstructionType instructionType)
        {
            switch (instructionType)
            {
                case InstructionType.POLICY_CREATE: return "Create Policy";
                case InstructionType.POLICY_UPDATE: return "Update Policy";
                case InstructionType.POLICY_REVIEW: return "Review Policy";
                case InstructionType.POLICY_CANCEL: return "Cancel Policy";
                case InstructionType.RETIRE_POLICY_NOTE: return "Retirement Note";
                case InstructionType.LIFE_POLICY_NOTE: return "Life Note";
                case InstructionType.INVEST_POLICY_NOTE: return "Investment Note";
                case InstructionType.MEDICAL_POLICY_NOTE: return "Medical Note";
                case InstructionType.ASSET_POLICY_NOTE: return "Asset Note";
                case InstructionType.EDU_POLICY_NOTE: return "Education Note";
                case InstructionType.OTHER: return "Other";
                default:
                    return "Custom Task";
            }

        }

        public static InstructionType ToType(string type)
        {
            switch (type)
            {
                case "Create Policy":
                    return InstructionType.POLICY_CREATE;
                case "Update Policy":
                    return InstructionType.POLICY_UPDATE;
                case "Review Policy":
                    return InstructionType.POLICY_REVIEW;
                case "Cancel Policy":
                    return InstructionType.POLICY_CANCEL;
                case "Retirement Note":
                    return InstructionType.RETIRE_POLICY_NOTE;
                case "Life Note":
                    return InstructionType.LIFE_POLICY_NOTE;
                case "Investment Note":
                    return InstructionType.INVEST_POLICY_NOTE;
                case "Medical Note":
                    return InstructionType.MEDICAL_POLICY_NOTE;
                case "Asset Note":
                    return InstructionType.ASSET_POLICY_NOTE;
                case "Education Note":
                    return InstructionType.EDU_POLICY_NOTE;
                case "Other":
                    return InstructionType.OTHER;
                default:
                    return InstructionType.UNKNOWN;
            }

        }

        public static List<ListDataItem> ToListDataItem()
        {
            List<ListDataItem> list = new List<ListDataItem>();
            foreach (InstructionType e in Enum.GetValues(typeof(InstructionType)))
                list.Add(new ListDataItem(ListDataItemType.ClientInstructionType, e.ToString(), e.ToText()));

            return list;
        }
    }

    public enum NeedStatus
    {
        Unknown,
        Advised,
        Accepted,
        InProgress,
        Submitted,
        ReadyForAuth,
        Authorised,
        Completed,
        Cancelled,
        Implemented,
        Pending,

    }
    public static class NeedStatusExt
    {
        public static string ToText(this NeedStatus needStatus)
        {

            return needStatus.ToString();
        }
        public static NeedStatus ToNeedStatus(this string needStatus)
        {
            NeedStatus r = NeedStatus.Unknown;
            Enum.TryParse<NeedStatus>(needStatus, out r);

            return r;
        }
        public static T ToEnum<T>(this string value) where T : struct
        {
            T r;
            Enum.TryParse(value, out r);

            return r;
        }
        public static List<ListDataItem> ToListDataItem()
        {
            List<ListDataItem> list = new List<ListDataItem>();
            foreach (InstructionType e in Enum.GetValues(typeof(NeedStatus)))
                list.Add(new ListDataItem(ListDataItemType.NeedStatus, e.ToString(), e.ToText()));

            return list;
        }


    }

    public enum PolicyAction
    {
        ViewFunds,
        UpdateFna,
        ViewPolicy,
        AmendPolicy,
        RemovePolicy,
        UpdateInstruction,
        PolicyHistory,
        UpdateRiskFna,
        Unknown
    }
    public static class PolicyActionExt
    {
        public static string ToDescription(this PolicyAction policyAction)
        {

            switch (policyAction)
            {
                case PolicyAction.AmendPolicy:
                    return "Amend Policy";
                case PolicyAction.ViewFunds:
                    return "View Policy";
                case PolicyAction.ViewPolicy:
                    return "Update Policy";
                case PolicyAction.UpdateFna:
                    return "Add Policy Funds";
                case PolicyAction.RemovePolicy:
                    return "Remove Policy";
                case PolicyAction.UpdateInstruction:
                    return "Admin Task";
                case PolicyAction.PolicyHistory:
                    return "Policy History";
                case PolicyAction.UpdateRiskFna:
                    return "Add Risk Benefits";
                default:
                    return "Unknown Policy Action";
            }
        }

        public static InstructionType ToInstructionType(this PolicyAction policyAction)
        {
            switch (policyAction)
            {
                case PolicyAction.AmendPolicy:
                    return InstructionType.POLICY_UPDATE;// "Amend Policy";
                case PolicyAction.ViewFunds:
                    return InstructionType.POLICY_REVIEW; //"View Policy";
                case PolicyAction.ViewPolicy:
                    return InstructionType.POLICY_REVIEW;// "Update Policy";
                case PolicyAction.UpdateFna:
                    return InstructionType.POLICY_CREATE;// "Add Policy Funds";
                case PolicyAction.RemovePolicy:
                    return InstructionType.POLICY_CANCEL;// "Remove Policy";
                case PolicyAction.UpdateInstruction:
                    return InstructionType.POLICY_UPDATE;// "Admin Task";
                case PolicyAction.PolicyHistory:
                    return InstructionType.OTHER;// "Policy History";
                case PolicyAction.UpdateRiskFna:
                    return InstructionType.POLICY_CREATE;// "Add Policy Funds";
                default:
                    return InstructionType.UNKNOWN;// "Unknown Policy Action";
            }
        }
    }

    public enum ContextMenuType
    {
        RetirementPortfolio,
        InvestmentPortfolio,
        LifePortfolio,
        EducationPortfolio,
        MedicalPortfolio,
        AssetPortfolio,
        RetirementFna,
        EducationFna,
        InvestmentFna,
        ClientInstruction,
        MeetingSync,
        AdminTask,
        ClientTask,
        ClientComms,
        RiskCoverFna

    }

    public enum ClientStatus
    {
        Unknown,
        Active,
        InActive,
        InProgress


    }

    public enum CommunicationAction
    {
        SendEmail,
        SendSMS,
        SendAll
        
    }

    public enum VersionType
    {
        Unlicensed,
        Trial,
        Standard,
        Premium,
        Basic
    }

    public enum ClientInvestmentRecordImportStatus
    {   
        Pending,
        Imported,
        ImportedWithError,
        Error,
        Cancelled
    }

    public enum FileFormat
    { 
    Csv,
    Xls,
    Xlsx,
    Txt
    }
}
