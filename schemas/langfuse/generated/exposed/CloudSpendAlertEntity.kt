package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CloudSpendAlertEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CloudSpendAlertEntity>(CloudSpendAlertTable)

    var orgId by CloudSpendAlertTable.orgId
    var org by OrganizationEntity referencedOn CloudSpendAlertTable.orgId
    var title by CloudSpendAlertTable.title
    var threshold by CloudSpendAlertTable.threshold
    var triggeredAt by CloudSpendAlertTable.triggeredAt
    var createdAt by CloudSpendAlertTable.createdAt
    var updatedAt by CloudSpendAlertTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CloudSpendAlertTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
