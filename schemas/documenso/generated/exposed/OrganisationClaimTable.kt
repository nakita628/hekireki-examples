package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganisationClaimTable : IdTable<String>("\"OrganisationClaim\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val originalSubscriptionClaimId = text("originalSubscriptionClaimId").nullable()
    val teamCount = integer("teamCount")
    val memberCount = integer("memberCount")
    val envelopeItemCount = integer("envelopeItemCount")
    val recipientCount = integer("recipientCount")
    val envelopeContentCount = integer("envelopeContentCount")
    val envelopeContentImageCount = integer("envelopeContentImageCount")
    val flags = jsonb<String>("flags", { it }, { it })
    val documentRateLimits = jsonb<String>("documentRateLimits", { it }, { it })
    val documentQuota = integer("documentQuota").nullable()
    val emailRateLimits = jsonb<String>("emailRateLimits", { it }, { it })
    val emailQuota = integer("emailQuota").nullable()
    val apiRateLimits = jsonb<String>("apiRateLimits", { it }, { it })
    val apiQuota = integer("apiQuota").nullable()
    val emailTransportId =
        optReference(
            "emailTransportId",
            EmailTransportTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationClaim_emailTransportId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationClaim_pkey\"")
}
