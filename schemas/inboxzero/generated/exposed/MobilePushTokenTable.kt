package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MobilePushTokenTable : IdTable<String>("\"MobilePushToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val token = text("token").uniqueIndex("\"MobilePushToken_token_key\"")
    val platform = pgEnum("platform", "\"MobilePushPlatform\"", MobilePushPlatform.entries, MobilePushPlatform::dbName)
    val tokenType =
        pgEnum("tokenType", "\"MobilePushTokenType\"", MobilePushTokenType.entries, MobilePushTokenType::dbName).default(
            MobilePushTokenType.EXPO,
        )
    val appVersion = text("appVersion").nullable()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MobilePushToken_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MobilePushToken_pkey\"")

    init {
        index("\"MobilePushToken_userId_idx\"", false, userId)
    }
}
