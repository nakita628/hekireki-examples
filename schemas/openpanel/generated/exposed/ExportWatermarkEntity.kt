package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class ExportWatermarkEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ExportWatermarkEntity>(ExportWatermarkTable)

    var project by ProjectEntity referencedOn ExportWatermarkTable.projectId
    var projectId by ExportWatermarkTable.projectId
    var integration by IntegrationEntity referencedOn ExportWatermarkTable.integrationId
    var integrationId by ExportWatermarkTable.integrationId
    var lastInsertedAt by ExportWatermarkTable.lastInsertedAt
    var lastEventId by ExportWatermarkTable.lastEventId
    var createdAt by ExportWatermarkTable.createdAt
    var updatedAt by ExportWatermarkTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExportWatermarkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
