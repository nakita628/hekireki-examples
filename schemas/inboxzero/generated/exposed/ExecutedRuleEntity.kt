package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExecutedRuleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExecutedRuleEntity>(ExecutedRuleTable)

    var createdAt by ExecutedRuleTable.createdAt
    var updatedAt by ExecutedRuleTable.updatedAt
    var threadId by ExecutedRuleTable.threadId
    var messageId by ExecutedRuleTable.messageId
    var status by ExecutedRuleTable.status
    var automated by ExecutedRuleTable.automated
    var reason by ExecutedRuleTable.reason
    var matchMetadata by ExecutedRuleTable.matchMetadata
    var ruleId by ExecutedRuleTable.ruleId
    var rule by RuleEntity optionalReferencedOn ExecutedRuleTable.ruleId
    var emailAccountId by ExecutedRuleTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ExecutedRuleTable.emailAccountId
    val actionItems by ExecutedActionEntity referrersOn ExecutedActionTable.executedRuleId
    val scheduledActions by ScheduledActionEntity referrersOn ScheduledActionTable.executedRuleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExecutedRuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
