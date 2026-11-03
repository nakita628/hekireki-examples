package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraAgentVersionsTable : IdTable<String>("mastra_agent_versions") {
    override val id = text("id").entityId()
    val agentId = text("agentId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val description = text("description").nullable()
    val instructions = text("instructions")
    val model = jsonb<String>("model", { it }, { it })
    val tools = jsonb<String>("tools", { it }, { it }).nullable()
    val defaultOptions = jsonb<String>("defaultOptions", { it }, { it }).nullable()
    val workflows = jsonb<String>("workflows", { it }, { it }).nullable()
    val agents = jsonb<String>("agents", { it }, { it }).nullable()
    val integrationTools = jsonb<String>("integrationTools", { it }, { it }).nullable()
    val inputProcessors = jsonb<String>("inputProcessors", { it }, { it }).nullable()
    val outputProcessors = jsonb<String>("outputProcessors", { it }, { it }).nullable()
    val memory = jsonb<String>("memory", { it }, { it }).nullable()
    val scorers = jsonb<String>("scorers", { it }, { it }).nullable()
    val mcpClients = jsonb<String>("mcpClients", { it }, { it }).nullable()
    val requestContextSchema = jsonb<String>("requestContextSchema", { it }, { it }).nullable()
    val workspace = jsonb<String>("workspace", { it }, { it }).nullable()
    val skills = jsonb<String>("skills", { it }, { it }).nullable()
    val skillsFormat = text("skillsFormat").nullable()
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val durable = jsonb<String>("durable", { it }, { it }).nullable()
    val browser = jsonb<String>("browser", { it }, { it }).nullable()
    val toolProviders = jsonb<String>("toolProviders", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_agent_versions_pkey")
}
