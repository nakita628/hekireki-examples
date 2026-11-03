package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttributeSyncFieldMappingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttributeSyncFieldMappingEntity>(AttributeSyncFieldMappingTable)

    var integrationFieldName by AttributeSyncFieldMappingTable.integrationFieldName
    var attributeId by AttributeSyncFieldMappingTable.attributeId
    var attribute by AttributeEntity referencedOn AttributeSyncFieldMappingTable.attributeId
    var enabled by AttributeSyncFieldMappingTable.enabled
    var integrationAttributeSyncId by AttributeSyncFieldMappingTable.integrationAttributeSyncId
    var integrationAttributeSync by IntegrationAttributeSyncEntity referencedOn AttributeSyncFieldMappingTable.integrationAttributeSyncId
    var createdAt by AttributeSyncFieldMappingTable.createdAt
    var updatedAt by AttributeSyncFieldMappingTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttributeSyncFieldMappingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
