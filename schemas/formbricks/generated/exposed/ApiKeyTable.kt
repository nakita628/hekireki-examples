package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyTable : IdTable<String>("\"ApiKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("createdBy").nullable()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val label = text("label")
    val hashedKey = text("hashedKey")
    val lookupHash = text("lookupHash").nullable().uniqueIndex("\"ApiKey_lookupHash_key\"")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_organizationId_fkey\"",
        )
    val organizationAccess = jsonb<String>("organizationAccess", { it }, { it }).default("{}")
    override val primaryKey = PrimaryKey(id, name = "\"ApiKey_pkey\"")

    init {
        index("\"ApiKey_organizationId_idx\"", false, organizationId)
    }
}
