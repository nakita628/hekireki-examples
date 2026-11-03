package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttributeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttributeEntity>(AttributeTable)

    var team by TeamEntity referencedOn AttributeTable.teamId
    var teamId by AttributeTable.teamId
    var type by AttributeTable.type
    var name by AttributeTable.name
    var slug by AttributeTable.slug
    var enabled by AttributeTable.enabled
    var usersCanEditRelation by AttributeTable.usersCanEditRelation
    var createdAt by AttributeTable.createdAt
    var updatedAt by AttributeTable.updatedAt
    val options by AttributeOptionEntity referrersOn AttributeOptionTable.attributeId
    var isWeightsEnabled by AttributeTable.isWeightsEnabled
    var isLocked by AttributeTable.isLocked
    val attributeSyncFieldMappings by AttributeSyncFieldMappingEntity referrersOn AttributeSyncFieldMappingTable.attributeId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttributeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
