package plunk.models

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

    var domain by DomainTable.domain
    var verified by DomainTable.verified
    var dkimTokens by DomainTable.dkimTokens
    var project by ProjectEntity referencedOn DomainTable.projectId
    var projectId by DomainTable.projectId
    var createdAt by DomainTable.createdAt
    var updatedAt by DomainTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DomainTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
