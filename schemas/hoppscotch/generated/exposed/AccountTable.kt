package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountTable : IdTable<String>("\"Account\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_userId_fkey\"",
        )
    val provider = text("provider")
    val providerAccountId = text("providerAccountId")
    val providerRefreshToken = text("providerRefreshToken").nullable()
    val providerAccessToken = text("providerAccessToken").nullable()
    val providerScope = text("providerScope").nullable()
    val loggedIn = pgTimestamptz("loggedIn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Account_pkey\"")

    init {
        uniqueIndex("\"Account_provider_providerAccountId_key\"", provider, providerAccountId)
    }
}
