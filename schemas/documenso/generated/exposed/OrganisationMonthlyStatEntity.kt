package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganisationMonthlyStatEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationMonthlyStatEntity>(OrganisationMonthlyStatTable)

    var createdAt by OrganisationMonthlyStatTable.createdAt
    var updatedAt by OrganisationMonthlyStatTable.updatedAt
    var organisationId by OrganisationMonthlyStatTable.organisationId
    var organisation by OrganisationEntity referencedOn OrganisationMonthlyStatTable.organisationId
    var period by OrganisationMonthlyStatTable.period
    var documentCount by OrganisationMonthlyStatTable.documentCount
    var emailCount by OrganisationMonthlyStatTable.emailCount
    var apiCount by OrganisationMonthlyStatTable.apiCount
    var emailReports by OrganisationMonthlyStatTable.emailReports

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganisationMonthlyStatTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
