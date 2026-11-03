package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DomainEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DomainEntity>(DomainTable)

    var slug by DomainTable.slug
    var user by UserEntity optionalReferencedOn DomainTable.userId
    var userId by DomainTable.userId
    var teamId by DomainTable.teamId
    var team by TeamEntity referencedOn DomainTable.teamId
    var verified by DomainTable.verified
    var isDefault by DomainTable.isDefault
    var redirectUrl by DomainTable.redirectUrl
    var lastChecked by DomainTable.lastChecked
    var createdAt by DomainTable.createdAt
    var updatedAt by DomainTable.updatedAt
    val links by LinkEntity optionalReferrersOn LinkTable.domainId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DomainTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
