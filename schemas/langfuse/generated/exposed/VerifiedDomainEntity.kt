package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VerifiedDomainEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VerifiedDomainEntity>(VerifiedDomainTable)

    var organizationId by VerifiedDomainTable.organizationId
    var organization by OrganizationEntity referencedOn VerifiedDomainTable.organizationId
    var domain by VerifiedDomainTable.domain
    var verificationToken by VerifiedDomainTable.verificationToken
    var verifiedAt by VerifiedDomainTable.verifiedAt
    var createdByUserId by VerifiedDomainTable.createdByUserId
    var createdAt by VerifiedDomainTable.createdAt
    var updatedAt by VerifiedDomainTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VerifiedDomainTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
