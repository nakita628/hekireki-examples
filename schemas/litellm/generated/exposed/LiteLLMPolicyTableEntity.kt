package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMPolicyTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMPolicyTableEntity>(LiteLLMPolicyTableTable)

    var policyName by LiteLLMPolicyTableTable.policyName
    var versionNumber by LiteLLMPolicyTableTable.versionNumber
    var versionStatus by LiteLLMPolicyTableTable.versionStatus
    var parentVersionId by LiteLLMPolicyTableTable.parentVersionId
    var isLatest by LiteLLMPolicyTableTable.isLatest
    var publishedAt by LiteLLMPolicyTableTable.publishedAt
    var productionAt by LiteLLMPolicyTableTable.productionAt
    var inherit by LiteLLMPolicyTableTable.inherit
    var description by LiteLLMPolicyTableTable.description
    var guardrailsAdd by LiteLLMPolicyTableTable.guardrailsAdd
    var guardrailsRemove by LiteLLMPolicyTableTable.guardrailsRemove
    var condition by LiteLLMPolicyTableTable.condition
    var pipeline by LiteLLMPolicyTableTable.pipeline
    var createdAt by LiteLLMPolicyTableTable.createdAt
    var createdBy by LiteLLMPolicyTableTable.createdBy
    var updatedAt by LiteLLMPolicyTableTable.updatedAt
    var updatedBy by LiteLLMPolicyTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMPolicyTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
