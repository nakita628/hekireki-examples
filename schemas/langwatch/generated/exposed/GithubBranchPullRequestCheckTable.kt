package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GithubBranchPullRequestCheckTable : IdTable<String>("\"GithubBranchPullRequestCheck\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val repositoryHost = text("repositoryHost")
    val repositoryFullName = text("repositoryFullName")
    val headBranch = text("headBranch")
    val lastCheckedAt = pgTimestamp("lastCheckedAt", 3)
    val prCount = integer("prCount").default(0)
    val notFoundAt = pgTimestamp("notFoundAt", 3).nullable()
    val recheckAfter = pgTimestamp("recheckAfter", 3).nullable()
    val attempts = integer("attempts").default(0)
    val lastRequestedAt = pgTimestamp("lastRequestedAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GithubBranchPullRequestCheck_pkey\"")

    init {
        index("GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_n_idx", false, recheckAfter, lastRequestedAt, notFoundAt)
        uniqueIndex(
            "GithubBranchPullRequestCheck_organizationId_repositoryHost__key",
            organizationId,
            repositoryHost,
            repositoryFullName,
            headBranch,
        )
    }
}
