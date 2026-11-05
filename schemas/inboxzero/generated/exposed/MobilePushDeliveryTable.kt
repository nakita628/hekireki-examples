package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MobilePushDeliveryTable : IdTable<String>("\"MobilePushDelivery\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deduplicationKey = text("deduplicationKey")
    val mobilePushTokenId =
        reference(
            "mobilePushTokenId",
            MobilePushTokenTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MobilePushDelivery_mobilePushTokenId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MobilePushDelivery_pkey\"")

    init {
        index("\"MobilePushDelivery_createdAt_idx\"", false, createdAt)
        index("\"MobilePushDelivery_mobilePushTokenId_idx\"", false, mobilePushTokenId)
        uniqueIndex("\"MobilePushDelivery_key_token_key\"", deduplicationKey, mobilePushTokenId)
    }
}
