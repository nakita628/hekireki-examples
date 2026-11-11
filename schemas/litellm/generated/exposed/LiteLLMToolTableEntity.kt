package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMToolTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMToolTableEntity>(LiteLLMToolTableTable)

    var toolName by LiteLLMToolTableTable.toolName
    var origin by LiteLLMToolTableTable.origin
    var inputPolicy by LiteLLMToolTableTable.inputPolicy
    var outputPolicy by LiteLLMToolTableTable.outputPolicy
    var callCount by LiteLLMToolTableTable.callCount
    var assignments by LiteLLMToolTableTable.assignments
    var keyHash by LiteLLMToolTableTable.keyHash
    var teamId by LiteLLMToolTableTable.teamId
    var keyAlias by LiteLLMToolTableTable.keyAlias
    var userAgent by LiteLLMToolTableTable.userAgent
    var lastUsedAt by LiteLLMToolTableTable.lastUsedAt
    var createdAt by LiteLLMToolTableTable.createdAt
    var createdBy by LiteLLMToolTableTable.createdBy
    var updatedAt by LiteLLMToolTableTable.updatedAt
    var updatedBy by LiteLLMToolTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMToolTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
