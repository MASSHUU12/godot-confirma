class_name ConfirmBoolean


static func is_true(actual: bool, message: String = "") -> bool:
	if (not actual):
		Confirma.emit_signal(
			"GdAssertionFailed", 
			"Expected true but was false." \
			if message.is_empty() else message
		)
	return actual


static func is_false(actual: bool, message: String = "") -> bool:
	if (actual):
		Confirma.emit_signal(
			"GdAssertionFailed",
			"Expected false but was true." \
			if message.is_empty() else message
		)
	return actual
