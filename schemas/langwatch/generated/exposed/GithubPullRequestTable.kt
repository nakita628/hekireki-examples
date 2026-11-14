package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GithubPullRequestTable : IdTable<String>("\"GithubPullRequest\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val repositoryHost = text("repositoryHost")
    val repositoryFullName = text("repositoryFullName")
    val headBranch = text("headBranch")
    val prNumber = integer("prNumber")
    val htmlUrl = text("htmlUrl")
    val title = text("title")
    val state = text("state")
    val isDraft = bool("isDraft").default(false)
    val authorLogin = text("authorLogin").nullable()
    val prCreatedAt = pgTimestamp("prCreatedAt", 3)
    val prClosedAt = pgTimestamp("prClosedAt", 3).nullable()
    val prMergedAt = pgTimestamp("prMergedAt", 3).nullable()
    val prUpdatedAt = pgTimestamp("prUpdatedAt", 3).nullable()
    val mappedAt = pgTimestamp("mappedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastCheckedAt =
        pgTimestamp("lastCheckedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GithubPullRequest_pkey\"")

    init {
        index(
            "GithubPullRequest_organizationId_repositoryHost_repositoryF_idx",
            false,
            organizationId,
            repositoryHost,
            repositoryFullName,
            headBranch,
        )
        uniqueIndex(
            "GithubPullRequest_organizationId_repositoryHost_repositoryF_key",
            organizationId,
            repositoryHost,
            repositoryFullName,
            prNumber,
        )
    }
}
