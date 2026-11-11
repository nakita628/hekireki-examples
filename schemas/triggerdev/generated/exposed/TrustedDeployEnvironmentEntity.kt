package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TrustedDeployEnvironmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TrustedDeployEnvironmentEntity>(TrustedDeployEnvironmentTable)

    var trustedDeployRepository by TrustedDeployRepositoryEntity referencedOn TrustedDeployEnvironmentTable.trustedDeployRepositoryId
    var trustedDeployRepositoryId by TrustedDeployEnvironmentTable.trustedDeployRepositoryId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn TrustedDeployEnvironmentTable.runtimeEnvironmentId
    var runtimeEnvironmentId by TrustedDeployEnvironmentTable.runtimeEnvironmentId
    var enabled by TrustedDeployEnvironmentTable.enabled
    var scope by TrustedDeployEnvironmentTable.scope
    var scopeValue by TrustedDeployEnvironmentTable.scopeValue
    var additionalClaims by TrustedDeployEnvironmentTable.additionalClaims
    var createdAt by TrustedDeployEnvironmentTable.createdAt
    var updatedAt by TrustedDeployEnvironmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TrustedDeployEnvironmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
