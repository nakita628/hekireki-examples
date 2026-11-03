package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CustomFieldResponseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomFieldResponseEntity>(CustomFieldResponseTable)

    var data by CustomFieldResponseTable.data
    var viewId by CustomFieldResponseTable.viewId
    var view by ViewEntity referencedOn CustomFieldResponseTable.viewId
    var createdAt by CustomFieldResponseTable.createdAt
    var updatedAt by CustomFieldResponseTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CustomFieldResponseTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
