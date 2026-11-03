package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object PlatformTable : IdTable<String>("\"Platform\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name").nullable()
    val url = text("url").uniqueIndex("\"Platform_url_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"Platform_pkey\"")

    init {
        index("\"Platform_name_idx\"", false, name)
    }
}
