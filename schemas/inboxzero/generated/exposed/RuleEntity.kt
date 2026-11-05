package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RuleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RuleEntity>(RuleTable)

    var createdAt by RuleTable.createdAt
    var updatedAt by RuleTable.updatedAt
    var name by RuleTable.name
    val attachmentSources by AttachmentSourceEntity referrersOn AttachmentSourceTable.ruleId
    var enabled by RuleTable.enabled
    var automate by RuleTable.automate
    var runOnThreads by RuleTable.runOnThreads
    var emailAccountId by RuleTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn RuleTable.emailAccountId
    var organizationRuleId by RuleTable.organizationRuleId
    var organizationRule by OrganizationRuleEntity optionalReferencedOn RuleTable.organizationRuleId
    var organizationRuleMemberEnabled by RuleTable.organizationRuleMemberEnabled
    val executedRules by ExecutedRuleEntity optionalReferrersOn ExecutedRuleTable.ruleId
    val classificationFeedback by ClassificationFeedbackEntity referrersOn ClassificationFeedbackTable.ruleId
    var conditionalOperator by RuleTable.conditionalOperator
    var instructions by RuleTable.instructions
    var groupId by RuleTable.groupId
    var group by GroupEntity optionalReferencedOn RuleTable.groupId
    var from by RuleTable.from
    var to by RuleTable.to
    var subject by RuleTable.subject
    var body by RuleTable.body
    var categoryFilterType by RuleTable.categoryFilterType
    var categoryFilters by CategoryEntity via CategoryToRuleTable
    var systemType by RuleTable.systemType
    var promptText by RuleTable.promptText
    val history by RuleHistoryEntity referrersOn RuleHistoryTable.ruleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
