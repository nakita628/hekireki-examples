package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UsedCodesTable : IdTable<String>("\"UsedCodes\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val code = text("code")
    val orgId =
        reference(
            "orgId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UsedCodes_orgId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UsedCodes_pkey\"")

    init {
        index("\"UsedCodes_code_idx\"", false, code)
    }
}
