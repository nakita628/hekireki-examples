package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ContactAttributeKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ContactAttributeKeyEntity>(ContactAttributeKeyTable)

    var createdAt by ContactAttributeKeyTable.createdAt
    var updatedAt by ContactAttributeKeyTable.updatedAt
    var isUnique by ContactAttributeKeyTable.isUnique
    var key by ContactAttributeKeyTable.key
    var name by ContactAttributeKeyTable.name
    var description by ContactAttributeKeyTable.description
    var type by ContactAttributeKeyTable.type
    var dataType by ContactAttributeKeyTable.dataType
    var workspace by WorkspaceEntity referencedOn ContactAttributeKeyTable.workspaceId
    var workspaceId by ContactAttributeKeyTable.workspaceId
    val attributes by ContactAttributeEntity referrersOn ContactAttributeTable.attributeKeyId
    val attributeFilters by SurveyAttributeFilterEntity referrersOn SurveyAttributeFilterTable.attributeKeyId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ContactAttributeKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
