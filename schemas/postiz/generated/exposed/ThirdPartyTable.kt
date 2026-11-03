package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ThirdPartyTable : IdTable<String>("\"ThirdParty\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ThirdParty_organizationId_fkey\"",
        )
    val identifier = text("identifier")
    val name = text("name")
    val internalId = text("internalId")
    val apiKey = text("apiKey")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ThirdParty_pkey\"")

    init {
        index("\"ThirdParty_organizationId_idx\"", false, organizationId)
        index("\"ThirdParty_deletedAt_idx\"", false, deletedAt)
        uniqueIndex("\"ThirdParty_organizationId_internalId_key\"", organizationId, internalId)
    }
}
