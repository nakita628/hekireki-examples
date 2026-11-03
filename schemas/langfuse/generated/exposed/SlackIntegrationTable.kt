package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SlackIntegrationTable : IdTable<String>("slack_integrations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "slack_integrations_project_id_fkey",
        ).uniqueIndex("slack_integrations_project_id_key")
    val teamId = text("team_id")
    val teamName = text("team_name")
    val botToken = text("bot_token")
    val botUserId = text("bot_user_id")
    val installerUserId = text("installer_user_id").nullable()
    val installerSlackUserId = text("installer_slack_user_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "slack_integrations_pkey")

    init {
        index("slack_integrations_team_id_idx", false, teamId)
    }
}
