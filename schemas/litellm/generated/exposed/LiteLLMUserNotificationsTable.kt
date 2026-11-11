package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LiteLLMUserNotificationsTable : IdTable<String>("\"LiteLLM_UserNotifications\"") {
    override val id = text("request_id").entityId()
    val userId = text("user_id")
    val models = array("models", TextColumnType()).nullable()
    val justification = text("justification")
    val status = text("status")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_UserNotifications_pkey\"")
}
