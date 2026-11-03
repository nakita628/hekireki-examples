package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayConfigEntity>(GatewayConfigTable)

    val organization by OrganizationEntity referencedOn GatewayConfigTable.id
    var defaultIngestionProjectId by GatewayConfigTable.defaultIngestionProjectId
    var defaultIngestionProject by ProjectEntity optionalReferencedOn GatewayConfigTable.defaultIngestionProjectId
    var ingestionMode by GatewayConfigTable.ingestionMode
    var createdAt by GatewayConfigTable.createdAt
    var updatedAt by GatewayConfigTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayConfigTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
