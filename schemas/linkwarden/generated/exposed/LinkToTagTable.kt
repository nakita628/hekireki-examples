package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object LinkToTagTable : Table("\"_LinkToTag\"") {
    val a =
        reference(
            "\"A\"",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_LinkToTag_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            TagTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_LinkToTag_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_LinkToTag_AB_pkey\"")

    init {
        index("\"_LinkToTag_B_index\"", false, b)
    }
}
