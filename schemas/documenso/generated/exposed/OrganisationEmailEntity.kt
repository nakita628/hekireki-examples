package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganisationEmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationEmailEntity>(OrganisationEmailTable)

    var createdAt by OrganisationEmailTable.createdAt
    var updatedAt by OrganisationEmailTable.updatedAt
    var email by OrganisationEmailTable.email
    var emailName by OrganisationEmailTable.emailName
    var emailDomainId by OrganisationEmailTable.emailDomainId
    var emailDomain by EmailDomainEntity referencedOn OrganisationEmailTable.emailDomainId
    var organisationId by OrganisationEmailTable.organisationId
    var organisation by OrganisationEntity referencedOn OrganisationEmailTable.organisationId
    val organisationGlobalSettings by OrganisationGlobalSettingsEntity optionalReferrersOn OrganisationGlobalSettingsTable.emailId
    val teamGlobalSettings by TeamGlobalSettingsEntity optionalReferrersOn TeamGlobalSettingsTable.emailId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganisationEmailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
