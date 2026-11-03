package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RuntimeEnvironmentSessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RuntimeEnvironmentSessionEntity>(RuntimeEnvironmentSessionTable)

    var ipAddress by RuntimeEnvironmentSessionTable.ipAddress
    var environment by RuntimeEnvironmentEntity referencedOn RuntimeEnvironmentSessionTable.environmentId
    var environmentId by RuntimeEnvironmentSessionTable.environmentId
    var createdAt by RuntimeEnvironmentSessionTable.createdAt
    var updatedAt by RuntimeEnvironmentSessionTable.updatedAt
    var disconnectedAt by RuntimeEnvironmentSessionTable.disconnectedAt
    val currentEnvironments by RuntimeEnvironmentEntity optionalReferrersOn RuntimeEnvironmentTable.currentSessionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RuntimeEnvironmentSessionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
