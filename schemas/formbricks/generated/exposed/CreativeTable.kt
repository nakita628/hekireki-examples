package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object CreativeTable : IdTable<String>("\"Creative\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val type = pgEnum("type", "\"CreativeType\"", CreativeType.entries, CreativeType::dbName)
    val status = pgEnum("status", "\"CreativeStatus\"", CreativeStatus.entries, CreativeStatus::dbName).default(CreativeStatus.DRAFT)
    val publishedAt = pgTimestamp("publishedAt", 3).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Creative_workspaceId_fkey\"",
        )
    val createdBy =
        optReference(
            "createdBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Creative_createdBy_fkey\"",
        )
    val content = jsonb<String>("content", { it }, { it })
    val styling = jsonb<String>("styling", { it }, { it })
    val surveyId = text("surveyId").nullable()
    val linkUrl = text("linkUrl").nullable()
    val showOn = pgEnum("showOn", "\"CreativeShowOn\"", CreativeShowOn.entries, CreativeShowOn::dbName).default(CreativeShowOn.PAGE_VIEW)
    val showConditions = jsonb<String>("showConditions", { it }, { it }).nullable()
    val displayPercentage = double("displayPercentage").nullable()
    val startAt = pgTimestamp("startAt", 3).nullable()
    val endAt = pgTimestamp("endAt", 3).nullable()
    val autoSelectLanguage = bool("autoSelectLanguage").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Creative_pkey\"")

    init {
        index("\"Creative_workspaceId_updated_at_idx\"", false, workspaceId, updatedAt)
        index("\"Creative_workspaceId_created_at_idx\"", false, workspaceId, createdAt)
        index("\"Creative_workspaceId_name_idx\"", false, workspaceId, name)
        index("\"Creative_surveyId_idx\"", false, surveyId)
        foreignKey(
            surveyId to SurveyTable.id,
            workspaceId to SurveyTable.workspaceId,
            onDelete = ReferenceOption.NO_ACTION,
            onUpdate = ReferenceOption.NO_ACTION,
            name = "\"Creative_surveyId_workspaceId_fkey\"",
        )
    }
}
