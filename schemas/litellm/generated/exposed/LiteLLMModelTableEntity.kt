package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMModelTableEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, LiteLLMModelTableEntity>(LiteLLMModelTableTable)

    var modelAliases by LiteLLMModelTableTable.modelAliases
    var createdAt by LiteLLMModelTableTable.createdAt
    var createdBy by LiteLLMModelTableTable.createdBy
    var updatedAt by LiteLLMModelTableTable.updatedAt
    var updatedBy by LiteLLMModelTableTable.updatedBy
    val team by LiteLLMTeamTableEntity optionalBackReferencedOn LiteLLMTeamTableTable.modelId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMModelTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
