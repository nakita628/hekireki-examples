package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object TrendingLogTable : IdTable<String>("\"TrendingLog\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val language = text("language").nullable()
    val date = pgTimestamp("date", 3)
    override val primaryKey = PrimaryKey(id, name = "\"TrendingLog_pkey\"")
}
