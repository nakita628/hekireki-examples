package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GithubInstallationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GithubInstallationEntity>(GithubInstallationTable)

    var installationId by GithubInstallationTable.installationId
    var organizationId by GithubInstallationTable.organizationId
    var accountLogin by GithubInstallationTable.accountLogin
    var accountType by GithubInstallationTable.accountType
    var accountId by GithubInstallationTable.accountId
    var repositorySelection by GithubInstallationTable.repositorySelection
    var repositories by GithubInstallationTable.repositories
    var suspendedAt by GithubInstallationTable.suspendedAt
    var createdAt by GithubInstallationTable.createdAt
    var updatedAt by GithubInstallationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GithubInstallationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
