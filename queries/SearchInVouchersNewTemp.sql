use FactoryData_1

SELECT temp2.*, z_colour.*
FROM
	(SELECT temp1.*,z_qualities.quality	
		FROM (SELECT z_inward.*,z_cartons.carton_id,z_cartons.quality_id,z_cartons.colour_id,z_cartons.inward_voucher_id
		FROM z_inward
		FULL OUTER JOIN z_cartons
		ON z_inward.id = z_cartons.inward_voucher_id) as temp1
	LEFT OUTER JOIN z_qualities
	ON z_qualities.quality_id = temp1.quality_id) as temp2
LEFT OUTER JOIN z_colour
ON z_colour.colour_id=temp2.colour_id
	
