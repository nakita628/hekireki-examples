package calcom.models

enum class CreditUsageType(
    val dbName: String,
) {
    SMS("SMS"),
    CAL_AI_PHONE_CALL("CAL_AI_PHONE_CALL"),
}
