package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object DocumentDataTable : IdTable<String>("\"DocumentData\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = pgEnum("type", "\"DocumentDataType\"", DocumentDataType.entries, DocumentDataType::dbName)
    val data = text("data")
    val initialData = text("initialData")
    override val primaryKey = PrimaryKey(id, name = "\"DocumentData_pkey\"")
}
