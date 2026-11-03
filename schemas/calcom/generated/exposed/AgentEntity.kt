package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AgentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AgentEntity>(AgentTable)

    var name by AgentTable.name
    var userId by AgentTable.userId
    var user by UserEntity optionalReferencedOn AgentTable.userId
    var teamId by AgentTable.teamId
    var team by TeamEntity optionalReferencedOn AgentTable.teamId
    var providerAgentId by AgentTable.providerAgentId
    var inboundEventTypeId by AgentTable.inboundEventTypeId
    var outboundEventTypeId by AgentTable.outboundEventTypeId
    var enabled by AgentTable.enabled
    var createdAt by AgentTable.createdAt
    var updatedAt by AgentTable.updatedAt
    val inboundPhoneNumbers by CalAiPhoneNumberEntity optionalReferrersOn CalAiPhoneNumberTable.inboundAgentId
    val outboundPhoneNumbers by CalAiPhoneNumberEntity optionalReferrersOn CalAiPhoneNumberTable.outboundAgentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AgentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
