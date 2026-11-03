package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailDomainEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailDomainEntity>(EmailDomainTable)

    var createdAt by EmailDomainTable.createdAt
    var updatedAt by EmailDomainTable.updatedAt
    var status by EmailDomainTable.status
    var selector by EmailDomainTable.selector
    var domain by EmailDomainTable.domain
    var publicKey by EmailDomainTable.publicKey
    var privateKey by EmailDomainTable.privateKey
    var lastVerifiedAt by EmailDomainTable.lastVerifiedAt
    var organisationId by EmailDomainTable.organisationId
    var organisation by OrganisationEntity referencedOn EmailDomainTable.organisationId
    val emails by OrganisationEmailEntity referrersOn OrganisationEmailTable.emailDomainId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailDomainTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
