package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PromptDependencyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PromptDependencyEntity>(PromptDependencyTable)

    var createdAt by PromptDependencyTable.createdAt
    var updatedAt by PromptDependencyTable.updatedAt
    var projectId by PromptDependencyTable.projectId
    var project by ProjectEntity referencedOn PromptDependencyTable.projectId
    var parentId by PromptDependencyTable.parentId
    var parent by PromptEntity referencedOn PromptDependencyTable.parentId
    var childName by PromptDependencyTable.childName
    var childLabel by PromptDependencyTable.childLabel
    var childVersion by PromptDependencyTable.childVersion

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PromptDependencyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
