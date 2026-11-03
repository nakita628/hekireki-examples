package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CustomFieldEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomFieldEntity>(CustomFieldTable)

    var createdAt by CustomFieldTable.createdAt
    var updatedAt by CustomFieldTable.updatedAt
    var type by CustomFieldTable.type
    var identifier by CustomFieldTable.identifier
    var label by CustomFieldTable.label
    var placeholder by CustomFieldTable.placeholder
    var required by CustomFieldTable.required
    var disabled by CustomFieldTable.disabled
    var link by LinkEntity referencedOn CustomFieldTable.linkId
    var linkId by CustomFieldTable.linkId
    var orderIndex by CustomFieldTable.orderIndex

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CustomFieldTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
