package documenso.models

enum class OrganisationGroupType(
    val dbName: String,
) {
    INTERNAL_ORGANISATION("INTERNAL_ORGANISATION"),
    INTERNAL_TEAM("INTERNAL_TEAM"),
    CUSTOM("CUSTOM"),
}
