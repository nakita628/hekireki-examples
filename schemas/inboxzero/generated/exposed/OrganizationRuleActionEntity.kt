package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationRuleActionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationRuleActionEntity>(OrganizationRuleActionTable)

    var createdAt by OrganizationRuleActionTable.createdAt
    var updatedAt by OrganizationRuleActionTable.updatedAt
    var organizationRuleId by OrganizationRuleActionTable.organizationRuleId
    var organizationRule by OrganizationRuleEntity referencedOn OrganizationRuleActionTable.organizationRuleId
    var type by OrganizationRuleActionTable.type
    var label by OrganizationRuleActionTable.label
    var subject by OrganizationRuleActionTable.subject
    var content by OrganizationRuleActionTable.content
    var to by OrganizationRuleActionTable.to
    var cc by OrganizationRuleActionTable.cc
    var bcc by OrganizationRuleActionTable.bcc
    var url by OrganizationRuleActionTable.url
    var folderName by OrganizationRuleActionTable.folderName
    var delayInMinutes by OrganizationRuleActionTable.delayInMinutes
    var staticAttachments by OrganizationRuleActionTable.staticAttachments

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationRuleActionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
