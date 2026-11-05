package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationRuleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationRuleEntity>(OrganizationRuleTable)

    var createdAt by OrganizationRuleTable.createdAt
    var updatedAt by OrganizationRuleTable.updatedAt
    var organizationId by OrganizationRuleTable.organizationId
    var organization by OrganizationEntity referencedOn OrganizationRuleTable.organizationId
    var enabled by OrganizationRuleTable.enabled
    var name by OrganizationRuleTable.name
    var instructions by OrganizationRuleTable.instructions
    var runOnThreads by OrganizationRuleTable.runOnThreads
    var conditionalOperator by OrganizationRuleTable.conditionalOperator
    var from by OrganizationRuleTable.from
    var to by OrganizationRuleTable.to
    var subject by OrganizationRuleTable.subject
    var body by OrganizationRuleTable.body
    val actions by OrganizationRuleActionEntity referrersOn OrganizationRuleActionTable.organizationRuleId
    val memberRules by RuleEntity optionalReferrersOn RuleTable.organizationRuleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationRuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
