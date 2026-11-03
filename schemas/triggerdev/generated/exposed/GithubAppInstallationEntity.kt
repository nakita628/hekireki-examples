package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GithubAppInstallationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GithubAppInstallationEntity>(GithubAppInstallationTable)

    var appInstallationId by GithubAppInstallationTable.appInstallationId
    var targetId by GithubAppInstallationTable.targetId
    var targetType by GithubAppInstallationTable.targetType
    var accountHandle by GithubAppInstallationTable.accountHandle
    var permissions by GithubAppInstallationTable.permissions
    var repositorySelection by GithubAppInstallationTable.repositorySelection
    var installedBy by GithubAppInstallationTable.installedBy
    var installedByUserId by GithubAppInstallationTable.installedByUserId
    var organization by OrganizationEntity referencedOn GithubAppInstallationTable.organizationId
    var organizationId by GithubAppInstallationTable.organizationId
    val repositories by GithubRepositoryEntity referrersOn GithubRepositoryTable.installationId
    var deletedAt by GithubAppInstallationTable.deletedAt
    var suspendedAt by GithubAppInstallationTable.suspendedAt
    var createdAt by GithubAppInstallationTable.createdAt
    var updatedAt by GithubAppInstallationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GithubAppInstallationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
