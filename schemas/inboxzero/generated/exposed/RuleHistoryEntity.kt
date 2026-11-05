package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RuleHistoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RuleHistoryEntity>(RuleHistoryTable)

    var createdAt by RuleHistoryTable.createdAt
    var ruleId by RuleHistoryTable.ruleId
    var rule by RuleEntity referencedOn RuleHistoryTable.ruleId
    var version by RuleHistoryTable.version
    var triggerType by RuleHistoryTable.triggerType
    var promptText by RuleHistoryTable.promptText
    var name by RuleHistoryTable.name
    var instructions by RuleHistoryTable.instructions
    var enabled by RuleHistoryTable.enabled
    var automate by RuleHistoryTable.automate
    var runOnThreads by RuleHistoryTable.runOnThreads
    var conditionalOperator by RuleHistoryTable.conditionalOperator
    var from by RuleHistoryTable.from
    var to by RuleHistoryTable.to
    var subject by RuleHistoryTable.subject
    var body by RuleHistoryTable.body
    var categoryFilterType by RuleHistoryTable.categoryFilterType
    var systemType by RuleHistoryTable.systemType
    var actions by RuleHistoryTable.actions
    var categoryFilters by RuleHistoryTable.categoryFilters
}
