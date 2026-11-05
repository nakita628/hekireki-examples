package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationEntity>(OrganizationTable)

    var name by OrganizationTable.name
    var slug by OrganizationTable.slug
    var logo by OrganizationTable.logo
    var metadata by OrganizationTable.metadata
    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt
    val members by MemberEntity referrersOn MemberTable.organizationId
    val ssoProvider by SsoProviderEntity optionalBackReferencedOn SsoProviderTable.organizationId
    val sessions by SessionEntity optionalReferrersOn SessionTable.activeOrganizationId
    val invitations by InvitationEntity referrersOn InvitationTable.organizationId
    val rules by OrganizationRuleEntity referrersOn OrganizationRuleTable.organizationId
    val conversations by ConversationEntity referrersOn ConversationTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
