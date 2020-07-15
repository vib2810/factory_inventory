use FactoryData

UPDATE Dyeing_Issue_Voucher 
    SET Net_Weight = (
        SELECT Net_Weight
        FROM Batch
        WHERE Dyeing_Issue_Voucher.Batch_No = Batch.Batch_No
    );