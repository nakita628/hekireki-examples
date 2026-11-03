package teable.models

enum class SpaceDataDbBindingMode(
    val dbName: String,
) {
    DEFAULT("default"),
    BYODB("byodb"),
}
