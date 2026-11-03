package triggerdev.models

enum class GithubRepositorySelection(
    val dbName: String,
) {
    ALL("ALL"),
    SELECTED("SELECTED"),
}
