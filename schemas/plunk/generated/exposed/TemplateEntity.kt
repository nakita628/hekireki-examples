package plunk.models

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

    var name by TemplateTable.name
    var description by TemplateTable.description
    var subject by TemplateTable.subject
    var body by TemplateTable.body
    var from by TemplateTable.from
    var fromName by TemplateTable.fromName
    var replyTo by TemplateTable.replyTo
    var type by TemplateTable.type
    var project by ProjectEntity referencedOn TemplateTable.projectId
    var projectId by TemplateTable.projectId
    val emails by EmailEntity optionalReferrersOn EmailTable.templateId
    val workflowSteps by WorkflowStepEntity optionalReferrersOn WorkflowStepTable.templateId
    var createdAt by TemplateTable.createdAt
    var updatedAt by TemplateTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TemplateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
