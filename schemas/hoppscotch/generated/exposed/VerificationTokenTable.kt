package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object VerificationTokenTable : IdTable<String>("\"VerificationToken\"") {
    val deviceIdentifier = text("deviceIdentifier")
    override val id = text("token").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerificationToken_userUid_fkey\"",
        )
    val expiresOn = pgTimestamptz("expiresOn", 3)

    init {
        uniqueIndex("\"VerificationToken_token_key\"", id)
        uniqueIndex("\"VerificationToken_deviceIdentifier_token_key\"", deviceIdentifier, id)
    }
}
