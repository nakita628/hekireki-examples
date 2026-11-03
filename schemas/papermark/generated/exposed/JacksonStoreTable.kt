package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object JacksonStoreTable : IdTable<String>("jackson_store") {
    override val id = text("key").entityId()
    val value = text("value")
    val iv = text("iv").nullable()
    val tag = text("tag").nullable()
    val namespace = text("namespace").nullable()
    val createdAt = pgTimestamp("createdAt", 0).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val modifiedAt = pgTimestamp("modifiedAt", 0).nullable()
    override val primaryKey = PrimaryKey(id, name = "jackson_store_pkey")

    init {
        index("_jackson_store_namespace", false, namespace)
    }
}
