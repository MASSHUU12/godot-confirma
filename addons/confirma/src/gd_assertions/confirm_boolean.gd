class_name ConfirmBoolean

static var exts: CSharpScript = load(
	GdHelper.get_plugin_path() + "/wrappers/ConfirmBooleanWrapper.cs"
).new()


static func is_true(actual: bool, message: String = "") -> bool:
	return exts.ConfirmTrue(actual, message)


static func is_false(actual: bool, message: String = "") -> bool:
	return exts.ConfirmFalse(actual, message)
