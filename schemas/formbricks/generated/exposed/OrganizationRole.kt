package formbricks.models

enum class OrganizationRole(
    val dbName: String,
) {
    OWNER("owner"),
    MANAGER("manager"),
    MEMBER("member"),
    BILLING("billing"),
    DASHBOARD_VIEWER("dashboard_viewer"),
}
