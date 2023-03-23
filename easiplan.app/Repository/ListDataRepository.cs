using DocumentFormat.OpenXml.Drawing.Charts;
using easiplan.domain;
using Microsoft.Graph;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Finx.App
{
    /// <summary>
    /// A list repository
    /// </summary>
    public class ListDataRepository
    {
        public virtual IList<ListDataItem> Items { get; set; }

        public ListDataRepository()
        {
            Items = new List<ListDataItem>();

            Items.Add(new ListDataItem(ListDataItemType.Titles, "Mr", "Mr"));
            Items.Add(new ListDataItem(ListDataItemType.Titles, "Mrs", "Mrs"));
            Items.Add(new ListDataItem(ListDataItemType.Titles, "Ms", "Ms"));
            Items.Add(new ListDataItem(ListDataItemType.Titles, "Mss", "Mss"));
            Items.Add(new ListDataItem(ListDataItemType.Titles, "Dr", "Dr"));

            Items.Add(new ListDataItem(ListDataItemType.Gender, "Male", "Male"));
            Items.Add(new ListDataItem(ListDataItemType.Gender, "Female", "Female"));

            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Unmarried", "Unmarried"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Married COP", "Married COP"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Married ANC", "Married ANC"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Married Islamic", "Married Islamic"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Co-habiting", "Co-habiting"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Divorced", "Divorced"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalStatus, "Widower", "Widower"));

            Items.Add(new ListDataItem(ListDataItemType.ContactTypes, "HomeTelephone", "Home Telephone"));
            Items.Add(new ListDataItem(ListDataItemType.ContactTypes, "WorkTelephone", "Work Telephone"));
            Items.Add(new ListDataItem(ListDataItemType.ContactTypes, "Cellphone", "Cellphone"));
            Items.Add(new ListDataItem(ListDataItemType.ContactTypes, "Email", "Email"));
            Items.Add(new ListDataItem(ListDataItemType.ContactTypes, "Fax", "Fax"));
            Items.Add(new ListDataItem(ListDataItemType.ContactTypes, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Income", "Income"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Non-Income", "Non-Income"));
            //Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Investments", "Investments"));
            //Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Retirement", "Retirement"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Depreciating", "Depreciating"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Appreciating", "Appreciating"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Fixed", "Fixed"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Variable", "Variable"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Cash", "Cash"));
            Items.Add(new ListDataItem(ListDataItemType.AssetTypes, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Property", "Property"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Vehicles", "Vehicles"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Jewellery", "Jewellery"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Household Goods", "Household Goods"));
            //Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Unit Trusts", "Unit Trusts"));
            //Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Pension/Provident Funds", "Pension/Provident Funds"));
            //Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Retirement Annuities", "Retirement Annuities"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Cash", "Cash"));
            //Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Endowment/Life Policies", "Endowment/Life Policies"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Shares", "Shares"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Debtors", "Debtors"));
            Items.Add(new ListDataItem(ListDataItemType.AssetClass, "Other", "Other"));

            //Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Property", "Property"));
            //Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Jewellery", "Jewellery"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Unit Trusts", "Unit Trusts"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Pension/Provident Funds", "Pension/Provident Funds"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Pension/Preservation Funds", "Pension/Preservation Funds"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Provident/Preservation Funds", "Provident/Preservation Funds"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Retirement Annuities", "Retirement Annuities"));

            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Endowment", "Endowment"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Offshore Endowment", "Offshore Endowment"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Flexible Investment", "Flexible Investment"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Living Annuity", "Living Annuity"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Life Annuity", "Life Annuity"));

            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Tax Free", "Tax Free"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Gold", "Gold"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Profit Share", "Profit Share"));
            //Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Buy & Sell", "Buy & Sell"));
            //.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Keyman", "Keyman"));
            //Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Contingent Liability", "Contingent Liability"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementAssetClass, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "CASH", "CASH"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "SAVINGS/EMERGENCY", "SAVINGS/EMERGENCY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "SHARES", "SHARES"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "STOCKS", "STOCKS"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "UNIT TRUSTS", "UNIT TRUSTS"));
            // Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "EDUCATION", "EDUCATION"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "HOLIDAY", "HOLIDAY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "PURCHASES", "PURCHASES"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "PROPERTY", "PROPERTY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "JEWELLERY", "JEWELLERY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "FLEXIBLE INVESTMENT", "FLEXIBLE INVESTMENT"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "ENDOWMENT", "ENDOWMENT"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "TAX FREE", "TAX FREE"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentClass, "OTHER", "OTHER"));

            Items.Add(new ListDataItem(ListDataItemType.RetirementWants, "Income", "Income"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementWants, "Holiday", "Holiday"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementWants, "Charity", "Charity"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementWants, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.RetirementWantsClass, "Monthly", "Monthly"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementWantsClass, "Annually", "Annually"));
            Items.Add(new ListDataItem(ListDataItemType.RetirementWantsClass, "LumpSum", "LumpSum"));

            Items.Add(new ListDataItem(ListDataItemType.LiabilityTypes, "Fixed", "Fixed"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityTypes, "Variable", "Variable"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityTypes, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Bond", "Bond"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Loan", "Loan"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Investment", "Investment"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Credit Card", "Credit Card"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Creditors", "Creditors"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Overdraft", "Overdraft"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Hire Purchase", "Hire Purchase"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Overdue Taxes", "Overdue Taxes"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Accounts", "Accounts"));
            Items.Add(new ListDataItem(ListDataItemType.LiabilityClass, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.DBTypes, "MySQL", "MySQL"));
            //Items.Add(new ListDataItem(ListDataItemType.DBTypes, "SQLServer","MS SQL Server"));
            //Items.Add(new ListDataItem(ListDataItemType.DBTypes, "Oracle", "Oracle 11g"));

            Items.Add(new ListDataItem(ListDataItemType.BnkAcctTypes, "Current", "Current Account"));
            Items.Add(new ListDataItem(ListDataItemType.BnkAcctTypes, "Saving", "Savings Account"));
            Items.Add(new ListDataItem(ListDataItemType.BnkAcctTypes, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Gross Salary", "Gross Salary"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Salary", "Salary"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Rental", "Rental"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Dividends", "Dividends"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Annuity", "Annuity"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Royalties", "Royalties"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Business", "Business"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeTypes, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.IncomeClass, "Monthly", "Monthly"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeClass, "Annually", "Annually"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeClass, "BiAnnually", "BiAnnually"));
            Items.Add(new ListDataItem(ListDataItemType.IncomeClass, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Rent/Bond Repayments", "Rent/Bond Repayments"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Children Expenses", "Children Expenses"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Credit Card", "Credit Card"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Entertainment", "Entertainment"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Food/Groceries", "Food/Groceries"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Gifts/Donations", "Gifts/Donations"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Gym Fees", "Gym Fees"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Household Insurance", "Household Insurance"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Housing Expenses", "Housing Expenses"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Legal Expenses", "Legal Expenses"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Loan Repayments", "Loan Repayments"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Medical Aid", "Medical Aid"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Outstanding Taxes", "Outstanding Taxes"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "PAYE Tax", "PAYE Tax"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Personal Expenses", "Personal Expenses"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Personal Insurance", "Personal Insurance"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Pets", "Pets"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Rates & Taxes", "Rates & Taxes"));
            //Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Savings/Investments", "Savings/Investments"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "School Fees", "School Fees"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Transport Expenses", "Transport Expenses"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Water & Electricity", "Water & Electricity"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Zakaat", "Zakaat"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseTypes, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.ExpenseClass, "Monthly", "Monthly"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseClass, "Annually", "Annually"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseClass, "BiAnnually", "BiAnnually"));
            Items.Add(new ListDataItem(ListDataItemType.ExpenseClass, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.ExpensePerc, 100D, "100%"));
            Items.Add(new ListDataItem(ListDataItemType.ExpensePerc, 75D, "75%"));
            Items.Add(new ListDataItem(ListDataItemType.ExpensePerc, 50D, "50%"));
            Items.Add(new ListDataItem(ListDataItemType.ExpensePerc, 25D, "25%"));


            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "SAVINGS/EMERGENCY", "SAVINGS/EMERGENCY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "INVESTMENT", "INVESTMENT"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "RETIREMENT ANNUITY", "RETIREMENT ANNUITY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "PENSION/PROVIDENT", "PENSION/PROVIDENT"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "ENDOWMENT", "ENDOWMENT"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "RISK", "RISK"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "MEDICAL", "MEDICAL"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "PR0PERTY", "PR0PERTY"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentType, "OTHER", "OTHER"));



            //Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "POLICY_CREATE", "Take out an Investment"));
            //Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "POLICY_UPDATE", "Update an Investment"));
            //Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "POLICY_REVIEW", "Review an Investment"));
            //Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "POLICY_CANCEL", "Cancel an Investment"));
            //Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "OTHER", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "", "Any"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "Pending", "Pending"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "AddPending", "AddPending"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "UpdatePending", "UpdatePending"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "InProgress", "InProgress"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "AddInProgress", "AddInProgress"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "UpdateInProgress", "UpdateInProgress"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "AddReadyForAuth", "AddReadyForAuth"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "UpdateReadyForAuth", "UpdateReadyForAuth"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "Authorised", "Authorised"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "AddAuthorised", "AddAuthorised"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "UpdateAuthorised", "UpdateAuthorised"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "Completed", "Completed"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "AddCompleted", "AddCompleted"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "UpdateCompleted", "UpdateCompleted"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "Cancelled", "Cancelled"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "AddCancelled", "AddCancelled"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionStatus, "UpdateCancelled", "UpdateCancelled"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionView, "", ""));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionView, "Authoriser", "Authoriser"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionView, "Admin", "Admin"));

            // Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Pending", "Pending"));
            //Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Advised", "Advised"));
            //Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Accepted", "Accepted"));
            Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "InProgress", "InProgress"));
            Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Submitted", "Submitted"));
            //Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "ReadyForAuth", "Ready For Authorisation"));
            //Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Authorised", "Authorised"));
            // Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Completed", "Completed"));
            // Items.Add(new ListDataItem(ListDataItemType.NeedStatus, "Cancelled", "Cancelled"));

            // Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Pending", "Pending"));
            //Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Advised", "Advised"));
            //Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Accepted", "Accepted"));
            Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "InProgress", "InProgress"));
            Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Submitted", "Submitted"));
            //Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "ReadyForAuth", "Ready For Authorisation"));
            //Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Authorised", "Authorised"));
            //Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Completed", "Completed"));
            //Items.Add(new ListDataItem(ListDataItemType.InstructionStatus, "Cancelled", "Cancelled"));


            // Items.Add(new ListDataItem(ListDataItemType.UserDesignation, "Administrator", "Administrator"));
            Items.Add(new ListDataItem(ListDataItemType.UserDesignation, "Advisor", "Advisor"));
            Items.Add(new ListDataItem(ListDataItemType.UserDesignation, "Clerk", "Clerk"));
            Items.Add(new ListDataItem(ListDataItemType.UserDesignation, "Authoriser", "Authoriser"));
            Items.Add(new ListDataItem(ListDataItemType.UserDesignation, "Guest", "Guest"));

            Items.Add(new ListDataItem(ListDataItemType.RiskProfileStatus, "Unknown", "Unknown"));
            Items.Add(new ListDataItem(ListDataItemType.RiskProfileStatus, "Conservative", "Conservative"));
            Items.Add(new ListDataItem(ListDataItemType.RiskProfileStatus, "Moderate", "Moderate"));
            Items.Add(new ListDataItem(ListDataItemType.RiskProfileStatus, "Aggressive", "Aggressive"));

            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Spouse", "Spouse"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Son", "Son"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Daughter", "Daughter"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Father", "Father"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Mother", "Mother"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Brother", "Brother"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Sister", "Sister"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "AdoptedSon", "Son Adopted"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "AdoptedDaughter", "Daughter Adopted"));
            Items.Add(new ListDataItem(ListDataItemType.DependentType, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.FundRiskCat, "High", "High"));
            Items.Add(new ListDataItem(ListDataItemType.FundRiskCat, "Moderate", "Moderate"));
            Items.Add(new ListDataItem(ListDataItemType.FundRiskCat, "Low", "Low"));

            Items.Add(new ListDataItem(ListDataItemType.ClientStatus, "New", "New"));
            Items.Add(new ListDataItem(ListDataItemType.ClientStatus, "Incomplete", "Incomplete"));
            Items.Add(new ListDataItem(ListDataItemType.ClientStatus, "Complete", "Complete"));
            Items.Add(new ListDataItem(ListDataItemType.ClientStatus, "Suspended", "Suspended"));

            Items.Add(new ListDataItem(ListDataItemType.MeetingStatus, "Scheduled", "Scheduled"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingStatus, "Accepted", "Accepted"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingStatus, "Declined", "Declined"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingStatus, "Cancelled", "Cancelled"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingStatus, "Postponed", "Postponed"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingStatus, "Expired", "Expired"));

            Items.Add(new ListDataItem(ListDataItemType.Banks, "Absa", "Absa"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Albaraka", "Albaraka"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Bidvest", "Bidvest"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Capitec", "Capitec"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Discovery Bank", "Discovery Bank"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "FNB", "FNB"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Investec", "Investec"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Mercantile", "Mercantile"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Nedbank", "Nedbank"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Standard Bank", "Standard Bank"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "TymeBank", "TymeBank"));
            Items.Add(new ListDataItem(ListDataItemType.Banks, "Bank Zero", "Bank Zero"));



            Items.Add(new ListDataItem(ListDataItemType.InvestmentStatus, "Pending", "Pending"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentStatus, "InProcess", "InProcess"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentStatus, "Active", "Active"));
            Items.Add(new ListDataItem(ListDataItemType.InvestmentStatus, "InActive", "InActive"));

            Items.Add(new ListDataItem(ListDataItemType.Language, "English", "English"));
            Items.Add(new ListDataItem(ListDataItemType.Language, "Afrikaans", "Afrikaans"));

            Items.Add(new ListDataItem(ListDataItemType.MedicalBenefit, "Day to Day", "Day to Day"));
            Items.Add(new ListDataItem(ListDataItemType.MedicalBenefit, "Hospital", "Hospital"));
            Items.Add(new ListDataItem(ListDataItemType.MedicalBenefit, "Chronic", "Chronic"));
            Items.Add(new ListDataItem(ListDataItemType.MedicalBenefit, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Life", "Life"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Dreaded Disease", "Dreaded Disease"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Disability", "Disability"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Income Protection", "Income Protection"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Keyman", "Keyman"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Contingent Liability", "Contingent Liability"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Buy And Sell", "Buy And Sell"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "GLA", "GLA"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Sickness", "Sickness"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Permanent Incapacity", "Permanent Incapacity"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Admissions Rider Benefit", "Admissions Rider Benefit"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Pregnancy Complications", "Pregnancy Complications"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Critical Illness", "Critical Illness"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Functional Impairment", "Functional Impairment"));
            Items.Add(new ListDataItem(ListDataItemType.LifeBenefit, "Other", "Other"));

            

            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "Implemented", "Implemented"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "Cancelled", "Cancelled"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "AddPending", "AddPending"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "UpdatePending", "UpdatePending"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "AddInProgress", "AddInProgress"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "UpdateInProgress", "UpdateInProgress"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "AddReadyForAuth", "AddReadyForAuth"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "UpdateReadyForAuth", "UpdateReadyForAuth"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "AddAuthorised", "AddAuthorised"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "UpdateAuthorised", "UpdateAuthorised"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "AddCompleted", "AddCompleted"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "UpdateCompleted", "UpdateCompleted"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "AddCancelled", "AddCancelled"));
            Items.Add(new ListDataItem(ListDataItemType.PolicyStatus, "UpdateCancelled", "UpdateCancelled"));

            Items.Add(new ListDataItem(ListDataItemType.MeetingType, "Complete Financial Plan", "Complete Financial Plan"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingType, "Review Meeting", "Review Meeting"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingType, "Discuss Will", "Discuss Will"));
            Items.Add(new ListDataItem(ListDataItemType.MeetingType, " ", " "));

            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "1", "Jan"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "2", "Feb"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "3", "Mar"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "4", "Apr"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "5", "May"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "6", "Jun"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "7", "Jul"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "8", "Aug"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "9", "Sept"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "10", "Oct"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "11", "Nov"));
            Items.Add(new ListDataItem(ListDataItemType.MonthsOfYear, "12", "Dec"));

            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "", ""));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "1", "1"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "2", "2"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "3", "3"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "4", "4"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "5", "5"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "6", "6"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "7", "7"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "8", "8"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "9", "9"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "10", "10"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "11", "11"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "12", "12"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "13", "13"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "14", "14"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "15", "15"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "16", "16"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "17", "17"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "18", "18"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "19", "19"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "20", "20"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "21", "21"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "22", "22"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "23", "23"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "24", "24"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "25", "25"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "26", "26"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "27", "27"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "28", "28"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "29", "29"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "30", "30"));
            Items.Add(new ListDataItem(ListDataItemType.DaysOfMonth, "31", "31"));


            Items.Add(new ListDataItem(ListDataItemType.ClientSegment, "", ""));
            Items.Add(new ListDataItem(ListDataItemType.ClientSegment, "A", "A"));
            Items.Add(new ListDataItem(ListDataItemType.ClientSegment, "B", "B"));
            Items.Add(new ListDataItem(ListDataItemType.ClientSegment, "C", "C"));

            Items.Add(new ListDataItem(ListDataItemType.CommsType, "Email", "Email"));
            Items.Add(new ListDataItem(ListDataItemType.CommsType, "SMS", "SMS"));
            Items.Add(new ListDataItem(ListDataItemType.CommsType, "Call", "Call"));
            Items.Add(new ListDataItem(ListDataItemType.CommsType, "WalkIn", "WalkIn"));

            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "", ""));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Create Policy", "Create Policy"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Update Policy", "Update Policy"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Review Policy", "Review Policy"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Cancel Policy", "Cancel Policy"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Retirement Note", "Retirement Note"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Life Note", "Life Note"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Investment Note", "Investment Note"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Medical Note", "Medical Note"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Asset Note", "Asset Note"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Education Note", "Education Note"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Contact Client", "Contact Client"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Schedule Meeting", "Schedule Meeting"));
            Items.Add(new ListDataItem(ListDataItemType.ClientInstructionType, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Contact Client", "Contact Client"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Schedule Meeting", "Schedule Meeting"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Send SMS", "Send SMS"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Send Email", "Send Email"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Death Claim", "Death Claim"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Risk Claim", "Risk Claim"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Medical Aid", "Medical Aid"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Gap Cover", "Gap Cover"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Employee Benefits", "Employee Benefits"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Trust Formation", "Trust Formation"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Full Surrender", "Full Surrender"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Amendments", "Amendments"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Quotes", "Quotes"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Risk Quotes", "Risk Quotes"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Portfolio Schedule", "Portfolio Schedule"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskType, "Other", "Other"));

            



            Items.Add(new ListDataItem(ListDataItemType.CustomTaskStatus, "Pending", "Pending"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskStatus, "InProgress", "InProgress"));
            Items.Add(new ListDataItem(ListDataItemType.CustomTaskStatus, "Completed", "Completed"));

            Items.Add(new ListDataItem(ListDataItemType.MaritalRegimes, 1, "Out of COP"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalRegimes, 2, "In COP"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalRegimes, 3, "In COP Non-Accrual"));
            Items.Add(new ListDataItem(ListDataItemType.MaritalRegimes, 4, "Islamic Shariah"));

            Items.Add(new ListDataItem(ListDataItemType.OtherDeductionTypes, "Donations To PBO", "Donations To PBO"));
            Items.Add(new ListDataItem(ListDataItemType.OtherDeductionTypes, "Bequests To Trust", "Bequests To Trust"));
            Items.Add(new ListDataItem(ListDataItemType.OtherDeductionTypes, "Bequests To Beneficiary", "Bequests To Beneficiary"));
            Items.Add(new ListDataItem(ListDataItemType.OtherDeductionTypes, "Bequests To Spouse", "Bequests To Spouse"));

            Items.Add(new ListDataItem(ListDataItemType.RiskNeedsType, "Income Protection", "Income Protection"));
            Items.Add(new ListDataItem(ListDataItemType.RiskNeedsType, "Education", "Education"));
            Items.Add(new ListDataItem(ListDataItemType.RiskNeedsType, "Other", "Other"));

            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Life Cover", "Life Cover"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Disability Cover", "Disability Cover"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Dreaded Disease", "Dreaded Disease"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Income Protection", "Income Protection"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Education", "Education"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Keyman", "Keyman"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Contingent Liability", "Contingent Liability"));
            Items.Add(new ListDataItem(ListDataItemType.RiskPolicyType, "Buy And Sell", "Buy And Sell"));

            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Life Cover", "Life Cover"));
            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Disability Cover", "Disability Cover"));
            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Dreaded Disease", "Dreaded Disease"));
            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Income Protection", "Income Protection"));
            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Keyman", "Keyman"));
            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Contingent Liability", "Contingent Liability"));
            Items.Add(new ListDataItem(ListDataItemType.RiskCoverType, "Buy And Sell", "Buy And Sell"));

            Items.Add(new ListDataItem(ListDataItemType.YesNo, false, "No"));
            Items.Add(new ListDataItem(ListDataItemType.YesNo, true, "Yes"));

        }

        public List<ListDataItem> List(ListDataItemType listType)
        {
            return Items.Where<ListDataItem>(x => x.ListType == listType).ToList();
        }
    }

    [DefaultProperty("Value")]
    public class ListDataItem : BaseEntity<int>
    {
        virtual public ListDataItemType ListType { get; set; }

        virtual public object Value { get; set; }
        virtual public string Text { get; set; }

        public ListDataItem()
        {

        }
        public ListDataItem(ListDataItemType listType, object value, string text)
        {
            ListType = listType;
            Value = value;
            Text = text;

        }
    }

    public enum ListDataItemType
    {
        AssetTypes,
        AssetClass,
        LiabilityTypes,
        LiabilityClass,
        Titles,
        Gender,
        MaritalStatus,
        DBTypes,
        ContactTypes,
        BnkAcctTypes,
        IncomeTypes,
        IncomeClass,
        ExpenseTypes,
        ExpenseClass,
        ExpensePerc,
        RetirementAssetClass,
        RetirementWantsClass,
        RetirementWants,
        InvestmentType,
        InvestmentClass,
        InvestmentStatus,
        Lisp,
        ClientInstructionType,
        ClientInstructionStatus,
        ClientInstructionView,
        UserDesignation,
        RiskProfileStatus,
        DependentType,
        FundRiskCat,
        ClientStatus,
        MeetingStatus,
        MeetingType,
        Banks,
        NeedStatus,
        Language,
        MedicalBenefit,
        LifeBenefit,
        User,
        LispFund,
        PolicyStatus,
        InstructionStatus,
        MonthsOfYear,
        DaysOfMonth,
        ClientSegment,
        CommsType,
        CustomTaskType,
        CustomTaskStatus,
        MaritalRegimes,
        OtherDeductionTypes,
        RiskNeedsType,
        RiskPolicyType,
        RiskCoverType,
        YesNo


    }
}
