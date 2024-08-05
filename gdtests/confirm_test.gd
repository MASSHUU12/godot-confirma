class_name ConfirmTest


func confirm_null_when_null() -> void:
	Confirm.is_null(null)


func confirm_not_null_when_not_null() -> void:
	Confirm.is_not_null(true)
