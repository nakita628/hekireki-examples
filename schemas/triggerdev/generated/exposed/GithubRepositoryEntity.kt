package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GithubRepositoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GithubRepositoryEntity>(GithubRepositoryTable)

    var githubId by GithubRepositoryTable.githubId
    var name by GithubRepositoryTable.name
    var fullName by GithubRepositoryTable.fullName
    var htmlUrl by GithubRepositoryTable.htmlUrl
    var private by GithubRepositoryTable.private
    var defaultBranch by GithubRepositoryTable.defaultBranch
    var installation by GithubAppInstallationEntity referencedOn GithubRepositoryTable.installationId
    var installationId by GithubRepositoryTable.installationId
    var createdAt by GithubRepositoryTable.createdAt
    var updatedAt by GithubRepositoryTable.updatedAt
    val connectedGithubRepository by ConnectedGithubRepositoryEntity referrersOn ConnectedGithubRepositoryTable.repositoryId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GithubRepositoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
