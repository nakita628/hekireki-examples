package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AiToolEntryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AiToolEntryEntity>(AiToolEntryTable)

    var organizationId by AiToolEntryTable.organizationId
    var scope by AiToolEntryTable.scope
    var scopeId by AiToolEntryTable.scopeId
    var type by AiToolEntryTable.type
    var displayName by AiToolEntryTable.displayName
    var slug by AiToolEntryTable.slug
    var iconKey by AiToolEntryTable.iconKey
    var iconAsset by AiToolEntryTable.iconAsset
    var order by AiToolEntryTable.order
    var enabled by AiToolEntryTable.enabled
    var config by AiToolEntryTable.config
    var archivedAt by AiToolEntryTable.archivedAt
    var createdAt by AiToolEntryTable.createdAt
    var updatedAt by AiToolEntryTable.updatedAt
    var createdById by AiToolEntryTable.createdById
    var updatedById by AiToolEntryTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AiToolEntryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
