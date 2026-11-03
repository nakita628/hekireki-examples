package documenso.models

enum class EmailDomainStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    ACTIVE("ACTIVE"),
}
