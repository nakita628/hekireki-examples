package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMGuardrailsTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMGuardrailsTableEntity>(LiteLLMGuardrailsTableTable)

    var guardrailName by LiteLLMGuardrailsTableTable.guardrailName
    var litellmParams by LiteLLMGuardrailsTableTable.litellmParams
    var guardrailInfo by LiteLLMGuardrailsTableTable.guardrailInfo
    var teamId by LiteLLMGuardrailsTableTable.teamId
    var createdAt by LiteLLMGuardrailsTableTable.createdAt
    var updatedAt by LiteLLMGuardrailsTableTable.updatedAt
    var status by LiteLLMGuardrailsTableTable.status
    var submittedAt by LiteLLMGuardrailsTableTable.submittedAt
    var reviewedAt by LiteLLMGuardrailsTableTable.reviewedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMGuardrailsTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
