package formbricks.models

enum class RetentionSkipReason(
    val dbName: String,
) {
    EXEMPT("exempt"),
    LAST_OWNER("lastOwner"),
    OTHER_ORGANIZATION("otherOrganization"),
    NO_RECIPIENT("noRecipient"),
}
