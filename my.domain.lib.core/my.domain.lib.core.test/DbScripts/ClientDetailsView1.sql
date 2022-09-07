CREATE  OR REPLACE VIEW ClientDetailsContactView AS
SELECT CD.Id, CONCAT(LastName, ', ', ClientTitle,' ',Initials)  AS FullName,LastName,MidName,FirstName,DateOfBirth,IdentificationNo,CD.ClientId,CD.Status , CC.CellNo,CC.EMailAddr
                            FROM clientdetails CD LEFT JOIN clientcontacts CC ON 
                            CD.Id = CC.ClientDetailsId
                            where CD.ClientId>0;