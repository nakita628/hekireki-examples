package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object PlatformOAuthClientToUserTable : Table("\"_PlatformOAuthClientToUser\"") {
    val a =
        reference(
            "\"A\"",
            PlatformOAuthClientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_PlatformOAuthClientToUser_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_PlatformOAuthClientToUser_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_PlatformOAuthClientToUser_AB_pkey\"")

    init {
        index("\"_PlatformOAuthClientToUser_B_index\"", false, b)
    }
}
