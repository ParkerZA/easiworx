create or replace VIEW clientretirementportfolio_view as
select
    distinct c.Id as ClientId,
    cd.FirstName as FirstName,
    cd.LastName as LastName,
    cd.IdentificationNo as IdentificationNo,
    cd.DateOfBirth as DateOfBirth,
    cd.PassportNo as PassportNo,
    r.Id as RetirementId,
    r.ClientPortfolio_id as ClientPortfolioId,
    r.Description as RetirementDescription,
    r.ReferenceNo as PolicyNo,
    f.Id as FundId,
    f.Description as FundName,
    f.CurrentAmount as CurrentAmount
from
    ((((client c
join clientdetails cd on
    ((cd.ClientId = c.Id)))
join clientportfolio cp on
    ((cp.Id = c.ClientPortfolio_id)))
join retirement r on
    ((r.ClientPortfolio_id = cp.Id)))
join fund f on
    ((f.Retirement_id = r.Id)))
where
    (length(cd.FirstName) > 0)
order by
    c.Id;