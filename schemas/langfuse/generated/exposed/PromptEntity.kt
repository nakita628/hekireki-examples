package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PromptEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PromptEntity>(PromptTable)

    var createdAt by PromptTable.createdAt
    var updatedAt by PromptTable.updatedAt
    var projectId by PromptTable.projectId
    var project by ProjectEntity referencedOn PromptTable.projectId
    var createdBy by PromptTable.createdBy
    var prompt by PromptTable.prompt
    var name by PromptTable.name
    var version by PromptTable.version
    var type by PromptTable.type
    var isActive by PromptTable.isActive
    var config by PromptTable.config
    var tags by PromptTable.tags
    var labels by PromptTable.labels
    var commitMessage by PromptTable.commitMessage
    val promptDependency by PromptDependencyEntity referrersOn PromptDependencyTable.parentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PromptTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
