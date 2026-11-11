package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMSkillsTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMSkillsTableEntity>(LiteLLMSkillsTableTable)

    var displayTitle by LiteLLMSkillsTableTable.displayTitle
    var description by LiteLLMSkillsTableTable.description
    var instructions by LiteLLMSkillsTableTable.instructions
    var source by LiteLLMSkillsTableTable.source1
    var latestVersion by LiteLLMSkillsTableTable.latestVersion
    var fileContent by LiteLLMSkillsTableTable.fileContent
    var fileName by LiteLLMSkillsTableTable.fileName
    var fileType by LiteLLMSkillsTableTable.fileType
    var metadata by LiteLLMSkillsTableTable.metadata
    var createdAt by LiteLLMSkillsTableTable.createdAt
    var createdBy by LiteLLMSkillsTableTable.createdBy
    var updatedAt by LiteLLMSkillsTableTable.updatedAt
    var updatedBy by LiteLLMSkillsTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMSkillsTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
