package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TypebotTable : IdTable<String>("\"Typebot\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val version = text("version").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val icon = text("icon").nullable()
    val name = text("name")
    val folderId =
        optReference(
            "folderId",
            DashboardFolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Typebot_folderId_fkey\"",
        )
    val groups = jsonb<String>("groups", { it }, { it })
    val events = jsonb<String>("events", { it }, { it }).nullable()
    val variables = jsonb<String>("variables", { it }, { it })
    val edges = jsonb<String>("edges", { it }, { it })
    val theme = jsonb<String>("theme", { it }, { it })
    val selectedThemeTemplateId = text("selectedThemeTemplateId").nullable()
    val settings = jsonb<String>("settings", { it }, { it })
    val publicId = text("publicId").nullable().uniqueIndex("\"Typebot_publicId_key\"")
    val customDomain = text("customDomain").nullable().uniqueIndex("\"Typebot_customDomain_key\"")
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Typebot_workspaceId_fkey\"",
        )
    val resultsTablePreferences = jsonb<String>("resultsTablePreferences", { it }, { it }).nullable()
    val isArchived = bool("isArchived").default(false)
    val isClosed = bool("isClosed").default(false)
    val whatsAppCredentialsId = text("whatsAppCredentialsId").nullable()
    val riskLevel = integer("riskLevel").nullable()
    val spaceId =
        optReference(
            "spaceId",
            SpaceTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Typebot_spaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Typebot_pkey\"")

    init {
        index("\"Typebot_workspaceId_idx\"", false, workspaceId)
        index(
            "\"Typebot_isArchived_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"isArchived\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
