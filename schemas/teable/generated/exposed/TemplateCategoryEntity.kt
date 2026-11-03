package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TemplateCategoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TemplateCategoryEntity>(TemplateCategoryTable)

    var name by TemplateCategoryTable.name
    var createdTime by TemplateCategoryTable.createdTime
    var createdBy by TemplateCategoryTable.createdBy
    var lastModifiedTime by TemplateCategoryTable.lastModifiedTime
    var lastModifiedBy by TemplateCategoryTable.lastModifiedBy
    var order by TemplateCategoryTable.order

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TemplateCategoryTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
