package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object KvStoreEntryTable : IdTable<String>("kv") {
    override val id = text("key").entityId()
    val value = jsonb<String>("value", { it }, { it })
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "kv_pkey")

    init {
        index("kv_expires_at_idx", false, expiresAt)
        index("kv_key_pattern_idx", false, functions = listOf(indexExpression("\"key\" text_pattern_ops")))
    }
}
