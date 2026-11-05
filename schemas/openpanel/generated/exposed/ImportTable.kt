package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ImportTable : IdTable<UUID>("imports") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"imports_projectId_fkey\"",
        )
    val jobId = text("jobId").nullable()
    val status = pgEnum("status", "\"ImportStatus\"", ImportStatus.entries, ImportStatus::dbName)
    val statusMessage = text("statusMessage").nullable()
    val errorMessage = text("errorMessage").nullable()
    val config = jsonb<String>("config", { it }, { it })
    val totalEvents = integer("totalEvents").default(0)
    val processedEvents = integer("processedEvents").default(0)
    val currentStep = text("currentStep").nullable()
    val currentBatch = text("currentBatch").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "imports_pkey")
}
