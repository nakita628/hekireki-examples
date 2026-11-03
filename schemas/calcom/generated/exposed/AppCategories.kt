package calcom.models

enum class AppCategories(
    val dbName: String,
) {
    CALENDAR("calendar"),
    MESSAGING("messaging"),
    OTHER("other"),
    PAYMENT("payment"),
    VIDEO("video"),
    WEB3("web3"),
    AUTOMATION("automation"),
    ANALYTICS("analytics"),
    CONFERENCING("conferencing"),
    CRM("crm"),
}
