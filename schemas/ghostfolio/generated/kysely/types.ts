import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type AccessType = 'MCP' | 'PRIVATE' | 'PUBLIC'

export type AssetClass =
  | 'ALTERNATIVE_INVESTMENT'
  | 'COMMODITY'
  | 'EQUITY'
  | 'FIXED_INCOME'
  | 'LIQUIDITY'
  | 'REAL_ESTATE'

export type AssetSubClass =
  | 'BOND'
  | 'CASH'
  | 'COLLECTIBLE'
  | 'COMMODITY'
  | 'CRYPTOCURRENCY'
  | 'ETF'
  | 'LOAN'
  | 'MUTUALFUND'
  | 'PRECIOUS_METAL'
  | 'PRIVATE_EQUITY'
  | 'STOCK'

export type DataGatheringFrequency = 'DAILY' | 'HOURLY'

export type DataSource =
  | 'ALPHA_VANTAGE'
  | 'COINGECKO'
  | 'EOD_HISTORICAL_DATA'
  | 'FINANCIAL_MODELING_PREP'
  | 'GHOSTFOLIO'
  | 'GOOGLE_SHEETS'
  | 'MANUAL'
  | 'RAPID_API'
  | 'YAHOO'

export type MarketDataState = 'CLOSE' | 'INTRADAY'

export type Provider = 'ANONYMOUS' | 'GOOGLE' | 'INTERNET_IDENTITY' | 'OIDC'

export type RequestClient = 'API' | 'MCP' | 'SELF_HOSTING' | 'WEB'

export type Role = 'ADMIN' | 'DEMO' | 'INACTIVE' | 'USER'

export type Type = 'BUY' | 'DIVIDEND' | 'FEE' | 'INTEREST' | 'LIABILITY' | 'SELL'

export interface Access {
  alias: string | null
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
  granteeUserId: string | null
  id: string
  lastUsedAt: Timestamp | null
  scopes: Generated<string[]>
  settings: Generated<unknown>
  type: Generated<AccessType>
  updatedAt: Timestamp
  userId: string
}

export interface Account {
  comment: string | null
  createdAt: Generated<Timestamp>
  currency: string | null
  id: string
  name: string | null
  platformId: string | null
  updatedAt: Timestamp
  userId: string
}

export interface AccountBalance {
  accountId: string
  comment: string | null
  createdAt: Generated<Timestamp>
  date: Generated<Timestamp>
  id: string
  updatedAt: Timestamp
  userId: string
  value: number
}

export interface Analytics {
  activityCount: Generated<number>
  country: string | null
  dataProviderGhostfolioDailyRequests: Generated<number>
  lastRequestAt: Generated<Timestamp>
  lastRequestClient: RequestClient | null
  updatedAt: Timestamp
  userId: string
}

export interface ApiKey {
  createdAt: Generated<Timestamp>
  hashedKey: string
  id: string
  updatedAt: Timestamp
  userId: string
}

export interface AssetProfileOverrides {
  assetClass: AssetClass | null
  assetSubClass: AssetSubClass | null
  countries: Generated<unknown>
  holdings: Generated<unknown>
  name: string | null
  sectors: Generated<unknown>
  symbolProfileId: string
  updatedAt: Timestamp
  url: string | null
}

export interface AssetProfileResolution {
  createdAt: Generated<Timestamp>
  currency: string
  dataSourceOrigin: DataSource
  dataSourceTarget: DataSource
  id: string
  requestCount: Generated<number>
  symbolOrigin: string
  symbolTarget: string
  updatedAt: Timestamp
}

export interface AssetProfileSplit {
  createdAt: Generated<Timestamp>
  date: Timestamp
  denominator: number
  id: string
  numerator: number
  symbolProfileId: string
  updatedAt: Timestamp
}

export interface AuthDevice {
  createdAt: Generated<Timestamp>
  credentialId: Buffer
  credentialPublicKey: Buffer
  counter: number
  id: string
  updatedAt: Timestamp
  userId: string
}

export interface MarketData {
  createdAt: Generated<Timestamp>
  dataSource: DataSource
  date: Timestamp
  id: string
  isCarriedForward: Generated<boolean>
  marketPrice: number
  state: Generated<MarketDataState>
  symbol: string
}

export interface Order {
  accountId: string | null
  accountUserId: string | null
  comment: string | null
  createdAt: Generated<Timestamp>
  currency: string | null
  date: Timestamp
  fee: number
  id: string
  quantity: number
  symbolProfileId: string
  type: Type
  unitPrice: number
  updatedAt: Timestamp
  userId: string
}

export interface Platform {
  id: string
  name: string | null
  url: string
}

export interface Property {
  key: string
  value: string
}

export interface Settings {
  settings: unknown
  updatedAt: Timestamp
  userId: string
}

export interface SymbolProfile {
  assetClass: AssetClass | null
  assetSubClass: AssetSubClass | null
  comment: string | null
  countries: unknown
  createdAt: Generated<Timestamp>
  currency: string
  cusip: string | null
  dataGatheringFrequency: Generated<DataGatheringFrequency>
  dataSource: DataSource
  figi: string | null
  figiComposite: string | null
  figiShareClass: string | null
  holdings: Generated<unknown>
  id: string
  isActive: Generated<boolean>
  isin: string | null
  name: string | null
  scraperConfiguration: unknown
  sectors: unknown
  symbol: string
  symbolMapping: unknown
  updatedAt: Timestamp
  url: string | null
  userId: string | null
}

export interface Subscription {
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
  id: string
  price: number | null
  stripeCheckoutSessionId: string | null
  updatedAt: Timestamp
  userId: string
}

export interface Tag {
  id: string
  name: string
  userId: string | null
}

export interface TagsOnAccounts {
  accountId: string
  createdAt: Generated<Timestamp>
  tagId: string
  updatedAt: Timestamp
  userId: string
}

export interface User {
  accessToken: string | null
  authChallenge: string | null
  createdAt: Generated<Timestamp>
  id: string
  provider: Generated<Provider>
  role: Generated<Role>
  thirdPartyId: string | null
  updatedAt: Timestamp
}

export interface OrderToTag {
  A: string
  B: string
}

export interface UserWatchlist {
  A: string
  B: string
}

export interface DB {
  Access: Access
  Account: Account
  AccountBalance: AccountBalance
  Analytics: Analytics
  ApiKey: ApiKey
  SymbolProfileOverrides: AssetProfileOverrides
  AssetProfileResolution: AssetProfileResolution
  AssetProfileSplit: AssetProfileSplit
  AuthDevice: AuthDevice
  MarketData: MarketData
  Order: Order
  Platform: Platform
  Property: Property
  Settings: Settings
  SymbolProfile: SymbolProfile
  Subscription: Subscription
  Tag: Tag
  TagsOnAccounts: TagsOnAccounts
  User: User
  _OrderToTag: OrderToTag
  _UserWatchlist: UserWatchlist
}
