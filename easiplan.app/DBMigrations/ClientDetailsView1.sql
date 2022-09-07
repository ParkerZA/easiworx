CREATE  OR REPLACE VIEW ClientDetailsContactView AS
SELECT CD.Id, CONCAT(LastName, ', ', ClientTitle,' ',FirstName)  AS FullName,ClientTitle,LastName,MidName,FirstName,DateOfBirth,IFNULL(IF(IdentificationNo = '',NULL,IdentificationNo),PassportNo) IdentificationNo,CD.ClientId,CD.Status , CC.CellNo,CC.EMailAddr
                            FROM clientdetails CD LEFT JOIN clientcontacts CC ON 
                            CD.Id = CC.ClientDetailsId
                            where LENGTH(CD.FirstName) > 0;
                            