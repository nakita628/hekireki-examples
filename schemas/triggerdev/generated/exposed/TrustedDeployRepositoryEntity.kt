package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TrustedDeployRepositoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TrustedDeployRepositoryEntity>(TrustedDeployRepositoryTable)

    var project by ProjectEntity referencedOn TrustedDeployRepositoryTable.projectId
    var projectId by TrustedDeployRepositoryTable.projectId
    var issuer by TrustedDeployRepositoryTable.issuer
    var repositoryOwner by TrustedDeployRepositoryTable.repositoryOwner
    var repositoryName by TrustedDeployRepositoryTable.repositoryName
    var repositoryId by TrustedDeployRepositoryTable.repositoryId
    var repositoryOwnerId by TrustedDeployRepositoryTable.repositoryOwnerId
    val environments by TrustedDeployEnvironmentEntity referrersOn TrustedDeployEnvironmentTable.trustedDeployRepositoryId
    var createdAt by TrustedDeployRepositoryTable.createdAt
    var updatedAt by TrustedDeployRepositoryTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TrustedDeployRepositoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
