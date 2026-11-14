package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PlatformToolPolicyTable : IdTable<String>("\"PlatformToolPolicy\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val toolSlug = text("toolSlug")
    val allowVk = bool("allowVk").default(true)
    val allowOtelDirect = bool("allowOtelDirect").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PlatformToolPolicy_pkey\"")

    init {
        index("\"PlatformToolPolicy_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"PlatformToolPolicy_organizationId_toolSlug_key\"", organizationId, toolSlug)
    }
}
