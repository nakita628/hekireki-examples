package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayGuardrailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayGuardrailEntity>(GatewayGuardrailTable)

    var projectId by GatewayGuardrailTable.projectId
    var name by GatewayGuardrailTable.name
    var description by GatewayGuardrailTable.description
    var evaluatorId by GatewayGuardrailTable.evaluatorId
    var direction by GatewayGuardrailTable.direction
    var failureMode by GatewayGuardrailTable.failureMode
    var archivedAt by GatewayGuardrailTable.archivedAt
    var createdAt by GatewayGuardrailTable.createdAt
    var updatedAt by GatewayGuardrailTable.updatedAt
    var createdById by GatewayGuardrailTable.createdById
    var updatedById by GatewayGuardrailTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayGuardrailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
