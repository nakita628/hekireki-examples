package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmbeddedDataTable : IdTable<String>("\"EmbeddedData\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val key = text("key").nullable()
    val name = text("name")
    val description = text("description").nullable()
    val source1 = pgEnum("source", "\"EmbeddedDataSource\"", EmbeddedDataSource.entries, EmbeddedDataSource::dbName)
    val dataType =
        pgEnum("dataType", "\"EmbeddedDataType\"", EmbeddedDataType.entries, EmbeddedDataType::dbName).default(EmbeddedDataType.STRING)
    val defaultValue = jsonb<String>("defaultValue", { it }, { it }).nullable()
    val locked = bool("locked").default(false)
    val surveyId = text("surveyId").nullable()
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmbeddedData_workspaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EmbeddedData_pkey\"")

    init {
        index("\"EmbeddedData_surveyId_idx\"", false, surveyId)
        uniqueIndex("\"EmbeddedData_workspaceId_key_key\"", workspaceId, key)
        uniqueIndex("\"EmbeddedData_id_workspaceId_key\"", id, workspaceId)
        foreignKey(
            surveyId to SurveyTable.id,
            workspaceId to SurveyTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"EmbeddedData_surveyId_workspaceId_fkey\"",
        )
    }
}
