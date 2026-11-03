package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailDomainTable : IdTable<String>("\"EmailDomain\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status =
        pgEnum("status", "\"EmailDomainStatus\"", EmailDomainStatus.entries, EmailDomainStatus::dbName).default(EmailDomainStatus.PENDING)
    val selector = text("selector").uniqueIndex("\"EmailDomain_selector_key\"")
    val domain = text("domain").uniqueIndex("\"EmailDomain_domain_key\"")
    val publicKey = text("publicKey")
    val privateKey = text("privateKey")
    val lastVerifiedAt = pgTimestamp("lastVerifiedAt", 3).nullable()
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmailDomain_organisationId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EmailDomain_pkey\"")
}
