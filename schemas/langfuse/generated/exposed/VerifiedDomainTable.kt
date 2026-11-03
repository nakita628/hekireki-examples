package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object VerifiedDomainTable : IdTable<String>("verified_domains") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organization_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "verified_domains_organization_id_fkey",
        )
    val domain = text("domain")
    val verificationToken =
        text("verification_token").clientDefault { UUID.randomUUID().toString() }.uniqueIndex("verified_domains_verification_token_key")
    val verifiedAt = pgTimestamp("verified_at", 3).nullable()
    val createdByUserId = text("created_by_user_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "verified_domains_pkey")

    init {
        index("verified_domains_organization_id_idx", false, organizationId)
        uniqueIndex("verified_domains_organization_id_domain_key", organizationId, domain)
    }
}
