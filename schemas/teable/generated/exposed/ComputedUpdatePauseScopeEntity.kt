package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ComputedUpdatePauseScopeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedUpdatePauseScopeEntity>(ComputedUpdatePauseScopeTable)

    var scopeType by ComputedUpdatePauseScopeTable.scopeType
    var scopeId by ComputedUpdatePauseScopeTable.scopeId
    var pausedAt by ComputedUpdatePauseScopeTable.pausedAt
    var pausedBy by ComputedUpdatePauseScopeTable.pausedBy
    var resumeAt by ComputedUpdatePauseScopeTable.resumeAt
    var reason by ComputedUpdatePauseScopeTable.reason
    var writePolicy by ComputedUpdatePauseScopeTable.writePolicy
    var updatedAt by ComputedUpdatePauseScopeTable.updatedAt
    var updatedBy by ComputedUpdatePauseScopeTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ComputedUpdatePauseScopeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
