package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object JacksonTtlTable : IdTable<String>("jackson_ttl") {
    override val id = text("key").entityId()
    val expiresAt = long("expiresAt")
    override val primaryKey = PrimaryKey(id, name = "jackson_ttl_pkey")

    init {
        index("_jackson_ttl_expires_at", false, expiresAt)
    }
}
