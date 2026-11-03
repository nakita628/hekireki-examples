package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UsedCodesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UsedCodesEntity>(UsedCodesTable)

    var code by UsedCodesTable.code
    var orgId by UsedCodesTable.orgId
    var createdAt by UsedCodesTable.createdAt
    var updatedAt by UsedCodesTable.updatedAt
    var organization by OrganizationEntity referencedOn UsedCodesTable.orgId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UsedCodesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
