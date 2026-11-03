package triggerdev.models

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

    var friendlyId by PromptTable.friendlyId
    var slug by PromptTable.slug
    var description by PromptTable.description
    var type by PromptTable.type
    var organization by OrganizationEntity referencedOn PromptTable.organizationId
    var organizationId by PromptTable.organizationId
    var project by ProjectEntity referencedOn PromptTable.projectId
    var projectId by PromptTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn PromptTable.runtimeEnvironmentId
    var runtimeEnvironmentId by PromptTable.runtimeEnvironmentId
    var filePath by PromptTable.filePath
    var exportName by PromptTable.exportName
    var variableSchema by PromptTable.variableSchema
    var defaultModel by PromptTable.defaultModel
    var defaultConfig by PromptTable.defaultConfig
    var tags by PromptTable.tags
    var archivedAt by PromptTable.archivedAt
    var createdAt by PromptTable.createdAt
    var updatedAt by PromptTable.updatedAt
    val versions by PromptVersionEntity referrersOn PromptVersionTable.promptId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PromptTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
