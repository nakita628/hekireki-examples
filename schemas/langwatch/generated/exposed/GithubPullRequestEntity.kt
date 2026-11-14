package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GithubPullRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GithubPullRequestEntity>(GithubPullRequestTable)

    var organizationId by GithubPullRequestTable.organizationId
    var repositoryHost by GithubPullRequestTable.repositoryHost
    var repositoryFullName by GithubPullRequestTable.repositoryFullName
    var headBranch by GithubPullRequestTable.headBranch
    var prNumber by GithubPullRequestTable.prNumber
    var htmlUrl by GithubPullRequestTable.htmlUrl
    var title by GithubPullRequestTable.title
    var state by GithubPullRequestTable.state
    var isDraft by GithubPullRequestTable.isDraft
    var authorLogin by GithubPullRequestTable.authorLogin
    var prCreatedAt by GithubPullRequestTable.prCreatedAt
    var prClosedAt by GithubPullRequestTable.prClosedAt
    var prMergedAt by GithubPullRequestTable.prMergedAt
    var prUpdatedAt by GithubPullRequestTable.prUpdatedAt
    var mappedAt by GithubPullRequestTable.mappedAt
    var lastCheckedAt by GithubPullRequestTable.lastCheckedAt
    var createdAt by GithubPullRequestTable.createdAt
    var updatedAt by GithubPullRequestTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GithubPullRequestTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
