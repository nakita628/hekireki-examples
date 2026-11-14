package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ModelDefaultConfigScopeTable : IdTable<String>("\"ModelDefaultConfigScope\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val configId = text("configId")
    val scopeType = pgEnum("scopeType", "\"ModelDefaultScopeType\"", ModelDefaultScopeType.entries, ModelDefaultScopeType::dbName)
    val scopeId = text("scopeId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ModelDefaultConfigScope_pkey\"")

    init {
        index("\"ModelDefaultConfigScope_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        index("\"ModelDefaultConfigScope_configId_idx\"", false, configId)
        uniqueIndex("\"ModelDefaultConfigScope_configId_scopeType_scopeId_key\"", configId, scopeType, scopeId)
    }
}
