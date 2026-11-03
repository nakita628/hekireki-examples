package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AvatarImageTable : IdTable<String>("\"AvatarImage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val bytes = text("bytes")
    override val primaryKey = PrimaryKey(id, name = "\"AvatarImage_pkey\"")
}
