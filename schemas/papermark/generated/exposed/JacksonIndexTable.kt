package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object JacksonIndexTable : IdTable<Int>("jackson_index") {
    override val id = integer("id").autoIncrement().entityId()
    val key = text("key")
    val storeKey = text("storeKey")
    override val primaryKey = PrimaryKey(id, name = "jackson_index_pkey")

    init {
        index("_jackson_index_key_store", false, key, storeKey)
    }
}
