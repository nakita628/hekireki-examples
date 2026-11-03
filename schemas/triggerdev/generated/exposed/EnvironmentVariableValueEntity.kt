package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EnvironmentVariableValueEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvironmentVariableValueEntity>(EnvironmentVariableValueTable)

    var valueReference by SecretReferenceEntity optionalReferencedOn EnvironmentVariableValueTable.valueReferenceId
    var valueReferenceId by EnvironmentVariableValueTable.valueReferenceId
    var variable by EnvironmentVariableEntity referencedOn EnvironmentVariableValueTable.variableId
    var variableId by EnvironmentVariableValueTable.variableId
    var environment by RuntimeEnvironmentEntity referencedOn EnvironmentVariableValueTable.environmentId
    var environmentId by EnvironmentVariableValueTable.environmentId
    var isSecret by EnvironmentVariableValueTable.isSecret
    var createdAt by EnvironmentVariableValueTable.createdAt
    var updatedAt by EnvironmentVariableValueTable.updatedAt
    var version by EnvironmentVariableValueTable.version
    var lastUpdatedBy by EnvironmentVariableValueTable.lastUpdatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EnvironmentVariableValueTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
