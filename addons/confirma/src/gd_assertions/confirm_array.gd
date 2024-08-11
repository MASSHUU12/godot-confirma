class_name ConfirmArray


static func is_of_size(actual: Array, expected: int, message: String = "") -> Array:
	if (not actual.size() == expected):
		Confirma.emit_signal(
			"GdAssertionFailed", 
			"Array size is %s, but expected %s." % actual.size(), expected \
			if message.is_empty() else message
		)
	return actual


static func is_empty(actual: Array, message: String = "") -> Array:
	if (not actual.size() == 0):
		Confirma.emit_signal(
			"GdAssertionFailed",
			"Array is not empty." \
			if message.is_empty() else message
		)
	return actual


static func is_not_empty(actual: Array, message: String = "") -> Array:
	if (not actual.size() > 0):
		Confirma.emit_signal(
			"GdAssertionFailed",
			"Array is empty." \
			if message.is_empty() else message
		)
	return actual


static func contains(actual: Array, expected: Variant, message: String = "") -> Array:
	if (not actual.has(expected)):
		Confirma.emit_signal(
			"GdAssertionFailed",
			"Array does not contain '%s'." % expected \
			if message.is_empty() else message
		)
	return actual


static func not_contains(actual: Array, expected: Variant, message: String = "") -> Array:
	if (actual.has(expected)):
		Confirma.emit_signal(
			"GdAssertionFailed",
			"Array contains '%s'." % expected \
			if message.is_empty() else message
		)
	return actual
