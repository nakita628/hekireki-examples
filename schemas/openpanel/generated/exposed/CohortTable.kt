package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CohortTable : IdTable<UUID>("cohorts") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val name = text("name")
    val description = text("description").nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"cohorts_projectId_fkey\"",
        )
    val definition = jsonb<String>("definition", { it }, { it }).default("{}")
    val isStatic = bool("isStatic").default(false)
    val profileCount = integer("profileCount").default(0)
    val lastComputedAt = pgTimestamp("lastComputedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "cohorts_pkey")

    init {
        index("\"cohorts_projectId_idx\"", false, projectId)
    }
}
