package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountsTable : IdTable<Int>("accounts") {
    override val id = integer("id").autoIncrement().entityId()
    val name = pgVarchar("name").default("")
    val nickname = pgVarchar("nickname").default("")
    val password = pgVarchar("password").default("")
    val image = pgVarchar("image").default("")
    val apiToken = pgVarchar("apiToken").default("")
    val description = text("description").default("")
    val note = integer("note").default(0)
    val role = pgVarchar("role").default("")
    val loginType = pgVarchar("loginType").default("")
    val linkAccountId = integer("linkAccountId").nullable()
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "accounts_pkey")
}
