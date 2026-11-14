package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ModelProviderScopeTable : IdTable<String>("\"ModelProviderScope\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val modelProviderId = text("modelProviderId")
    val scopeType = pgEnum("scopeType", "\"ModelProviderScopeType\"", ModelProviderScopeType.entries, ModelProviderScopeType::dbName)
    val scopeId = text("scopeId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ModelProviderScope_pkey\"")

    init {
        index("\"ModelProviderScope_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        index("\"ModelProviderScope_modelProviderId_idx\"", false, modelProviderId)
        uniqueIndex("\"ModelProviderScope_modelProviderId_scopeType_scopeId_key\"", modelProviderId, scopeType, scopeId)
    }
}
