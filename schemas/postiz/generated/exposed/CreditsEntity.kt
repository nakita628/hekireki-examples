package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CreditsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CreditsEntity>(CreditsTable)

    var organizationId by CreditsTable.organizationId
    var credits by CreditsTable.credits
    var createdAt by CreditsTable.createdAt
    var updatedAt by CreditsTable.updatedAt
    var type by CreditsTable.type
    var organization by OrganizationEntity referencedOn CreditsTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CreditsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
