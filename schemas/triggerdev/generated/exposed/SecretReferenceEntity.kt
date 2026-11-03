package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SecretReferenceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SecretReferenceEntity>(SecretReferenceTable)

    var key by SecretReferenceTable.key
    var provider by SecretReferenceTable.provider
    val environmentVariableValues by EnvironmentVariableValueEntity optionalReferrersOn EnvironmentVariableValueTable.valueReferenceId
    var createdAt by SecretReferenceTable.createdAt
    var updatedAt by SecretReferenceTable.updatedAt
    val organizationIntegration by OrganizationIntegrationEntity referrersOn OrganizationIntegrationTable.tokenReferenceId
    val user by UserEntity optionalReferrersOn UserTable.mfaSecretReferenceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SecretReferenceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
