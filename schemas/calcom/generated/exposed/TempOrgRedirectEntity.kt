package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TempOrgRedirectEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TempOrgRedirectEntity>(TempOrgRedirectTable)

    var from by TempOrgRedirectTable.from
    var fromOrgId by TempOrgRedirectTable.fromOrgId
    var type by TempOrgRedirectTable.type
    var toUrl by TempOrgRedirectTable.toUrl
    var enabled by TempOrgRedirectTable.enabled
    var createdAt by TempOrgRedirectTable.createdAt
    var updatedAt by TempOrgRedirectTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TempOrgRedirectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
