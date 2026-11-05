package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object EmailUnsubscribeTable : IdTable<UUID>("email_unsubscribes") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val email = text("email")
    val category = text("category")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "email_unsubscribes_pkey")

    init {
        uniqueIndex("email_unsubscribes_email_category_key", email, category)
    }
}
