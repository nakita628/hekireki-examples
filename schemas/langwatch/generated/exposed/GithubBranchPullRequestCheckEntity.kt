package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GithubBranchPullRequestCheckEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GithubBranchPullRequestCheckEntity>(GithubBranchPullRequestCheckTable)

    var organizationId by GithubBranchPullRequestCheckTable.organizationId
    var repositoryHost by GithubBranchPullRequestCheckTable.repositoryHost
    var repositoryFullName by GithubBranchPullRequestCheckTable.repositoryFullName
    var headBranch by GithubBranchPullRequestCheckTable.headBranch
    var lastCheckedAt by GithubBranchPullRequestCheckTable.lastCheckedAt
    var prCount by GithubBranchPullRequestCheckTable.prCount
    var notFoundAt by GithubBranchPullRequestCheckTable.notFoundAt
    var recheckAfter by GithubBranchPullRequestCheckTable.recheckAfter
    var attempts by GithubBranchPullRequestCheckTable.attempts
    var lastRequestedAt by GithubBranchPullRequestCheckTable.lastRequestedAt
    var createdAt by GithubBranchPullRequestCheckTable.createdAt
    var updatedAt by GithubBranchPullRequestCheckTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GithubBranchPullRequestCheckTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
