package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalAiPhoneNumberEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CalAiPhoneNumberEntity>(CalAiPhoneNumberTable)

    var userId by CalAiPhoneNumberTable.userId
    var user by UserEntity optionalReferencedOn CalAiPhoneNumberTable.userId
    var teamId by CalAiPhoneNumberTable.teamId
    var team by TeamEntity optionalReferencedOn CalAiPhoneNumberTable.teamId
    var phoneNumber by CalAiPhoneNumberTable.phoneNumber
    var provider by CalAiPhoneNumberTable.provider
    var providerPhoneNumberId by CalAiPhoneNumberTable.providerPhoneNumberId
    var createdAt by CalAiPhoneNumberTable.createdAt
    var updatedAt by CalAiPhoneNumberTable.updatedAt
    var stripeCustomerId by CalAiPhoneNumberTable.stripeCustomerId
    var stripeSubscriptionId by CalAiPhoneNumberTable.stripeSubscriptionId
    var subscriptionStatus by CalAiPhoneNumberTable.subscriptionStatus
    var inboundAgentId by CalAiPhoneNumberTable.inboundAgentId
    var inboundAgent by AgentEntity optionalReferencedOn CalAiPhoneNumberTable.inboundAgentId
    var outboundAgentId by CalAiPhoneNumberTable.outboundAgentId
    var outboundAgent by AgentEntity optionalReferencedOn CalAiPhoneNumberTable.outboundAgentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalAiPhoneNumberTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
