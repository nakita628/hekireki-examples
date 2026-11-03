package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GitHubEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GitHubEntity>(GitHubTable)

    var login by GitHubTable.login
    var name by GitHubTable.name
    var token by GitHubTable.token
    var jobId by GitHubTable.jobId
    var organizationId by GitHubTable.organizationId
    var createdAt by GitHubTable.createdAt
    var updatedAt by GitHubTable.updatedAt
    var organization by OrganizationEntity referencedOn GitHubTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GitHubTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
