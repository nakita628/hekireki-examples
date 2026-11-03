package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganisationEmailTable : IdTable<String>("\"OrganisationEmail\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email").uniqueIndex("\"OrganisationEmail_email_key\"")
    val emailName = text("emailName")
    val emailDomainId =
        reference(
            "emailDomainId",
            EmailDomainTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationEmail_emailDomainId_fkey\"",
        )
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationEmail_organisationId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationEmail_pkey\"")
}
