package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TemplateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TemplateEntity>(TemplateTable)

    var baseId by TemplateTable.baseId
    var cover by TemplateTable.cover
    var name by TemplateTable.name
    var description by TemplateTable.description
    var markdownDescription by TemplateTable.markdownDescription
    var categoryId by TemplateTable.categoryId
    var createdTime by TemplateTable.createdTime
    var createdBy by TemplateTable.createdBy
    var lastModifiedTime by TemplateTable.lastModifiedTime
    var lastModifiedBy by TemplateTable.lastModifiedBy
    var isSystem by TemplateTable.isSystem
    var isPublished by TemplateTable.isPublished
    var featured by TemplateTable.featured
    var snapshot by TemplateTable.snapshot
    var order by TemplateTable.order
    var usageCount by TemplateTable.usageCount
    var publishInfo by TemplateTable.publishInfo
    var visitCount by TemplateTable.visitCount
    var kind by TemplateTable.kind

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TemplateTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
