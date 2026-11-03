package ghostfolio.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"AccessType\" AS ENUM ('MCP', 'PRIVATE', 'PUBLIC')",
            "CREATE TYPE \"AssetClass\" AS ENUM ('ALTERNATIVE_INVESTMENT', 'COMMODITY', 'EQUITY', 'FIXED_INCOME', 'LIQUIDITY', 'REAL_ESTATE')",
            "CREATE TYPE \"AssetSubClass\" AS ENUM ('BOND', 'CASH', 'COLLECTIBLE', 'COMMODITY', 'CRYPTOCURRENCY', 'ETF', 'LOAN', 'MUTUALFUND', 'PRECIOUS_METAL', 'PRIVATE_EQUITY', 'STOCK')",
            "CREATE TYPE \"DataGatheringFrequency\" AS ENUM ('DAILY', 'HOURLY')",
            "CREATE TYPE \"DataSource\" AS ENUM ('ALPHA_VANTAGE', 'COINGECKO', 'EOD_HISTORICAL_DATA', 'FINANCIAL_MODELING_PREP', 'GHOSTFOLIO', 'GOOGLE_SHEETS', 'MANUAL', 'RAPID_API', 'YAHOO')",
            "CREATE TYPE \"MarketDataState\" AS ENUM ('CLOSE', 'INTRADAY')",
            "CREATE TYPE \"Provider\" AS ENUM ('ANONYMOUS', 'GOOGLE', 'INTERNET_IDENTITY', 'OIDC')",
            "CREATE TYPE \"RequestClient\" AS ENUM ('API', 'MCP', 'SELF_HOSTING', 'WEB')",
            "CREATE TYPE \"Role\" AS ENUM ('ADMIN', 'DEMO', 'INACTIVE', 'USER')",
            "CREATE TYPE \"Type\" AS ENUM ('BUY', 'DIVIDEND', 'FEE', 'INTEREST', 'LIABILITY', 'SELL')",
            "CREATE TYPE \"ViewMode\" AS ENUM ('DEFAULT', 'ZEN')",
        )
    val tables: List<Table> =
        listOf(
            AccessTable,
            AccountTable,
            AccountBalanceTable,
            AnalyticsTable,
            ApiKeyTable,
            AssetProfileOverridesTable,
            AssetProfileResolutionTable,
            AssetProfileSplitTable,
            AuthDeviceTable,
            MarketDataTable,
            OrderTable,
            PlatformTable,
            PropertyTable,
            SettingsTable,
            SymbolProfileTable,
            SubscriptionTable,
            TagTable,
            TagsOnAccountsTable,
            UserTable,
            OrderToTagTable,
            UserWatchlistTable,
        )
}
