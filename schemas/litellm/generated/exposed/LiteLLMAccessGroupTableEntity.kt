package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMAccessGroupTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMAccessGroupTableEntity>(LiteLLMAccessGroupTableTable)

    var accessGroupName by LiteLLMAccessGroupTableTable.accessGroupName
    var description by LiteLLMAccessGroupTableTable.description
    var accessModelNames by LiteLLMAccessGroupTableTable.accessModelNames
    var accessMcpServerIds by LiteLLMAccessGroupTableTable.accessMcpServerIds
    var mcpToolPermissions by LiteLLMAccessGroupTableTable.mcpToolPermissions
    var accessAgentIds by LiteLLMAccessGroupTableTable.accessAgentIds
    var accessSkillNames by LiteLLMAccessGroupTableTable.accessSkillNames
    var assignedTeamIds by LiteLLMAccessGroupTableTable.assignedTeamIds
    var assignedKeyIds by LiteLLMAccessGroupTableTable.assignedKeyIds
    var createdAt by LiteLLMAccessGroupTableTable.createdAt
    var createdBy by LiteLLMAccessGroupTableTable.createdBy
    var updatedAt by LiteLLMAccessGroupTableTable.updatedAt
    var updatedBy by LiteLLMAccessGroupTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMAccessGroupTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
