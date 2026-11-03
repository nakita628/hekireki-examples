package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object PinnedLinksTable : Table("\"_PinnedLinks\"") {
    val a =
        reference(
            "\"A\"",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_PinnedLinks_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_PinnedLinks_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_PinnedLinks_AB_pkey\"")

    init {
        index("\"_PinnedLinks_B_index\"", false, b)
    }
}
