package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvaluatorVersionLabelTable : IdTable<String>("\"EvaluatorVersionLabel\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val evaluatorId = text("evaluatorId")
    val label = text("label")
    val versionId = text("versionId")
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"EvaluatorVersionLabel_pkey\"")

    init {
        index("\"EvaluatorVersionLabel_projectId_idx\"", false, projectId)
        index("\"EvaluatorVersionLabel_versionId_idx\"", false, versionId)
        uniqueIndex("\"EvaluatorVersionLabel_evaluatorId_label_key\"", evaluatorId, label)
    }
}
