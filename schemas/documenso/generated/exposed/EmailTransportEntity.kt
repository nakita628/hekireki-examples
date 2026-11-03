package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailTransportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailTransportEntity>(EmailTransportTable)

    var createdAt by EmailTransportTable.createdAt
    var updatedAt by EmailTransportTable.updatedAt
    var name by EmailTransportTable.name
    var type by EmailTransportTable.type
    var fromName by EmailTransportTable.fromName
    var fromAddress by EmailTransportTable.fromAddress
    var config by EmailTransportTable.config
    val subscriptionClaims by SubscriptionClaimEntity optionalReferrersOn SubscriptionClaimTable.emailTransportId
    val organisationClaims by OrganisationClaimEntity optionalReferrersOn OrganisationClaimTable.emailTransportId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailTransportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
