package documenso.models

enum class OrganisationMemberInviteStatus(
    val dbName: String,
) {
    ACCEPTED("ACCEPTED"),
    PENDING("PENDING"),
    DECLINED("DECLINED"),
}
