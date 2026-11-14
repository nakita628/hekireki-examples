package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IngestionTemplateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IngestionTemplateEntity>(IngestionTemplateTable)

    var organizationId by IngestionTemplateTable.organizationId
    var slug by IngestionTemplateTable.slug
    var sourceType by IngestionTemplateTable.sourceType
    var displayName by IngestionTemplateTable.displayName
    var description by IngestionTemplateTable.description
    var iconAsset by IngestionTemplateTable.iconAsset
    var credentialSchema by IngestionTemplateTable.credentialSchema
    var ottlRules by IngestionTemplateTable.ottlRules
    var platformPublished by IngestionTemplateTable.platformPublished
    var enabled by IngestionTemplateTable.enabled
    var archivedAt by IngestionTemplateTable.archivedAt
    var createdAt by IngestionTemplateTable.createdAt
    var updatedAt by IngestionTemplateTable.updatedAt
    var createdById by IngestionTemplateTable.createdById
    var updatedById by IngestionTemplateTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IngestionTemplateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
