package linkwarden.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"Theme\" AS ENUM ('dark', 'light', 'auto')",
            "CREATE TYPE \"AiTaggingMethod\" AS ENUM ('DISABLED', 'GENERATE', 'EXISTING', 'PREDEFINED')",
            "CREATE TYPE \"LinksRouteTo\" AS ENUM ('ORIGINAL', 'PDF', 'READABLE', 'MONOLITH', 'SCREENSHOT', 'DETAILS')",
            "CREATE TYPE \"SubscriptionProvider\" AS ENUM ('STRIPE', 'APPLE', 'GOOGLE')",
            "CREATE TYPE \"DashboardSectionType\" AS ENUM ('STATS', 'RECENT_LINKS', 'PINNED_LINKS', 'COLLECTION')",
            "CREATE TYPE \"AppMigrationStatus\" AS ENUM ('APPLIED', 'PENDING', 'FAILED')",
        )
    val tables: List<Table> =
        listOf(
            AccountTable,
            UserTable,
            VerificationTokenTable,
            PasswordResetTokenTable,
            CollectionTable,
            UsersAndCollectionsTable,
            LinkTable,
            TagTable,
            SubscriptionTable,
            AccessTokenTable,
            RssSubscriptionTable,
            HighlightTable,
            DashboardSectionTable,
            AppMigrationTable,
            PinnedLinksTable,
            LinkToTagTable,
        )
}
