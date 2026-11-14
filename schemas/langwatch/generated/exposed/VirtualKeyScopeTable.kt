package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object VirtualKeyScopeTable : IdTable<String>("\"VirtualKeyScope\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val virtualKeyId = text("virtualKeyId")
    val scopeType = pgEnum("scopeType", "\"VirtualKeyScopeType\"", VirtualKeyScopeType.entries, VirtualKeyScopeType::dbName)
    val scopeId = text("scopeId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"VirtualKeyScope_pkey\"")

    init {
        index("\"VirtualKeyScope_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        index("\"VirtualKeyScope_virtualKeyId_idx\"", false, virtualKeyId)
        uniqueIndex("\"VirtualKeyScope_virtualKeyId_scopeType_scopeId_key\"", virtualKeyId, scopeType, scopeId)
    }
}
