package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object DataContentTable : IdTable<String>("\"DataContent\"") {
    override val id = text("id").entityId()
    val type = pgEnum("type", "\"DocumentDataType\"", DocumentDataType.entries, DocumentDataType::dbName)
    val data = text("data")
    val metadata = jsonb<String>("metadata", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"DataContent_pkey\"")
}
