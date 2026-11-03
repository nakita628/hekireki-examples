package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationBillingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationBillingEntity>(OrganizationBillingTable)

    val organization by OrganizationEntity referencedOn OrganizationBillingTable.id
    var stripeCustomerId by OrganizationBillingTable.stripeCustomerId
    var limits by OrganizationBillingTable.limits
    var usageCycleAnchor by OrganizationBillingTable.usageCycleAnchor
    var stripe by OrganizationBillingTable.stripe
    var createdAt by OrganizationBillingTable.createdAt
    var updatedAt by OrganizationBillingTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationBillingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
