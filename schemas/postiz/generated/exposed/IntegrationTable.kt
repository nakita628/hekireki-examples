package postiz.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IntegrationTable : IdTable<String>("\"Integration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val internalId = text("internalId")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Integration_organizationId_fkey\"",
        )
    val name = text("name")
    val customName = text("customName").nullable()
    val picture = text("picture").nullable()
    val providerIdentifier = text("providerIdentifier")
    val type = text("type")
    val token = text("token")
    val disabled = bool("disabled").default(false)
    val tokenExpiration = pgTimestamp("tokenExpiration", 3).nullable()
    val refreshToken = text("refreshToken").nullable()
    val profile = text("profile").nullable()
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val inBetweenSteps = bool("inBetweenSteps").default(false)
    val refreshNeeded = bool("refreshNeeded").default(false)
    val postingTimes = text("postingTimes").default("[{\"time\":120}, {\"time\":400}, {\"time\":700}]")
    val customInstanceDetails = text("customInstanceDetails").nullable()
    val customerId =
        optReference(
            "customerId",
            CustomerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Integration_customerId_fkey\"",
        )
    val rootInternalId = text("rootInternalId").nullable()
    val additionalSettings = text("additionalSettings").nullable().default("[]")
    override val primaryKey = PrimaryKey(id, name = "\"Integration_pkey\"")

    init {
        index("\"Integration_rootInternalId_idx\"", false, rootInternalId)
        index("\"Integration_organizationId_idx\"", false, organizationId)
        index("\"Integration_providerIdentifier_idx\"", false, providerIdentifier)
        index("\"Integration_updatedAt_idx\"", false, updatedAt)
        index("\"Integration_createdAt_idx\"", false, createdAt)
        index("\"Integration_deletedAt_idx\"", false, deletedAt)
        index("\"Integration_customerId_idx\"", false, customerId)
        index("\"Integration_inBetweenSteps_idx\"", false, inBetweenSteps)
        index("\"Integration_refreshNeeded_idx\"", false, refreshNeeded)
        index("\"Integration_disabled_idx\"", false, disabled)
        uniqueIndex("\"Integration_organizationId_internalId_key\"", organizationId, internalId)
    }
}
